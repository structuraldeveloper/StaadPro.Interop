param(
    [Parameter(Mandatory = $true)][string] $PackageVersion,
    [Parameter(Mandatory = $true)][string] $DocumentedVersion,
    [Parameter(Mandatory = $true)][string] $ReadmeSource,
    [Parameter(Mandatory = $true)][string] $ReleaseNotesSource,
    [Parameter(Mandatory = $true)][string] $OutputDirectory
)

$ErrorActionPreference = 'Stop'
$readmeText = [IO.File]::ReadAllText((Resolve-Path -LiteralPath $ReadmeSource).Path)
$releaseText = [IO.File]::ReadAllText((Resolve-Path -LiteralPath $ReleaseNotesSource).Path)
$readmeVersion = [regex]::Match($readmeText, '(?m)^## Version ([^\r\n]+)').Groups[1].Value.Trim()
$releaseVersion = [regex]::Match($releaseText, '(?m)^## ([^ \r\n]+)').Groups[1].Value
if ($readmeVersion -ne $DocumentedVersion -or $releaseVersion -ne $DocumentedVersion) {
    throw 'Refresh the README version and latest release-notes entry before building a new package.'
}
$isCiPreview = $PackageVersion -match '^\d+\.\d+\.\d+-preview\.ci\.\d+$'
if ($PackageVersion -ne $DocumentedVersion -and -not $isCiPreview) {
    throw 'The distribution version must match ReleaseDocumentationVersion and the reviewed README/release notes.'
}
# CI archives receive their actual version in packaged documentation. Maintained source is unchanged.
if ($isCiPreview) {
    $readmeText = $readmeText.Replace($DocumentedVersion, $PackageVersion)
    $releaseText = $releaseText.Replace('## ' + $DocumentedVersion + ' ', '## ' + $PackageVersion + ' ')
}
[void] [IO.Directory]::CreateDirectory([IO.Path]::GetFullPath($OutputDirectory))
[IO.File]::WriteAllText((Join-Path $OutputDirectory 'README.md'), $readmeText, [Text.UTF8Encoding]::new($false))
[IO.File]::WriteAllText((Join-Path $OutputDirectory 'Release-Notes.md'), $releaseText, [Text.UTF8Encoding]::new($false))
Write-Output "Prepared README and release notes for $PackageVersion."
