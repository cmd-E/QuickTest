# QuickTestSetup

`QuickTestSetup.vdproj` builds a Windows Installer MSI. It is supported by Visual Studio 2022 and Visual Studio 2026 when Microsoft's **Visual Studio Installer Projects 2022** extension is installed:

https://marketplace.visualstudio.com/items?itemName=visualstudioclient.MicrosoftVisualStudio2022InstallerProjects

The message in `UpgradeLog2.htm` ("The application which this project type is based on was not found") means the project loader is missing, not that QuickTest requires an obsolete operating system. Install the extension, restart Visual Studio, and reload the solution. Full Visual Studio with the .NET desktop workload is required for installer builds; MSBuild/Build Tools alone cannot load `.vdproj`.

## Build

Run in PowerShell on Windows from the repository root:

```powershell
./QuickTestSetup/build-installer.ps1
```

The helper restores/builds the application, runs Microsoft's `DisableOutOfProcBuild.exe` from its installation directory, and uses `devenv.com` to rebuild the setup project. The Microsoft helper sets `EnableOutOfProcBuild=0` for the selected Visual Studio instance, as documented for command-line setup builds. An explicit installation can be selected with `-VisualStudioPath`.

Outputs are written to `.artifacts/installer/Release/QuickTestSetup.msi` and `setup.exe`. Use `-Configuration Debug` for a debug package. The helper opens the MSI database read-only to check the payload: executable, configuration, four required Syncfusion libraries, icon and license. It does not install the application or execute the bootstrapper.

The CI test job also builds/inspects the MSI and uploads the installer as the `windows-installer` artifact. Install the extension on any replacement runner that lacks it. Normal test and CodeQL builds restore the test-project dependency graph directly and do not need the installer extension.

## Compatibility and release notes

- Setup now uses repository-relative paths for the icon, MIT license and restored Word libraries. Obsolete JSON/runtime dependency entries are removed.
- The existing .NET Framework 4.7.2 minimum allows later 4.x versions, including 4.8/4.8.1 on modern Windows. The application itself still targets 4.6.1.
- Installation is per-user under `%LOCALAPPDATA%\Programs\QuickTest`, matching the installer's non-elevated scope.
- The stable upgrade code is preserved and downgrade detection is enabled. Before publishing a new product version, update `ProductVersion`, generate a new `ProductCode` in the setup project's properties, and update the application assembly version together. The checked-in version remains 1.0.4; this work does not invent a release version.
- Packages are unsigned development artifacts. Signing and a real install/upgrade/uninstall test in a disposable Windows VM remain release-time checks.
