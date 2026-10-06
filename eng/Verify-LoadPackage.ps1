param(
    [Parameter(Mandatory = $true)]
    [string] $PackagePath
)

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.IO.Compression.FileSystem
$archive = [IO.Compression.ZipFile]::OpenRead((Resolve-Path -LiteralPath $PackagePath).Path)
function Read-PackageEntry([string] $name) {
    $entry = $archive.GetEntry($name)
    if ($null -eq $entry) { throw "Missing package entry: $name" }
    $reader = [IO.StreamReader]::new($entry.Open())
    try { return $reader.ReadToEnd() } finally { $reader.Dispose() }
}

try {
    if ($null -eq $archive.GetEntry('lib/net481/StaadPro.Interop.dll')) { throw 'Missing net481 library.' }
    [xml] $xml = Read-PackageEntry 'lib/net481/StaadPro.Interop.xml'
    $readme = Read-PackageEntry 'README.md'
    $guide = Read-PackageEntry 'docs/Load-Queries.md'
    if ($readme -notmatch 'net481') { throw 'Incomplete packaged README.' }
    $queryMethods = @('GetNodalLoads', 'GetMemberUniformlyDistributedLoads', 'GetMemberConcentratedLoads', 'GetMemberConcentratedMoments', 'GetMemberLinearVaryingLoads', 'GetMemberTrapezoidalLoads')
    foreach ($method in $queryMethods) {
        if ($readme -notmatch $method -or $guide -notmatch $method) { throw "Missing packaged guidance for $method" }
    }
    $nuspecEntry = @($archive.Entries | Where-Object FullName -like '*.nuspec')
    if ($nuspecEntry.Count -ne 1) { throw 'Expected exactly one nuspec.' }
    [xml] $nuspec = Read-PackageEntry $nuspecEntry[0].FullName
    $metadata = $nuspec.package.metadata
    if ($metadata.id -ne 'StaadPro.Interop' -or $metadata.license.InnerText -ne 'MIT' -or $metadata.readme -ne 'README.md') { throw 'Invalid package identity, license or README metadata.' }
    if ($metadata.repository.url -ne 'https://github.com/structuraldeveloper/StaadPro.Interop') { throw 'Missing repository metadata.' }
    $version = [string]$metadata.version
    $snippets = @()
    foreach ($owner in @('Adapters.Interfaces.IOSLoad', 'Adapters.Models.OSLoadAdapter')) {
        foreach ($method in $queryMethods) {
            $name = "M:StaadPro.Interop.$owner.$method(StaadPro.Interop.Entities.ILoadCase,System.Int32)"
            $members = @($xml.doc.members.member | Where-Object name -eq $name)
            if ($members.Count -ne 1) { throw "Missing or duplicate IntelliSense entry: $name" }
            $member = $members[0]
            foreach ($tag in @('summary','returns','remarks','example/code')) {
                $node = $member.SelectSingleNode($tag)
                if ($null -eq $node -or [string]::IsNullOrWhiteSpace($node.InnerText)) { throw "Incomplete $tag for $name" }
            }
            if (@($member.param).Count -ne 2 -or @($member.exception).Count -ne 6 -or $null -ne $member.SelectSingleNode('.//inheritdoc')) { throw "Incomplete contract for $name" }
            if ($member.OuterXml -match 'cref="!:') { throw "Unresolved documentation reference in $name" }
            $snippets += "{`n" + $member.SelectSingleNode('example/code').InnerText + "`n}"
        }
    }
    foreach ($typeName in @('MemberConcentratedMoment', 'MemberLinearVaryingLoad', 'MemberTrapezoidalLoad')) {
        $member = @($xml.doc.members.member | Where-Object name -eq "T:StaadPro.Interop.Entities.$typeName")
        if ($member.Count -ne 1 -or [string]::IsNullOrWhiteSpace($member[0].SelectSingleNode('summary').InnerText)) { throw "Missing model documentation: $typeName" }
    }
    if ($xml.OuterXml -match 'ConnectivityService|SegregateMembersBasedOnConnectivity|SegregatePlatesBasedOnConnectivity') { throw 'Rolled-back connectivity API leaked into package.' }
} finally { $archive.Dispose() }

# Unique project and package cache: no project references or previously installed library.
$consumerRoot = Join-Path ([IO.Path]::GetTempPath()) ('StaadProLoadConsumer-' + [Guid]::NewGuid().ToString('N'))
$null = New-Item -ItemType Directory -Path $consumerRoot
$feed = [Security.SecurityElement]::Escape((Split-Path -Parent (Resolve-Path -LiteralPath $PackagePath).Path))
$config = @"
<?xml version="1.0" encoding="utf-8"?>
<configuration><packageSources><clear/><add key="local" value="$feed"/><add key="nuget.org" value="https://api.nuget.org/v3/index.json"/></packageSources></configuration>
"@
$project = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup><TargetFramework>net481</TargetFramework><OutputType>Exe</OutputType><LangVersion>latest</LangVersion><TreatWarningsAsErrors>true</TreatWarningsAsErrors><RestorePackagesPath>packages</RestorePackagesPath></PropertyGroup>
  <ItemGroup><Reference Include="Microsoft.CSharp"/><PackageReference Include="StaadPro.Interop" Version="$version"/></ItemGroup>
</Project>
"@
$program = @'
using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

public static class Program
{
    public static void Main()
    {
        var fake = new FakeLoad();
        using (var wrapper = new OpenStaadWrapper(new FakeRoot(fake)))
        {
            IOSLoad load = wrapper.Load;
            RunDocumentationExamples(load);
            foreach (var type in new[] { LoadCaseType.PrimaryLoad, LoadCaseType.ReferenceLoad })
            {
                var lc = new LoadCase(17, "case", type);
                var nodes = load.GetNodalLoads(lc, 10);
                var udls = load.GetMemberUniformlyDistributedLoads(lc, 101);
                var points = load.GetMemberConcentratedLoads(lc, 101);
                var moments = load.GetMemberConcentratedMoments(lc, 101);
                var linear = load.GetMemberLinearVaryingLoads(lc, 101);
                var trapezoidal = load.GetMemberTrapezoidalLoads(lc, 101);
                Check(nodes.Count == 1 && nodes[0].Forces.Fx == 1 && nodes[0].Forces.Fy == -2 && nodes[0].Forces.Mz == -6, "nodal mapping");
                Check(udls.Count == 1 && udls[0].Direction == LoadDirection.GlobalY && udls[0].Magnitude == -7.25 && udls[0].EPosition == 0 && udls[0].Eccentricity == -.5, "UDL mapping");
                Check(points.Count == 1 && points[0].Direction == LoadDirection.GlobalZ && points[0].Magnitude == -8.5 && points[0].Position == 2.5 && points[0].Eccentricity == -.25, "point mapping");
                Check(moments.Count == 1 && moments[0].Direction == LoadDirection.GlobalZ && moments[0].Magnitude == -9.75 && moments[0].Position == 2.25 && moments[0].Eccentricity == -.125, "moment mapping");
                Check(linear.Count == 1 && linear[0].Direction == LoadDirection.LocalY && linear[0].WStart == -11 && linear[0].WEnd == -22 && linear[0].WMiddle == -33, "start/end/middle mapping");
                Check(trapezoidal.Count == 1 && trapezoidal[0].Direction == LoadDirection.ProjectedX && trapezoidal[0].WStart == -4 && trapezoidal[0].WEnd == -12 && trapezoidal[0].SPosition == 1.5 && trapezoidal[0].EPosition == 3.25, "trapezoidal mapping");
                Check(ReferenceEquals(nodes[0].LoadCase, lc) && ReferenceEquals(udls[0].LoadCase, lc) && ReferenceEquals(points[0].LoadCase, lc), "case identity");
                Check(ReferenceEquals(moments[0].LoadCase, lc) && ReferenceEquals(linear[0].LoadCase, lc) && ReferenceEquals(trapezoidal[0].LoadCase, lc), "new query case identity");
                Check(fake.ActiveId == 17 && fake.Reference == (type == LoadCaseType.ReferenceLoad), "activation");
                // Exercise the new models through the installed package's existing assignment adapter.
                // The stand-in uses the same units for reading and writing; native units still need live checks.
                var beam = new Beam(101, new Node(0, 0, 0), new Node(5, 0, 0));
                load.AddMemberLoad(lc, beam, moments[0]);
                Check(fake.Assignment == "moment" && fake.AssignedValues[0] == -9.75 && fake.AssignedValues[1] == 2.25 && fake.AssignedValues[2] == -.125, "moment assignment");
                load.AddMemberLoad(lc, beam, linear[0]);
                Check(fake.Assignment == "linear" && fake.AssignedValues[0] == -11 && fake.AssignedValues[1] == -22 && fake.AssignedValues[2] == -33, "linear native assignment order");
                load.AddMemberLoad(lc, beam, trapezoidal[0]);
                Check(fake.Assignment == "trapezoidal" && fake.AssignedValues[0] == -4 && fake.AssignedValues[1] == -12 && fake.AssignedValues[2] == 1.5 && fake.AssignedValues[3] == 3.25, "trapezoidal assignment");
            }
            fake.Empty = true;
            Check(load.GetNodalLoads(new LoadCase(1), 10).Count == 0 && load.GetMemberUniformlyDistributedLoads(new LoadCase(1), 101).Count == 0 && load.GetMemberConcentratedLoads(new LoadCase(1), 101).Count == 0, "empty queries");
            Check(load.GetMemberConcentratedMoments(new LoadCase(1), 101).Count == 0 && load.GetMemberLinearVaryingLoads(new LoadCase(1), 101).Count == 0 && load.GetMemberTrapezoidalLoads(new LoadCase(1), 101).Count == 0, "empty new queries");
            Check(typeof(IOSLoad).Assembly.GetType("StaadPro.Interop.Services.ConnectivityService") == null, "connectivity rollback");
        }
        Console.WriteLine("PASS: twelve packaged XML examples, all six installed-package queries, and three new load models.");
    }
    private static void Check(bool success, string name) { if (!success) throw new Exception(name); }
    private static void RunDocumentationExamples(IOSLoad load)
    {
__EXAMPLES__
    }
    public class FakeRoot
    {
        public FakeRoot(FakeLoad load) { Load = load; }
        public FakeLoad Load { get; }
        public object Geometry => null;
    }
    public class FakeLoad
    {
        public int ActiveId { get; private set; }
        public bool Reference { get; private set; }
        public bool Empty { get; set; }
        public string Assignment { get; private set; }
        public double[] AssignedValues { get; private set; }
        public bool SetLoadActive(int id) { ActiveId = id; Reference = false; return true; }
        public int SetReferenceLoadActive(int id) { ActiveId = id; Reference = true; return id; }
        public int GetNodalLoadCount(int id) => Empty ? 0 : 1;
        public int GetUDLLoadCount(int id) => Empty ? 0 : 1;
        public int GetConcForceCount(int id) => Empty ? 0 : 1;
        public int GetConcMomentCount(int id) => Empty ? 0 : 1;
        public int GetLinearVaryingLoadCount(int id) => Empty ? 0 : 1;
        public int GetTrapLoadCount(int id) => Empty ? 0 : 1;
        public int GetNodalLoads(int id, ref object fx, ref object fy, ref object fz, ref object mx, ref object my, ref object mz)
        { fx = new[] { 1.0 }; fy = new[] { -2.0 }; fz = new[] { 3.0 }; mx = new[] { -4.0 }; my = new[] { 5.0 }; mz = new[] { -6.0 }; return 0; }
        public int GetUDLLoads(int id, ref object direction, ref object force, ref object start, ref object end, ref object offset)
        { direction = new[] { 5 }; force = new[] { -7.25 }; start = new[] { 0.0 }; end = new[] { 0.0 }; offset = new[] { -.5 }; return 0; }
        public int GetConcForces(int id, ref object direction, ref object force, ref object position, ref object offset)
        { direction = new[] { 6 }; force = new[] { -8.5 }; position = new[] { 2.5 }; offset = new[] { -.25 }; return 0; }
        public int GetConcMoments(int id, ref object direction, ref object moment, ref object position, ref object offset)
        { direction = new[] { 6 }; moment = new[] { -9.75 }; position = new[] { 2.25 }; offset = new[] { -.125 }; return 0; }
        public int GetLinearVaryingLoads(int id, ref object direction, ref object start, ref object end, ref object middle)
        { direction = new[] { 2 }; start = new[] { -11.0 }; end = new[] { -22.0 }; middle = new[] { -33.0 }; return 0; }
        public int GetTrapLoads(int id, ref object direction, ref object startForce, ref object endForce, ref object startPosition, ref object endPosition)
        { direction = new[] { 7 }; startForce = new[] { -4.0 }; endForce = new[] { -12.0 }; startPosition = new[] { 1.5 }; endPosition = new[] { 3.25 }; return 0; }
        public int AddMemberConcMoment(int[] ids, int direction, double moment, double position, double offset)
        { Assignment = "moment"; AssignedValues = new[] { moment, position, offset }; return 0; }
        public int AddMemberLinearVari(int[] ids, int direction, double start, double end, double middle)
        { Assignment = "linear"; AssignedValues = new[] { start, end, middle }; return 0; }
        public int AddMemberTrapezoidal(int[] ids, int direction, double start, double end, double startPosition, double endPosition)
        { Assignment = "trapezoidal"; AssignedValues = new[] { start, end, startPosition, endPosition }; return 0; }
    }
}
'@
$program = $program.Replace('__EXAMPLES__', ($snippets -join "`n"))
[IO.File]::WriteAllText((Join-Path $consumerRoot 'NuGet.Config'), $config)
[IO.File]::WriteAllText((Join-Path $consumerRoot 'Consumer.csproj'), $project)
[IO.File]::WriteAllText((Join-Path $consumerRoot 'Program.cs'), $program)

Write-Host "Verifying package $version in $consumerRoot"
& dotnet restore (Join-Path $consumerRoot 'Consumer.csproj') --configfile (Join-Path $consumerRoot 'NuGet.Config') --verbosity minimal
if ($LASTEXITCODE -ne 0) { throw 'Isolated consumer restore failed.' }
$restoredArchive = Join-Path $consumerRoot "packages/staadpro.interop/$version/staadpro.interop.$version.nupkg"
if ((Get-FileHash -LiteralPath $restoredArchive -Algorithm SHA256).Hash -ne (Get-FileHash -LiteralPath $PackagePath -Algorithm SHA256).Hash) { throw 'Consumer restored a different package archive.' }
& dotnet build (Join-Path $consumerRoot 'Consumer.csproj') -c Release --no-restore --verbosity minimal
if ($LASTEXITCODE -ne 0) { throw 'Isolated consumer compilation failed.' }
& (Join-Path $consumerRoot 'bin/Release/net481/Consumer.exe')
if ($LASTEXITCODE -ne 0) { throw 'Isolated consumer runtime checks failed.' }
Write-Host 'PASS: package metadata, XML IntelliSense, packaged guides, isolated restore/build/runtime.'
