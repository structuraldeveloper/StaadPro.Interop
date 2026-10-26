# Read-only diagnostic. Requires a running analyzed model on the caller's COM apartment.
# No launch, file open, load activation, analysis, save or model edits are performed.
# Results describe sampled nodal equilibrium only; they do not certify every native build.
$ErrorActionPreference = 'Stop'
Add-Type -Path (Join-Path $PSScriptRoot 'PlateCornerCoordinateProbe.cs') -ReferencedAssemblies 'Microsoft.CSharp.dll','System.Core.dll'
[CornerEquilibriumProbe]::Run()
