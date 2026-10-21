param(
    [string]$MSBuildPath,
    [switch]$Rebuild
)
$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
$testProject = Join-Path $projectRoot 'QuickTest.Tests/QuickTest.Tests.csproj'

if (!$MSBuildPath) {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio/Installer/vswhere.exe'
    if (!(Test-Path -LiteralPath $vswhere)) { throw 'Install Visual Studio Build Tools with .NET desktop build tools, or supply -MSBuildPath.' }
    $MSBuildPath = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild Microsoft.NetCore.Component.SDK -find 'MSBuild\**\Bin\MSBuild.exe' |
        Select-Object -First 1
}
if (!$MSBuildPath) { throw 'MSBuild was not found.' }

# Both normal tests and CodeQL use the same restored production build.
# Restore the project graph directly: MSBuild cannot load the optional .vdproj in the IDE solution.
& $MSBuildPath $testProject /t:Restore /p:RestorePackagesConfig=true "/p:RestoreRepositoryPath=$projectRoot/packages" /nologo /verbosity:minimal
if ($LASTEXITCODE -ne 0) { throw 'Dependency restore failed.' }
$target = if ($Rebuild) { '/t:Rebuild' } else { '/t:Build' }
& $MSBuildPath $testProject $target /p:Configuration=Debug /nologo /verbosity:minimal
if ($LASTEXITCODE -ne 0) { throw 'Build failed.' }
