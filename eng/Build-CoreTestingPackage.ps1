param(
    [string] $Version = '1.0.0-preview.20261013.2'
)

$ErrorActionPreference = 'Stop'
if ($Version -notmatch '^\d+\.\d+\.\d+-[0-9A-Za-z]+(?:[.-][0-9A-Za-z]+)*$') {
    throw 'Use an explicit prerelease version for the testing package.'
}
$repositoryRoot = Split-Path -Parent $PSScriptRoot
Push-Location -LiteralPath $repositoryRoot
try {
    $commit = git rev-parse HEAD
    if ($LASTEXITCODE -ne 0) { throw 'Cannot determine the source commit.' }
    $changes = git status --porcelain
    if ($LASTEXITCODE -ne 0 -or $changes) { throw 'Commit source changes before building the release archive.' }
    $outputDirectory = Join-Path $repositoryRoot 'artifacts/core-testing'
    dotnet pack src/StaadPro.Interop/StaadPro.Interop.csproj -c Release `
        '-p:TargetFrameworks=net8.0-windows' '-p:BuildCoreTestingPackage=true' `
        "-p:PackageVersion=$Version" "-p:RepositoryCommit=$commit" `
        "-p:RepositoryBranch=$(git branch --show-current)" -o $outputDirectory
    if ($LASTEXITCODE -ne 0) { throw 'Test package build failed.' }

    $packagePath = Join-Path $outputDirectory "StaadPro.Interop.Core.Testing.$Version.nupkg"
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [IO.Compression.ZipFile]::OpenRead($packagePath)
    function Read-Entry([string] $name) {
        $entry = $archive.GetEntry($name)
        if ($null -eq $entry) { throw "Missing package entry: $name" }
        $reader = [IO.StreamReader]::new($entry.Open())
        try { return $reader.ReadToEnd() } finally { $reader.Dispose() }
    }
    try {
        [xml] $manifest = Read-Entry 'StaadPro.Interop.Core.Testing.nuspec'
        $metadata = $manifest.package.metadata
        if ($metadata.id -ne 'StaadPro.Interop.Core.Testing' -or $metadata.version -ne $Version) {
            throw 'Incorrect test package identity.'
        }
        if ($metadata.repository.commit -ne $commit) { throw 'Source commit metadata mismatch.' }
        if ($metadata.authors -ne 'Srinivasa Rao Masanam' -or $metadata.license.InnerText -ne 'MIT') {
            throw 'Incorrect author or license metadata.'
        }
        if ([string]::IsNullOrWhiteSpace([string] $metadata.copyright) -or
            [string] $metadata.releaseNotes -notmatch 'Testing-only') { throw 'Missing test release metadata.' }
        if ($null -eq $archive.GetEntry('lib/net8.0-windows7.0/StaadPro.Interop.dll')) {
            throw 'Missing Windows .NET 8 library.'
        }
        if (@($archive.Entries | Where-Object { $_.FullName -like 'lib/*/*.dll' }).Count -ne 1) {
            throw 'The testing package must contain only the modern library asset.'
        }
        [xml] $intelliSense = Read-Entry 'lib/net8.0-windows7.0/StaadPro.Interop.xml'
        if ($null -eq $intelliSense.SelectSingleNode("//member[@name='T:StaadPro.Interop.Services.OpenStaadSessionProvider']/summary")) {
            throw 'Missing provider IntelliSense.'
        }
        if ((Read-Entry 'README.md') -notmatch 'StaadPro.Interop.Core.Testing') { throw 'Wrong test package README.' }
        [void] (Read-Entry 'docs/OpenStaad-Session-Provider.md')
        if ($metadata.frameworkReferences.group.frameworkReference.name -notcontains 'Microsoft.WindowsDesktop.App.WPF') {
            throw 'Missing Windows Desktop/WPF dependency.'
        }
        if (@($metadata.dependencies.group.dependency | Where-Object { $_.id -eq 'Newtonsoft.Json' -and $_.version -eq '13.0.3' }).Count -ne 1) {
            throw 'Missing Newtonsoft.Json dependency.'
        }
    } finally { $archive.Dispose() }
    Get-FileHash -LiteralPath $packagePath -Algorithm SHA256
    Write-Output "Verified test archive: $packagePath"
    Write-Output 'This script does not upload or publish a package.'
} finally { Pop-Location }
