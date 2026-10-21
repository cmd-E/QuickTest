# QuickTestSetup

`QuickTestSetup.vdproj` builds a Windows Installer MSI. It is supported by Visual Studio 2022 and Visual Studio 2026 when Microsoft's **Visual Studio Installer Projects 2022** extension is installed:

https://marketplace.visualstudio.com/items?itemName=visualstudioclient.MicrosoftVisualStudio2022InstallerProjects

The message in `UpgradeLog2.htm` ("The application which this project type is based on was not found") means the project loader is missing, not that QuickTest requires an obsolete operating system. Install the extension, restart Visual Studio, and reload the solution. Full Visual Studio with the .NET desktop workload is required for installer builds; MSBuild/Build Tools alone cannot load `.vdproj`.

## Build

Run in PowerShell on Windows from the repository root:

```powershell
./QuickTestSetup/build-installer.ps1
```

The helper restores dependencies and uses MSBuild to compile the selected application configuration into `.artifacts/installer-payload`. It runs Microsoft's `DisableOutOfProcBuild.exe` from its installation directory and uses `devenv.com` to rebuild the dedicated `QuickTestSetup/QuickTestSetup.sln`. This solution contains only the installer and consumes an explicit eight-file payload. The IDE therefore does not need the obsolete 4.6.1 targeting pack to load an application project or resolve a primary-output group. MSBuild uses the restored reference assemblies instead. The Microsoft helper sets `EnableOutOfProcBuild=0` for the selected Visual Studio instance, as documented for command-line setup builds. An explicit installation can be selected with `-VisualStudioPath`.

Outputs are written to `.artifacts/installer/Release/QuickTestSetup.msi` and `setup.exe`. Use `-Configuration Debug` for a debug package. The helper opens the MSI database read-only to check the payload: executable, configuration, four required Syncfusion libraries, icon and license. It does not install the application or execute the bootstrapper.

The CI test job also builds/inspects the MSI and uploads the installer as the `windows-installer` artifact. It uses `-SkipRestore` after the preceding tests have restored all packages; the application is still rebuilt for packaging. Install the extension on any replacement runner that lacks it. For manual IDE packaging, first run the build helper to refresh the payload, then open `QuickTestSetup.sln`. Normal test and CodeQL builds restore the test-project dependency graph directly and do not need the installer extension.

## Compatibility and release notes

- Setup uses repository-relative paths for the icon, MIT license and freshly compiled executable/configuration/Word libraries. The explicit payload omits obsolete JSON dependencies and framework-provided DLLs, so compile-only reference assemblies are not shipped.
- The MSI condition matches the application's .NET Framework 4.6.1 target and accepts later 4.x runtimes, including 4.8/4.8.1 on modern Windows. The existing bootstrapper offers 4.7.2 when a compatible framework is missing. The application itself is not retargeted.
- Installation is per-user under `%LOCALAPPDATA%\Programs\QuickTest`, matching the installer's non-elevated scope.
- The stable upgrade code is preserved and downgrade detection is enabled. Before publishing a new product version, update `ProductVersion`, generate a new `ProductCode` in the setup project's properties, and update the application assembly version together. The checked-in version remains 1.0.4; this work does not invent a release version.
- Packages are unsigned development artifacts. Signing and a real install/upgrade/uninstall test in a disposable Windows VM remain release-time checks.

The repaired project was built and its eight-file MSI payload and newer-runtime acceptance were verified locally with Visual Studio 2026 and Installer Projects 3.0.0. This verifies package construction, not a real installation on the user's system.
