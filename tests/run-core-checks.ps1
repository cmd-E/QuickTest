$ErrorActionPreference = 'Stop'
$project = Split-Path $PSScriptRoot -Parent
$compiler = Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
$output = Join-Path ([IO.Path]::GetTempPath()) ('quicktest-checks-' + [Guid]::NewGuid().ToString('N') + '.exe')
try {
    $sources = @('Question','TestSession','TaggedQuestionParser','CheckForUpdates') | ForEach-Object { Join-Path $project ('QuickTest/Classes/' + $_ + '.cs') }
    & $compiler /nologo /target:exe "/out:$output" /reference:System.Core.dll /reference:System.Net.Http.dll /reference:System.Runtime.Serialization.dll $sources (Join-Path $PSScriptRoot 'CoreChecks.cs')
    if ($LASTEXITCODE -ne 0) { throw 'Compilation failed' }
    & $output
    if ($LASTEXITCODE -ne 0) { throw 'Checks failed' }
} finally { if (Test-Path -LiteralPath $output) { Remove-Item -LiteralPath $output } }
