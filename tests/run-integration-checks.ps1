param([string]$BuildDirectory)
$ErrorActionPreference='Stop'
$projectRoot=Split-Path $PSScriptRoot -Parent
if(!$BuildDirectory){$BuildDirectory=Join-Path $projectRoot '.artifacts/build'}
$fixture=Join-Path ([IO.Path]::GetTempPath()) ('quicktest-integration-'+[Guid]::NewGuid().ToString('N'))
$compiler=Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
New-Item -ItemType Directory -Path $fixture | Out-Null
try {
    Copy-Item -LiteralPath (Join-Path $BuildDirectory 'QuickTest.exe') -Destination $fixture
    Get-ChildItem -LiteralPath $BuildDirectory -Filter '*.dll' | Copy-Item -Destination $fixture
    $references=@('/reference:System.Core.dll',('/reference:'+(Join-Path $fixture 'QuickTest.exe')),('/reference:'+(Join-Path $fixture 'Syncfusion.DocIO.Base.dll')))
    $output=Join-Path $fixture 'Checks.exe'
    & $compiler /nologo /target:exe "/out:$output" $references (Join-Path $PSScriptRoot 'DocumentChecks.cs')
    if($LASTEXITCODE -ne 0){throw 'Integration check compilation failed'}
    & $output
    if($LASTEXITCODE -ne 0){throw 'Integration checks failed'}
} finally {
    $resolved=[IO.Path]::GetFullPath($fixture)
    $temporaryRoot=[IO.Path]::GetFullPath([IO.Path]::GetTempPath())
    if(!$resolved.StartsWith($temporaryRoot,[StringComparison]::OrdinalIgnoreCase)){throw 'Unsafe fixture cleanup target'}
    Remove-Item -LiteralPath $resolved -Recurse -Force
}
