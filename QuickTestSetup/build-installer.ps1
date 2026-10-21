param(
    [ValidateSet('Debug', 'Release')][string]$Configuration = 'Release',
    [string]$VisualStudioPath,
    [switch]$SkipApplicationBuild
)
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
if (!$SkipApplicationBuild) { & (Join-Path $root 'tests/build.ps1') }
if (!$VisualStudioPath) {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio/Installer/vswhere.exe'
    if (!(Test-Path -LiteralPath $vswhere)) { throw 'Visual Studio 2022/2026 is required to build .vdproj projects.' }
    $VisualStudioPath = & $vswhere -latest -products '*' -requires Microsoft.VisualStudio.Workload.ManagedDesktop -property installationPath
}
$devenv = Join-Path $VisualStudioPath 'Common7/IDE/devenv.com'
$disableOutOfProc = Join-Path $VisualStudioPath 'Common7/IDE/CommonExtensions/Microsoft/VSI/DisableOutOfProcBuild/DisableOutOfProcBuild.exe'
if (!(Test-Path -LiteralPath $devenv) -or !(Test-Path -LiteralPath $disableOutOfProc)) {
    throw 'Install Microsoft Visual Studio Installer Projects 2022 (supports VS 2022 and 2026), restart Visual Studio, and retry. MSBuild alone cannot build .vdproj.'
}

# Microsoft's documented prerequisite for command-line .vdproj builds.
Push-Location -LiteralPath (Split-Path $disableOutOfProc -Parent)
try {
    & $disableOutOfProc
    if ($LASTEXITCODE -ne 0) { throw 'Unable to configure command-line installer builds.' }
} finally { Pop-Location }
$output = Join-Path $root ('.artifacts/installer/' + $Configuration)
New-Item -ItemType Directory -Path $output -Force | Out-Null
$log = Join-Path $output 'build.log'
# A previous successful MSI must not conceal a later build that produced no output.
foreach ($name in @('QuickTestSetup.msi', 'setup.exe')) {
    $previous = Join-Path $output $name
    if (Test-Path -LiteralPath $previous) { Remove-Item -LiteralPath $previous }
}
# Keep IDE setup builds separate from SDK test-project design-time restoration.
if (Test-Path -LiteralPath $log) { Remove-Item -LiteralPath $log }
$arguments = @(('"' + (Join-Path $PSScriptRoot 'QuickTestSetup.sln') + '"'), '/Rebuild', $Configuration,
    '/Out', ('"' + $log + '"'))
$process = Start-Process -FilePath $devenv -ArgumentList $arguments -WindowStyle Hidden -Wait -PassThru
if ($process.ExitCode -ne 0) {
    if (Test-Path -LiteralPath $log) { Get-Content -LiteralPath $log -Tail 35 }
    throw 'QuickTestSetup build failed.'
}
$msi = Join-Path $output 'QuickTestSetup.msi'
if (!(Test-Path -LiteralPath $msi)) { throw 'Setup build did not produce QuickTestSetup.msi.' }
& (Join-Path $PSScriptRoot 'check-installer.ps1') -MsiPath $msi
Write-Output "Installer ready: $msi"
