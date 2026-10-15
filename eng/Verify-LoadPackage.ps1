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
    $installation = Read-PackageEntry 'docs/Installation.md'
    $publishing = Read-PackageEntry 'docs/Publishing.md'
    $releaseNotes = Read-PackageEntry 'docs/Release-Notes.md'
    $geometryGuide = Read-PackageEntry 'docs/Geometry-Porting.md'
    $loadPorting = Read-PackageEntry 'docs/Load-Porting.md'
    $rootPorting = Read-PackageEntry 'docs/Root-Porting.md'
    if ($installation -notmatch 'net481' -or $publishing -notmatch 'nuget.org') { throw 'Incomplete installation/publishing guides.' }
    $installationCode = [regex]::Match($installation, '(?s)```csharp\s*\r?\n(.*?)```').Groups[1].Value
    if ([string]::IsNullOrWhiteSpace($installationCode)) { throw 'Missing installation C# example.' }
    if ($readme -notmatch 'net481') { throw 'Incomplete packaged README.' }
    $queryMethods = @('GetNodalLoads', 'GetMemberUniformlyDistributedLoads', 'GetMemberConcentratedLoads', 'GetMemberConcentratedMoments', 'GetMemberLinearVaryingLoads', 'GetMemberTrapezoidalLoads', 'GetAllPrimaryLoadCases', 'GetAllReferenceLoadCases', 'GetMemberUniformMoments', 'GetPrimaryLoadCaseFromId', 'GetReferenceLoadCaseFromId', 'GetPrimaryLoadCasesFromIds', 'GetReferenceLoadCasesFromIds', 'GetAllLoadCombinationCases', 'GetLoadCombinationCaseFromId', 'GetLoadCombinationCasesFromIds', 'GetAllLoadCasesOfType', 'GetLoadCase', 'GetLoadCases', 'GetLoadCasesFromIds', 'GetLoadCasesOfType')
    $queryMethods += @('GetEnvelopeIds', 'GetLoadEnvelopeDetail', 'GetAllLoadEnvelopeDetails')
    foreach ($method in $queryMethods) {
        if ($readme -notmatch $method -or $guide -notmatch $method) { throw "Missing packaged guidance for $method" }
    }
    $nuspecEntry = @($archive.Entries | Where-Object FullName -like '*.nuspec')
    if ($nuspecEntry.Count -ne 1) { throw 'Expected exactly one nuspec.' }
    [xml] $nuspec = Read-PackageEntry $nuspecEntry[0].FullName
    $metadata = $nuspec.package.metadata
    if ($metadata.id -ne 'StaadPro.Interop' -or $metadata.license.InnerText -ne 'MIT' -or $metadata.readme -ne 'README.md') { throw 'Invalid package identity, license or README metadata.' }
    if ($metadata.repository.url -ne 'https://github.com/structuraldeveloper/StaadPro.Interop') { throw 'Missing repository metadata.' }
    if ([string]$metadata.authors -ne 'Srinivasa Rao Masanam') { throw 'Incorrect package author.' }
    if ([string]$metadata.copyright -ne 'Copyright (c) 2026 StaadDev') { throw 'Incorrect package copyright.' }
    if ([string]$metadata.description -ne 'A strongly-typed .NET Framework wrapper for Bentley STAAD.Pro OpenSTAAD') { throw 'Incorrect package description.' }
    if ([string]::IsNullOrWhiteSpace([string]$metadata.releaseNotes)) { throw 'Missing release notes in package metadata.' }
    if ($releaseNotes -notmatch '1.0.0-preview.20261012' -or $releaseNotes -notmatch 'Srinivasa Rao Masanam' -or $releaseNotes -notmatch 'GetLoadCasesFromIds') { throw 'Incomplete packaged release notes.' }
    $version = [string]$metadata.version
    $readmeVersion = [regex]::Match($readme, '(?m)^## Version ([^\r\n]+)').Groups[1].Value.Trim()
    $releaseVersion = [regex]::Match($releaseNotes, '(?m)^## ([^ \r\n]+)').Groups[1].Value
    if ($readmeVersion -ne $version -or $releaseVersion -ne $version) { throw 'Packaged README/release-note versions do not match the archive.' }
    $snippets = @()
    foreach ($owner in @('Adapters.Interfaces.IOSLoad', 'Adapters.Models.OSLoadAdapter')) {
        foreach ($method in $queryMethods) {
            $isEnumeration = $method -in @('GetAllPrimaryLoadCases', 'GetAllReferenceLoadCases', 'GetAllLoadCombinationCases', 'GetEnvelopeIds', 'GetAllLoadEnvelopeDetails')
            $isSingleLookup = $method -in @('GetPrimaryLoadCaseFromId', 'GetReferenceLoadCaseFromId', 'GetLoadCombinationCaseFromId', 'GetLoadEnvelopeDetail')
            $isBatchLookup = $method -in @('GetPrimaryLoadCasesFromIds', 'GetReferenceLoadCasesFromIds', 'GetLoadCombinationCasesFromIds')
            $isFamilyQuery = $method -in @('GetAllLoadCasesOfType', 'GetLoadCasesOfType')
            $isGenericLookup = $method -eq 'GetLoadCase'
            $isRangeQuery = $method -eq 'GetLoadCases'
            $isIdFilter = $method -eq 'GetLoadCasesFromIds'
            $suffix = if ($isEnumeration) { '' } elseif ($isFamilyQuery) { '(StaadPro.Interop.Enums.LoadCaseType)' } elseif ($isRangeQuery) { '(System.Int32,System.Int32,StaadPro.Interop.Enums.LoadCaseType)' } elseif ($isIdFilter) { '(System.Collections.Generic.IEnumerable{System.Int32},StaadPro.Interop.Enums.LoadCaseType)' } elseif ($isGenericLookup) { '(System.Int32,StaadPro.Interop.Enums.LoadCaseType)' } elseif ($isSingleLookup) { '(System.Int32)' } elseif ($isBatchLookup) { '(System.Collections.Generic.IEnumerable{System.Int32})' } else { '(StaadPro.Interop.Entities.ILoadCase,System.Int32)' }
            $name = "M:StaadPro.Interop.$owner.$method$suffix"
            $members = @($xml.doc.members.member | Where-Object name -eq $name)
            if ($members.Count -ne 1) { throw "Missing or duplicate IntelliSense entry: $name" }
            $member = $members[0]
            foreach ($tag in @('summary','returns','remarks','example/code')) {
                $node = $member.SelectSingleNode($tag)
                if ($null -eq $node -or [string]::IsNullOrWhiteSpace($node.InnerText)) { throw "Incomplete $tag for $name" }
            }
            $expectedParams = if ($isEnumeration) { 0 } elseif ($isRangeQuery) { 3 } elseif ($isSingleLookup -or $isBatchLookup -or $isFamilyQuery) { 1 } else { 2 }
            $expectedExceptions = if ($isEnumeration) { 3 } elseif ($isSingleLookup -or $isFamilyQuery) { 4 } elseif ($isBatchLookup -or $isGenericLookup) { 5 } else { 6 }
            if (@($member.SelectNodes('param')).Count -ne $expectedParams -or @($member.exception).Count -ne $expectedExceptions -or $null -ne $member.SelectSingleNode('.//inheritdoc')) { throw "Incomplete contract for $name" }
            if ($member.OuterXml -match 'cref="!:') { throw "Unresolved documentation reference in $name" }
            $snippets += "{`n" + $member.SelectSingleNode('example/code').InnerText + "`n}"
        }
    }
    foreach ($typeName in @('MemberConcentratedMoment', 'MemberLinearVaryingLoad', 'MemberTrapezoidalLoad', 'MemberUniformMoment')) {
        $member = @($xml.doc.members.member | Where-Object name -eq "T:StaadPro.Interop.Entities.$typeName")
        if ($member.Count -ne 1 -or [string]::IsNullOrWhiteSpace($member[0].SelectSingleNode('summary').InnerText)) { throw "Missing model documentation: $typeName" }
    }
    $geometryMethods = @('SegregateBeamsByConnectivity', 'SegregateMembersByConnectivity', 'SegregatePlatesByConnectivity')
    foreach ($owner in @('Adapters.Interfaces.IOSGeometry', 'Adapters.Models.OSGeometryAdapter', 'Services.ConnectivityService')) {
        foreach ($method in $geometryMethods) {
            $prefix = "M:StaadPro.Interop.$owner.$method("
            $member = @($xml.doc.members.member | Where-Object { $_.name.StartsWith($prefix) })
            if ($member.Count -ne 1) { throw "Missing geometry IntelliSense: $owner.$method" }
            foreach ($tag in @('summary','returns','remarks','example/code')) {
                $node = $member[0].SelectSingleNode($tag)
                if ($null -eq $node -or [string]::IsNullOrWhiteSpace($node.InnerText)) { throw "Incomplete $tag for $owner.$method" }
            }
            $snippets += "{`n" + $member[0].SelectSingleNode('example/code').InnerText + "`n}"
        }
    }
    foreach ($owner in @('Adapters.Interfaces.IOSGeometry', 'Adapters.Models.OSGeometryAdapter', 'Adapters.Interfaces.IOSLoad', 'Adapters.Models.OSLoadAdapter')) {
        $methods = if ($owner -match 'Geometry') {
            @('CreateMultipleBeamsExtended', 'CreateMultipleNodesExtended', 'MergeNodesExtended')
        } else { @('AddReferenceLoadsTo', 'AddRepeatLoadsTo', 'AddAxialThermalGradientTo', 'AddMemberLoadsFromGroupsByLoadCase', 'AddNodalLoadsFromGroupsByLoadCase', 'AddSelfweightInXYZTo', 'CreateLoadEnvelopExt', 'CreateLoadList', 'CreateLoadCombinations', 'GetLoadCombination', 'GetMemberLoadsGroupedByLoadCase', 'GetNodalLoadsGroupedByLoadCase', 'GetRepeatLoadCases', 'PopulateMemberConcentratedLoads', 'PopulateMemberConcentratedMoments', 'PopulateMemberLinearVaryingLoads', 'PopulateMemberLoads', 'PopulateMemberTrapezoidalLoads') }
        foreach ($method in $methods) {
            $prefix = "M:StaadPro.Interop.$owner.$method"
            $member = @($xml.doc.members.member | Where-Object { $_.name -eq $prefix -or $_.name.StartsWith($prefix + "(") })
            $expectedOverloads = if ($method -in @('AddAxialThermalGradientTo','AddSelfweightInXYZTo','CreateLoadEnvelopExt')) { 2 } else { 1 }
            if ($member.Count -ne $expectedOverloads) { throw "Missing new mutation IntelliSense: $owner.$method" }
            foreach ($entry in $member) {
                foreach ($tag in @('summary','returns','remarks','example/code')) {
                    $node = $entry.SelectSingleNode($tag)
                    if ($null -eq $node -or [string]::IsNullOrWhiteSpace($node.InnerText)) { throw "Incomplete $tag for $($entry.name)" }
                }
                $snippets += "{`n" + $entry.SelectSingleNode('example/code').InnerText + "`n}"
            }
            $documentation = if ($owner -match 'Geometry') { $geometryGuide } else { $loadPorting }
            if ($documentation -notmatch $method -or $readme -notmatch $method) { throw "Missing packaged guidance for $method" }
        }
    }
    foreach ($owner in @('Adapters.Interfaces.IOSRoot', 'Adapters.Models.OSRootAdapter')) {
        $rootEntries = @($xml.doc.members.member | Where-Object { $_.name.StartsWith("M:StaadPro.Interop.$owner.") -and $_.name -notmatch '\.#ctor' })
        foreach ($rootEntry in $rootEntries) {
            $method = ($rootEntry.name.Split('(')[0]).Substring(("M:StaadPro.Interop.$owner.").Length)
            $entry = @($rootEntry)
            if ($entry.Count -ne 1) { throw "Missing root IntelliSense: $owner.$method" }
            foreach ($tag in @('summary','returns','remarks','example/code')) {
                if ($tag -eq 'returns' -and $method -in @('CloseCurrentStaadModel','MinimizeStaadMainWindow','MaximizeStaadMainWindow','RestoreStaadMainWindow','SetInputUnitForLength','SetInputUnitForForce','SetShortJobInfo','UpdateStructure','SetFullJobInfo','Analyze','AnalyzeModel','NewSTAADFile','CreateNamedView','SaveNamedView','RemoveNamedView','ModifyNamedView','Quit')) { continue }
                $node = $entry[0].SelectSingleNode($tag)
                if ($null -eq $node -or [string]::IsNullOrWhiteSpace($node.InnerText)) { throw "Incomplete $tag for $owner.$method" }
            }
            # A current-model status example requires a named fixture after earlier close examples.
            $exampleSetup = if ($method -eq 'GetAnalysisStatus') { "((FakeRoot)(object)root.ComObject).Filename = @`"C:\Models\Frame.v2.STD`";`n" } else { '' }
            $snippets += "{`n" + $exampleSetup + $entry[0].SelectSingleNode('example/code').InnerText + "`n}"
            if ($rootPorting -notmatch $method -or $readme -notmatch $method) { throw "Missing packaged root guidance for $method" }
        }
    }
    foreach ($owner in @('Adapters.Interfaces.IOSGeometry', 'Adapters.Models.OSGeometryAdapter', 'Adapters.Interfaces.IOSLoad', 'Adapters.Models.OSLoadAdapter', 'Adapters.Interfaces.IOSRoot', 'Adapters.Models.OSRootAdapter')) {
        $entries = @($xml.doc.members.member | Where-Object { $_.name.StartsWith("M:StaadPro.Interop.$owner.") -and $_.name -notmatch '\.#ctor' })
        $expectedCount = if ($owner -match 'Geometry') { 140 } elseif ($owner -match 'Root') { 53 } else { 78 }
        if ($entries.Count -ne $expectedCount) { throw "Unexpected documented method count for $owner" }
        foreach ($entry in $entries) {
            foreach ($tag in @('summary','remarks','returns')) {
                if ($tag -eq 'returns' -and $entry.name -match '\.(CloseCurrentStaadModel|MinimizeStaadMainWindow|MaximizeStaadMainWindow|RestoreStaadMainWindow|SetInputUnitForLength|SetInputUnitForForce|SetShortJobInfo|UpdateStructure|SetFullJobInfo|Analyze|AnalyzeModel|NewSTAADFile|CreateNamedView|SaveNamedView|RemoveNamedView|ModifyNamedView|Quit)(\(|$)') { continue }
                $node = $entry.SelectSingleNode($tag)
                if ($null -eq $node -or [string]::IsNullOrWhiteSpace($node.InnerText)) { throw "Incomplete $tag for $($entry.name)" }
            }
        }
    }
    foreach ($owner in @('Adapters.Interfaces.Capabilities.IOSRootCapabilities',
        'Adapters.Interfaces.Capabilities.IOSRootAdinaExport',
        'Adapters.Interfaces.Capabilities.IOSRootAnalysisErrorMessages',
        'Adapters.Interfaces.Capabilities.IOSRootAnalysisWarningMessages')) {
        $entries = @($xml.doc.members.member | Where-Object { $_.name.StartsWith("M:StaadPro.Interop.$owner.") })
        $expected = if ($owner.EndsWith('IOSRootCapabilities')) { 3 } else { 1 }
        if ($entries.Count -ne $expected) { throw "Unexpected optional Root method count for $owner" }
        foreach ($entry in $entries) {
            foreach ($tag in @('summary','remarks','returns','example/code')) {
                $node = $entry.SelectSingleNode($tag)
                if ($null -eq $node -or [string]::IsNullOrWhiteSpace($node.InnerText)) { throw "Incomplete $tag for $($entry.name)" }
            }
            $snippets += "{" + [Environment]::NewLine + $entry.SelectSingleNode('example/code').InnerText + [Environment]::NewLine + "}"
        }
    }
    if ($xml.OuterXml -match 'cref="!:|<inheritdoc|<include') { throw 'Unresolved documentation in installed package XML.' }
    if ($snippets.Count -ne 217) { throw "Expected 217 packaged XML examples; found $($snippets.Count)." }
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
  <PropertyGroup><TargetFramework>net481</TargetFramework><OutputType>Exe</OutputType><StartupObject>Program</StartupObject><LangVersion>latest</LangVersion><TreatWarningsAsErrors>true</TreatWarningsAsErrors><RestorePackagesPath>packages</RestorePackagesPath></PropertyGroup>
  <ItemGroup><Reference Include="Microsoft.CSharp"/><PackageReference Include="StaadPro.Interop" Version="$version"/></ItemGroup>
</Project>
"@
$program = @'
using System;
using System.Linq;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

public static class Program
{
    public static void Main()
    {
        var copyright = (System.Reflection.AssemblyCopyrightAttribute)typeof(OpenStaadWrapper).Assembly
            .GetCustomAttributes(typeof(System.Reflection.AssemblyCopyrightAttribute), false)[0];
        Check(copyright.Copyright == "Copyright (c) 2026 StaadDev", "installed assembly copyright");
        var fake = new FakeLoad();
        using (var wrapper = new OpenStaadWrapper(new FakeRoot(fake)))
        {
            IOSLoad load = wrapper.Load;
            RunDocumentationExamples(load, wrapper.Geometry, wrapper.Root);
            ((FakeRoot)wrapper.RawOpenStaad).Filename = @"C:\Models\Frame.v2.STD";
            Check(wrapper.Root.GetStaadFileFullPath() == @"C:\Models\Frame.v2.STD" && wrapper.Root.GetStaadFileNameOnly() == "Frame.v2.STD" && wrapper.Root.GetAnlFileFullPath() == @"C:\Models\Frame.v2.ANL", "root paths from installed package");
            Check(wrapper.Root.GetOpenedStaadFilename() == @"C:\Models\Frame.v2.STD", "normalized native model path");
            Check(wrapper.Root.IsTarget(@"c:\Models\sub\..\frame.v2.std") && wrapper.Root.ModelIsOpen(@"C:\Models\Frame.v2.STD"), "normalized full-path model identity");
            Check(!wrapper.Root.ModelIsOpen(@"D:\Models\Frame.v2.STD"), "same basename in different folder is not identity");
            var nativeRoot = (FakeRoot)wrapper.RawOpenStaad;
            nativeRoot.Filename = string.Empty;
            Check(wrapper.Root.GetOpenedStaadFilename() == string.Empty && !wrapper.Root.IsTarget(@"C:\Models\Frame.v2.STD") && !wrapper.Root.ModelIsOpen(@"C:\Models\Frame.v2.STD"), "unnamed native session identity");
            nativeRoot.Filename = @"C:\Models\Frame.v2.STD";
            var shortJob = wrapper.Root.GetShortJobInfo();
            var fullJob = wrapper.Root.GetFullJobInfo();
            Check(shortJob.Name == "JOB" && shortJob.Client == "CLIENT" && shortJob.Originator == "ENGINEER", "short job field mapping");
            Check(fullJob.Number == "NUMBER" && fullJob.Reference == "REFERENCE" && fullJob.Checker == "CHECKER" && fullJob.Approver == "APPROVER" && fullJob.Comments == "COMMENTS", "full job thirteen-field mapping");
            Check(fullJob.Originated == new DateTime(2026, 10, 13) && fullJob.Checked == new DateTime(2026, 10, 12) && fullJob.Approved == DateTime.MinValue, "full job date parsing and unspecified date");
            Check(ReferenceEquals(wrapper.Root.SetInputUnitsEx(LengthInputUnit.MilliMeter, ForceInputUnit.Newton), wrapper.Root) && nativeRoot.LengthCode == 5 && nativeRoot.ForceCode == 4, "native unit codes and fluent result");
            nativeRoot.SetSilentMode(0);
            Check(wrapper.Root.SetSilentMode(SilentMode.Enable) && nativeRoot.SilentFlag == 1, "silent-mode previous zero is not failure");
            Check(ReferenceEquals(wrapper.Root.SaveModel(SilentMode.Disable), wrapper.Root) && nativeRoot.SaveFlag == 0, "save integer code and fluent result");
            Check(ReferenceEquals(wrapper.Root.CloseSTAADFile(), wrapper.Root) && nativeRoot.CloseCount > 0, "native close and fluent result");
            string rewritePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "StaadRootConsumer-" + Guid.NewGuid().ToString("N") + ".std");
            try
            {
                System.IO.File.WriteAllText(rewritePath, "original");
                nativeRoot.Filename = rewritePath;
                Check(wrapper.Root.CloseWriteReopenStaadFile(rewritePath, new System.Collections.Generic.List<string> { "STAAD SPACE", "FINISH" }), "controlled file rewrite and reopen request");
                Check(System.IO.File.ReadAllLines(rewritePath).SequenceEqual(new[] { "STAAD SPACE", "FINISH" }) && nativeRoot.Filename == rewritePath, "rewritten contents and requested target");
            }
            finally { System.IO.File.Delete(rewritePath); nativeRoot.Filename = @"C:\Models\Frame.v2.STD"; }
            load.AddSelfweightInXYZTo(17, LoadCaseType.ReferenceLoad, new[] { 101, 102 }, SelfWeightDirection.GlobalZ, -1.25);
            Check(fake.Assignment == "selfweight" && fake.AssignedIds.SequenceEqual(new[] { 101, 102 }) && fake.AssignedValues[0] == -1.25 && fake.Reference && fake.Direction == 3, "self-weight native arguments and family");
            load.CreateLoadEnvelopExt(7, LoadEnvelopType.Strength, new[] { 17, 41 });
            Check(fake.Assignment == "envelope" && fake.CreatedEnvelopeId == 7 && fake.Direction == 5 && fake.AssignedIds.SequenceEqual(new[] { 17, 41 }), "envelope creation arguments");
            object listStatus = load.CreateLoadList(LoadListType.EnvelopList, new[] { 7 });
            Check(listStatus is int && (int)listStatus == 0 && fake.Assignment == "list" && fake.Direction == 1 && fake.AssignedIds.SequenceEqual(new[] { 7 }), "native integer-zero load-list contract");
            int envelopeActiveId = fake.ActiveId;
            bool envelopeReference = fake.Reference;
            int envelopeCountCalls = fake.EnvelopeCountCalls;
            int envelopeIdCalls = fake.EnvelopeIdCalls;
            var envelopes = load.GetAllLoadEnvelopeDetails();
            Check(envelopes.Count == 2 && fake.EnvelopeCountCalls == envelopeCountCalls + 1 && fake.EnvelopeIdCalls == envelopeIdCalls + 1, "envelope enumeration reads IDs once");
            var strength = envelopes.Single(e => e.Id == 7);
            Check(strength is LoadEnvelopeDetail && strength.EnvelopType == LoadEnvelopType.Strength && strength.LoadCasesCount == 2 && strength.LoadCaseIds.SetEquals(new[] { 17, 41 }), "envelope type, count and membership mapping");
            var emptyEnvelope = envelopes.Single(e => e.Id == 19);
            Check(emptyEnvelope.EnvelopType == LoadEnvelopType.None && emptyEnvelope.LoadCasesCount == 0 && emptyEnvelope.LoadCaseIds.Count == 0, "valid zero-member envelope");
            var envelopeCopy = load.GetLoadEnvelopeDetail(7);
            strength.LoadCaseIds.Clear();
            Check(envelopeCopy.LoadCaseIds.SetEquals(new[] { 17, 41 }) && !ReferenceEquals(strength, envelopeCopy), "independent envelope metadata");
            Check(fake.ActiveId == envelopeActiveId && fake.Reference == envelopeReference, "envelope queries preserve active state");
            Check(wrapper.Root != null && ReferenceEquals((object)wrapper.Root.ComObject, wrapper.RawOpenStaad), "root binding");
            var geometry = ((FakeRoot)wrapper.RawOpenStaad).Geometry;
            wrapper.Geometry.CreateMultipleNodesExtended(wrapper.Root, new[] { new Node(901, 1.25, -2.5, 3.75), new Node(902, 4, 5, 6) });
            Check(geometry.Ids.SequenceEqual(new[] { 901, 902 }) && geometry.Coordinates.GetLength(0) == 2 && geometry.Coordinates.GetLength(1) == 3 && geometry.Coordinates[0, 0] == 1.25 && geometry.Coordinates[0, 1] == -2.5 && geometry.Coordinates[0, 2] == 3.75, "bulk node matrix");
            wrapper.Geometry.CreateMultipleBeamsExtended(wrapper.Root, new[] { new Beam(903, new Node(901, 0, 0, 0), new Node(902, 0, 0, 0)) });
            Check(geometry.BeamId == 903 && geometry.StartId == 901 && geometry.EndId == 902, "explicit beam incidence");
            wrapper.Geometry.MergeNodesExtended(wrapper.Root, new Node(901, 0, 0, 0), new[] { new Node(902, 0, 0, 0), new Node(901, 0, 0, 0) });
            Check(geometry.RetainId == 901 && geometry.Ids.SequenceEqual(new[] { 902, 901 }), "retained ID inclusion");
            load.AddReferenceLoadsTo(17, new System.Collections.Generic.Dictionary<int, double> { { 41, -1.25 }, { 42, 0 } });
            Check(fake.Assignment == "reference" && fake.AssignedIds.SequenceEqual(new[] { 41, 42 }) && fake.AssignedValues.SequenceEqual(new[] { -1.25, 0.0 }) && fake.ActiveId == 17 && !fake.Reference, "reference load factors");
            load.AddRepeatLoadsTo(17, new System.Collections.Generic.Dictionary<int, double> { { 41, 1.123456789123 } });
            Check(fake.Assignment == "repeat" && fake.AssignedIds[0] == 41 && fake.AssignedValues[0] == 1.123456789123, "repeat load precision");
            load.AddAxialThermalGradientTo(17, new[] { 101, 102 }, -15.5);
            Check(fake.Assignment == "temperature" && fake.AssignedIds.SequenceEqual(new[] { 101, 102 }) && fake.AssignedValues.SequenceEqual(new[] { -15.5, 0.0, 0.0 }), "temperature native arguments");
            fake.ScalarTemperatureIds.Clear();
            fake.TemperatureThreadIds.Clear();
            load.AddAxialThermalGradientTo(17, new[] { 103, 101, 103 }, -12.25, nThreads: 4);
            Check(fake.ScalarTemperatureIds.SequenceEqual(new[] { 103, 101, 103 }) && fake.TemperatureThreadIds.All(id => id == System.Threading.Thread.CurrentThread.ManagedThreadId), "thermal scalar order and caller thread");
            Check(fake.AssignedValues.SequenceEqual(new[] { -12.25, 0.0, 0.0 }) && fake.ActiveId == 17 && !fake.Reference, "thermal overload values and activation");
            var memberGroups = new System.Collections.Generic.Dictionary<ILoadCase, System.Collections.Generic.List<(MemberLoad Load, System.Collections.Generic.List<Beam> Beams)>>
            {
                { new LoadCase(41, "REF", LoadCaseType.ReferenceLoad), new System.Collections.Generic.List<(MemberLoad Load, System.Collections.Generic.List<Beam> Beams)>
                    { (new MemberConcentratedLoad(LoadDirection.GlobalZ, -11.25, 1.75, -.5) { LoadCase = new LoadCase(999) }, new System.Collections.Generic.List<Beam> { new Beam(101, new Node(1, 0, 0, 0), new Node(2, 5, 0, 0)) }) } }
            };
            load.AddMemberLoadsFromGroupsByLoadCase(memberGroups);
            Check(fake.Assignment == "pointforce" && fake.ActiveId == 41 && fake.Reference && fake.AssignedIds[0] == 101 && fake.AssignedValues.SequenceEqual(new[] { -11.25, 1.75, -.5 }), "grouped member mapping and reference routing");
            var nodalGroups = new System.Collections.Generic.Dictionary<ILoadCase, System.Collections.Generic.List<(NodalLoad Load, System.Collections.Generic.List<Node> Nodes)>>
            {
                { new LoadCase(17), new System.Collections.Generic.List<(NodalLoad Load, System.Collections.Generic.List<Node> Nodes)>
                    { (new NodalLoad(1, -2, 3, -4, 5, -6), new System.Collections.Generic.List<Node> { new Node(10, 0, 0, 0), null, new Node(10, 0, 0, 0) }) } }
            };
            load.AddNodalLoadsFromGroupsByLoadCase(nodalGroups);
            Check(fake.Assignment == "nodal" && fake.ActiveId == 17 && !fake.Reference && fake.AssignedIds.SequenceEqual(new[] { 10, 10 }) && fake.AssignedValues.SequenceEqual(new[] { 1.0, -2.0, 3.0, -4.0, 5.0, -6.0 }), "grouped nodal mapping and primary routing");
            foreach (var type in new[] { LoadCaseType.PrimaryLoad, LoadCaseType.ReferenceLoad })
            {
                var lc = new LoadCase(17, "case", type);
                var nodes = load.GetNodalLoads(lc, 10);
                var udls = load.GetMemberUniformlyDistributedLoads(lc, 101);
                var points = load.GetMemberConcentratedLoads(lc, 101);
                var moments = load.GetMemberConcentratedMoments(lc, 101);
                var linear = load.GetMemberLinearVaryingLoads(lc, 101);
                var trapezoidal = load.GetMemberTrapezoidalLoads(lc, 101);
                var uniformMoments = load.GetMemberUniformMoments(lc, 101);
                Check(uniformMoments.Count == 1 && uniformMoments[0].Direction == LoadDirection.ProjectedZ && uniformMoments[0].Magnitude == -13.5 && uniformMoments[0].SPosition == 1.25 && uniformMoments[0].EPosition == 0 && uniformMoments[0].Eccentricity == -.75, "uniform moment mapping");
                Check(ReferenceEquals(uniformMoments[0].LoadCase, lc), "uniform moment case identity");
                var primaryCases = load.GetAllPrimaryLoadCases();
                var referenceCases = load.GetAllReferenceLoadCases();
                Check(primaryCases.Count == 1 && referenceCases.Count == 1, "case enumeration count");
                foreach (var item in primaryCases) Check(item.Id == 17 && item.Title == "PRIMARY" && item.Type == LoadType.Dead && item.CaseType == LoadCaseType.PrimaryLoad, "primary metadata");
                foreach (var item in referenceCases) Check(item.Id == 17 && item.Title == "REFERENCE" && item.Type == LoadType.Mass && item.CaseType == LoadCaseType.ReferenceLoad, "reference metadata");
                Check(fake.ActiveId == 17 && fake.Reference == (type == LoadCaseType.ReferenceLoad), "enumeration preserves activation");
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
                load.AddMemberLoad(lc, beam, uniformMoments[0]);
                Check(fake.Assignment == "uniformmoment" && fake.AssignedValues[0] == -13.5 && fake.AssignedValues[1] == 1.25 && fake.AssignedValues[2] == 0 && fake.AssignedValues[3] == -.75, "uniform moment assignment");
                load.AddMemberLoad(lc, beam, trapezoidal[0]);
                Check(fake.Assignment == "trapezoidal" && fake.AssignedValues[0] == -4 && fake.AssignedValues[1] == -12 && fake.AssignedValues[2] == 1.5 && fake.AssignedValues[3] == 3.25, "trapezoidal assignment");
            }
            int activeBeforeLookup = fake.ActiveId;
            bool referenceBeforeLookup = fake.Reference;
            var primaryLookup = load.GetPrimaryLoadCaseFromId(17);
            var referenceLookup = load.GetReferenceLoadCaseFromId(17);
            Check(primaryLookup.Id == 17 && primaryLookup.Title == "PRIMARY" && primaryLookup.Type == LoadType.Dead && primaryLookup.CaseType == LoadCaseType.PrimaryLoad, "primary lookup");
            Check(referenceLookup.Id == 17 && referenceLookup.Title == "REFERENCE" && referenceLookup.Type == LoadType.Mass && referenceLookup.CaseType == LoadCaseType.ReferenceLoad, "reference lookup");
            var batchLookup = load.GetPrimaryLoadCasesFromIds(new[] { 41, 17, 41 });
            Check(batchLookup.Count == 3 && batchLookup[0].Id == 41 && batchLookup[1].Id == 17 && batchLookup[2].Id == 41, "ordered batch lookup");
            Check(!ReferenceEquals(batchLookup[0], batchLookup[2]) && !ReferenceEquals(batchLookup[1], primaryLookup), "independent lookup records");
            foreach (var item in batchLookup) Check(item.Title == "PRIMARY" && item.Type == LoadType.Dead && item.CaseType == LoadCaseType.PrimaryLoad, "batch metadata");
            Check(load.GetPrimaryLoadCasesFromIds(new int[0]).Count == 0, "empty batch lookup");
            var referenceBatch = load.GetReferenceLoadCasesFromIds(new[] { 41, 17, 41 });
            Check(referenceBatch.Count == 3 && referenceBatch[0].Id == 41 && referenceBatch[1].Id == 17 && referenceBatch[2].Id == 41 && !ReferenceEquals(referenceBatch[0], referenceBatch[2]), "ordered independent reference batch");
            foreach (var item in referenceBatch) Check(item.Title == "REFERENCE" && item.Type == LoadType.Mass && item.CaseType == LoadCaseType.ReferenceLoad, "reference batch metadata");
            Check(load.GetReferenceLoadCasesFromIds(new int[0]).Count == 0, "empty reference batch");
            int typeCallsBeforeCombinations = fake.PrimaryTypeCalls;
            var combinations = load.GetAllLoadCombinationCases();
            Check(combinations.Count == 2, "combination enumeration count");
            foreach (var item in combinations) Check((item.Id == 201 || item.Id == 301) && item.Title == "COMB " + item.Id && item.Type == LoadType.None && item.CaseType == LoadCaseType.LoadCombination, "combination metadata");
            var combination = load.GetLoadCombinationCaseFromId(201);
            Check(combination.Id == 201 && combination.Title == "COMB 201" && combination.Type == LoadType.None && combination.CaseType == LoadCaseType.LoadCombination, "combination lookup");
            bool missingCombination = false;
            try { load.GetLoadCombinationCaseFromId(17); } catch (InvalidOperationException) { missingCombination = true; }
            Check(missingCombination && fake.PrimaryTypeCalls == typeCallsBeforeCombinations, "combination membership and no engineering type lookup");
            int numberCallsBeforeBatch = fake.CombinationNumberCalls;
            var combinationBatch = load.GetLoadCombinationCasesFromIds(new[] { 201, 301, 201 });
            int combinationBatchCount = 0;
            foreach (var item in combinationBatch)
            {
                combinationBatchCount++;
                Check((item.Id == 201 || item.Id == 301) && item.CaseType == LoadCaseType.LoadCombination && item.Type == LoadType.None && item.Title == "COMB " + item.Id, "combination batch metadata");
            }
            Check(combinationBatchCount == 2 && fake.CombinationNumberCalls == numberCallsBeforeBatch + 1, "distinct combination batch with one number fetch");
            foreach (var item in combinationBatch) Check(item.Id > 0, "combination batch re-enumeration");
            Check(fake.CombinationNumberCalls == numberCallsBeforeBatch + 1, "eager combination batch");
            foreach (var type in new[] { LoadCaseType.PrimaryLoad, LoadCaseType.ReferenceLoad, LoadCaseType.LoadCombination })
            {
                var familyCases = load.GetAllLoadCasesOfType(type);
                int familyCount = 0;
                foreach (var item in familyCases) { familyCount++; Check(item.CaseType == type, "family dispatch"); }
                Check(familyCount == (type == LoadCaseType.LoadCombination ? 2 : 1), "family count");
                var familyList = load.GetLoadCasesOfType(type);
                Check(familyList.Count == familyCount, "concrete family list");
                int id = type == LoadCaseType.LoadCombination ? 201 : 17;
                int rangeCount = 0;
                foreach (var item in load.GetLoadCases(id, id, type)) { rangeCount++; Check(item.Id == id && item.CaseType == type, "inclusive equal range"); }
                Check(rangeCount == 1, "equal range count");
                int filteredCount = 0;
                foreach (var item in load.GetLoadCasesFromIds(new[] { int.MaxValue, id, id }, type)) { filteredCount++; Check(item.Id == id && item.CaseType == type, "distinct existing family ID filter"); }
                Check(filteredCount == 1, "ID filter omits missing and duplicate requests");
                var found = load.GetLoadCase(id, type);
                Check(found != null && found.Id == id && found.CaseType == type, "generic family lookup");
                Check(load.GetLoadCase(int.MaxValue, type) == null, "absent generic lookup");
            }
            foreach (var item in load.GetAllLoadCasesOfType()) Check(item.CaseType == LoadCaseType.PrimaryLoad, "default family is primary");
            Check(load.GetLoadCase(17, LoadCaseType.LoadCombination) == null, "generic lookup does not cross families");
            Check(fake.ActiveId == activeBeforeLookup && fake.Reference == referenceBeforeLookup, "lookup preserves activation");
            fake.Empty = true;
            int emptyEnvelopeIdCalls = fake.EnvelopeIdCalls;
            Check(load.GetEnvelopeIds().Count == 0 && load.GetAllLoadEnvelopeDetails().Count == 0 && fake.EnvelopeIdCalls == emptyEnvelopeIdCalls, "zero envelope count skips IDs and details");
            Check(load.GetNodalLoads(new LoadCase(1), 10).Count == 0 && load.GetMemberUniformlyDistributedLoads(new LoadCase(1), 101).Count == 0 && load.GetMemberConcentratedLoads(new LoadCase(1), 101).Count == 0, "empty queries");
            Check(load.GetMemberConcentratedMoments(new LoadCase(1), 101).Count == 0 && load.GetMemberLinearVaryingLoads(new LoadCase(1), 101).Count == 0 && load.GetMemberTrapezoidalLoads(new LoadCase(1), 101).Count == 0, "empty new queries");
            Check(load.GetMemberUniformMoments(new LoadCase(1), 101).Count == 0 && load.GetAllPrimaryLoadCases().Count == 0 && load.GetAllReferenceLoadCases().Count == 0, "empty third increment");
            Check(load.GetAllLoadCombinationCases().Count == 0, "empty combination enumeration");
            bool emptyCombination = false;
            try { load.GetLoadCombinationCaseFromId(201); } catch (InvalidOperationException) { emptyCombination = true; }
            Check(emptyCombination, "lookup in empty combination set");
            foreach (var type in new[] { LoadCaseType.PrimaryLoad, LoadCaseType.ReferenceLoad, LoadCaseType.LoadCombination })
            {
                int count = 0;
                foreach (var item in load.GetAllLoadCasesOfType(type)) count++;
                Check(count == 0 && load.GetLoadCase(17, type) == null, "empty generic family queries");
                Check(load.GetLoadCasesOfType(type).Count == 0, "empty concrete family list");
                int emptyFilterCount = 0;
                foreach (var item in load.GetLoadCases(1, int.MaxValue, type)) emptyFilterCount++;
                foreach (var item in load.GetLoadCasesFromIds(new[] { 17 }, type)) emptyFilterCount++;
                Check(emptyFilterCount == 0, "empty family filters");
            }
            int emptyBatchCount = 0;
            foreach (var item in load.GetLoadCombinationCasesFromIds(new int[0])) emptyBatchCount++;
            Check(emptyBatchCount == 0, "empty combination batch");
            Check(typeof(IOSGeometry).GetMethod("SegregateBeamsByConnectivity") != null, "beam connectivity contract");
            Check(typeof(IOSGeometry).GetMethod("SegregateMembersByConnectivity").GetParameters()[1].DefaultValue.Equals(MemberConnectivityScope.EndpointsOnly), "member default scope");
            Check(typeof(IOSGeometry).GetMethod("SegregatePlatesByConnectivity").GetParameters()[1].DefaultValue.Equals(PlateConnectivityScope.SharedEdge), "plate default scope");
        }
        Console.WriteLine("PASS: 217 packaged XML examples and complete geometry/load/root IntelliSense.");
    }
    private static void Check(bool success, string name) { if (!success) throw new Exception(name); }
    private static void RunDocumentationExamples(IOSLoad load, IOSGeometry geometry, IOSRoot root)
    {
__EXAMPLES__
    }
    public class FakeRoot
    {
        public FakeRoot(FakeLoad load) { Load = load; }
        public FakeLoad Load { get; }
        public FakeGeometry Geometry { get; } = new FakeGeometry();
        public string Filename { get; set; } = @"C:\Models\Frame.v2.STD";
        public int LengthCode { get; private set; }
        public int ForceCode { get; private set; }
        public void SetInputUnits(int length, int force) { LengthCode = length; ForceCode = force; }
        public int SilentFlag { get; private set; }
        public int SaveFlag { get; private set; }
        public int CloseCount { get; private set; }
        public object SetSilentMode(int flag) { int previous = SilentFlag; SilentFlag = flag; return previous; }
        public void SaveModel(int flag) { SaveFlag = flag; }
        public void CloseSTAADFile() { CloseCount++; Filename = string.Empty; }
        public void OpenSTAADFile(string path) { Filename = path; }
        public void ShowApplication() { }
        public int AnalyzeEx(int silent, int hidden, int wait) { return 2; }
        public bool IsAnalyzing() { return false; }
        public int GetAnalysisStatus(string path, ref int warnings, ref int errors, ref double cpuSeconds)
        {
            warnings = 0; errors = 0; cpuSeconds = 0;
            return 2;
        }
        public void GetSTAADFileFolder(ref object folder) { folder = @"C:\Models\"; }
        public bool IsPhysicalModel() { return false; }
        public string GetErrorMessage() { return string.Empty; }
        public int GetProcessId() { return 1234; }
        public object GetProcessHandle() { return 1234L; }
        public object GetCONNECTEDProjectInfo(ref object projectId, ref object projectName)
        {
            if (!(projectId is string) || !(projectName is string)) throw new Exception("Expected BSTR project outputs.");
            projectId = "test-project-id"; projectName = "Test project";
            return true;
        }
        public void GetShortJobInfo(ref object name, ref object client, ref object engineer)
        {
            if (!(name is string) || !(client is string) || !(engineer is string)) throw new Exception("Expected BSTR job outputs.");
            name = "JOB"; client = "CLIENT"; engineer = "ENGINEER";
        }
        public void GetFullJobInfo(ref object name, ref object client, ref object engineer, ref object eDate,
            ref object number, ref object revision, ref object part, ref object reference, ref object checker,
            ref object cDate, ref object approver, ref object aDate, ref object comments)
        {
            if (!(name is string) || !(client is string) || !(engineer is string) || !(eDate is string) || !(number is string) || !(revision is string) || !(part is string) || !(reference is string) || !(checker is string) || !(cDate is string) || !(approver is string) || !(aDate is string) || !(comments is string)) throw new Exception("Expected thirteen BSTR job outputs.");
            name = "JOB"; client = "CLIENT"; engineer = "ENGINEER"; eDate = "2026-10-13";
            number = "NUMBER"; revision = "REVISION"; part = "PART"; reference = "REFERENCE";
            checker = "CHECKER"; cDate = "2026-10-12"; approver = "APPROVER"; aDate = ""; comments = "COMMENTS";
        }
        public void GetSTAADFile(ref object name, bool fullPath)
        {
            if (!fullPath) throw new Exception("Expected full-path native flag.");
            if (!(name is string)) throw new Exception("Expected a BSTR output buffer for native GetSTAADFile.");
            name = Filename;
        }
    }
    public class FakeGeometry
    {
        public int[] Ids { get; private set; }
        public double[,] Coordinates { get; private set; }
        public int BeamId { get; private set; }
        public int StartId { get; private set; }
        public int EndId { get; private set; }
        public int RetainId { get; private set; }
        public void CreateMultipleNodes(int[] ids, double[,] coordinates) { Ids = ids; Coordinates = coordinates; }
        public void CreateBeam(int id, int start, int end) { BeamId = id; StartId = start; EndId = end; }
        public void MergeNodes(int retain, int[] ids) { RetainId = retain; Ids = ids; }
    }
    public class FakeLoad
    {
        public int ActiveId { get; private set; }
        public bool Reference { get; private set; }
        public bool Empty { get; set; }
        public int PrimaryTypeCalls { get; private set; }
        public int CombinationNumberCalls { get; private set; }
        public int EnvelopeCountCalls { get; private set; }
        public int EnvelopeIdCalls { get; private set; }
        public int GetEnvelopeCount() { EnvelopeCountCalls++; return Empty ? 0 : 2; }
        public int GetEnvelopeIDs(ref object ids) { EnvelopeIdCalls++; ids = new[] { 7, 19 }; return 1; }
        public int GetLoadEnvelopeDetails(int id, ref int type, ref int count)
        { type = id == 7 ? 5 : 0; count = Empty || id == 19 ? 0 : 2; return 1; }
        public int GetLoadListfromLoadEnvelope(int id, ref object ids) { ids = new[] { 17, 41 }; return 1; }
        public string Assignment { get; private set; }
        public double[] AssignedValues { get; private set; }
        public int[] AssignedIds { get; private set; }
        public int GetRepeatLoadCount() => Empty ? 0 : 1;
        public int GetNoOfLoadAndFactorPairsForCombination(int id) => Empty ? 0 : 1;
        public bool GetLoadAndFactorForCombination(int id, ref object ids, ref object factors) { ids = new[] { 17 }; factors = new[] { -1.25 }; return true; }
        public int Direction { get; private set; }
        public int CreatedEnvelopeId { get; private set; }
        public bool AddSelfWeightInXYZToGeometry(int[] ids, int direction, double factor)
        { Assignment = "selfweight"; AssignedIds = ids; Direction = direction; AssignedValues = new[] { factor }; return true; }
        public bool CreateLoadEnvelop(int id, int type, int[] ids)
        { Assignment = "envelope"; CreatedEnvelopeId = id; Direction = type; AssignedIds = ids; return true; }
        public int CreateLoadList(int type, int[] ids)
        { Assignment = "list"; Direction = type; AssignedIds = ids; return 0; }
        public System.Collections.Generic.List<int> ScalarTemperatureIds { get; } = new System.Collections.Generic.List<int>();
        public System.Collections.Generic.List<int> TemperatureThreadIds { get; } = new System.Collections.Generic.List<int>();
        public void AddReferenceLoad(int[] ids, double[] factors) { Assignment = "reference"; AssignedIds = ids; AssignedValues = factors; }
        public void AddRepeatLoad(int[] ids, double[] factors) { Assignment = "repeat"; AssignedIds = ids; AssignedValues = factors; }
        public void AddTemperatureLoad(int[] ids, double axial, double y, double z) { Assignment = "temperature"; AssignedIds = ids; AssignedValues = new[] { axial, y, z }; }
        public void AddTemperatureLoad(int id, double axial, double y, double z)
        { Assignment = "temperature-scalar"; AssignedIds = new[] { id }; AssignedValues = new[] { axial, y, z }; ScalarTemperatureIds.Add(id); TemperatureThreadIds.Add(System.Threading.Thread.CurrentThread.ManagedThreadId); }
        public void AddMemberConcForce(int[] ids, int direction, double force, double position, double offset)
        { Assignment = "pointforce"; AssignedIds = ids; AssignedValues = new[] { force, position, offset }; }
        public void AddNodalLoad(int[] ids, double fx, double fy, double fz, double mx, double my, double mz)
        { Assignment = "nodal"; AssignedIds = ids; AssignedValues = new[] { fx, fy, fz, mx, my, mz }; }
        public void CreateNewLoadCombination(string title, int id) { Assignment = "combination"; }
        public void AddLoadAndFactorToCombination(int combinationId, int caseId, double factor) { AssignedIds = new[] { combinationId, caseId }; AssignedValues = new[] { factor }; }
        public bool SetLoadActive(int id) { ActiveId = id; Reference = false; return true; }
        public int SetReferenceLoadActive(int id) { ActiveId = id; Reference = true; return id; }
        public int GetNodalLoadCount(int id) => Empty ? 0 : 1;
        public int GetUDLLoadCount(int id) => Empty ? 0 : 1;
        public int GetConcForceCount(int id) => Empty ? 0 : 1;
        public int GetConcMomentCount(int id) => Empty ? 0 : 1;
        public int GetLinearVaryingLoadCount(int id) => Empty ? 0 : 1;
        public int GetTrapLoadCount(int id) => Empty ? 0 : 1;
        public int GetUNIMomentCount(int id) => Empty ? 0 : 1;
        public int GetPrimaryLoadCaseCount() => Empty ? 0 : 1;
        public int GetReferenceLoadCaseCount() => Empty ? 0 : 1;
        public int GetLoadCombinationCaseCount() => Empty ? 0 : 2;
        public int GetLoadCombinationCaseNumbers(ref object ids) { CombinationNumberCalls++; ids = new[] { 201, 301 }; return 2; }
        public int GetPrimaryLoadCaseNumbers(ref object ids) { ids = new[] { 17 }; return 1; }
        public int GetReferenceLoadCaseNumbers(ref object ids) { ids = new[] { 17 }; return 1; }
        public string GetLoadCaseTitle(int id) => id == 201 || id == 301 ? "COMB " + id : "PRIMARY";
        public string GetReferenceLoadCaseTitle(int id) => "REFERENCE";
        public int GetLoadType(int id) { PrimaryTypeCalls++; return (int)LoadType.Dead; }
        public int GetReferenceLoadType(int id) => (int)LoadType.Mass;
        public int GetUNIMoments(int id, ref object direction, ref object moment, ref object start, ref object end, ref object offset)
        { direction = new[] { 9 }; moment = new[] { -13.5 }; start = new[] { 1.25 }; end = new[] { 0.0 }; offset = new[] { -.75 }; return 0; }
        public int AddMemberUniformMoment(int[] ids, int direction, double moment, double start, double end, double offset)
        { Assignment = "uniformmoment"; AssignedValues = new[] { moment, start, end, offset }; return 0; }
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
[IO.File]::WriteAllText((Join-Path $consumerRoot 'InstallationExample.cs'), ('namespace GuideExamples {' + [Environment]::NewLine + $installationCode + [Environment]::NewLine + '}'))

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
