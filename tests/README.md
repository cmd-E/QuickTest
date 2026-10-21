# Tests and CI

`QuickTest.Tests` is an xUnit project in `QuickTest.sln`. Tests reference the production assembly, with internal access limited to the test assembly. The application remains on .NET Framework 4.6.1 and the test host targets 4.8.

On Windows, install the .NET SDK, Visual Studio desktop build tools, and .NET Framework 4.8. Compilation reference assemblies and the pinned Syncfusion dependencies are restored from NuGet. Run from the repository root:

```powershell
./tests/run-tests.ps1
./tests/run-tests.ps1 -UnitOnly
./tests/run-tests.ps1 -Coverage
./tests/test-coverage-check.ps1
```

`tests/build.ps1` is shared by tests and CodeQL. It restores the test-project graph directly because MSBuild cannot load the optional `.vdproj` installer in the IDE solution. Build outputs go to ignored `.artifacts` directories. Real Word adapter tests do not require Microsoft Word to be installed.

## FIRST and scope

- **Fast:** parser, model, session, version, color and update-check tests use fixed inputs, seeded random generators, and fake HTTP handlers; no live GitHub requests or sleeps.
- **Independent:** document tests create their own disposable directories and actual `.doc`/`.docx` fixtures. Window tests create/dispose hidden controls on dedicated STA threads. No application is installed, and no user documents or saved settings are modified.
- **Repeatable:** no external network or clock dependencies in the tests. Concurrent update checks are coordinated with task completion sources, rather than timing assumptions.
- **Self-validating:** assertions check answer identity, collection immutability, independent sessions, scoring, request headers/disposal, error recovery, document round trips, source/output preservation and staging-file cleanup.
- **Timely:** regression tests accompany the color fix and installer repairs. Existing behavior receives retrospective characterization coverage; these are not presented as tests that predated the original application.

Tests tagged `Category=Integration` exercise the real Word adapter and hidden WinForms controls. Use `--filter Category=Integration` or `--filter Category!=Integration` with `dotnet test --no-build --no-restore` to select a suite. The older core/document smoke-check scripts also remain available.

Interactive dialogs, browser launching, live update-server behavior, user-settings persistence, the full test-taking UI flow, and actual MSI install/upgrade/uninstall still need separate UI/release testing. The test suite does not claim complete application coverage.

## GitHub Actions

The workflow runs on pushes, pull requests and manual dispatch. It builds on Windows, runs the xUnit suite and both smoke checks, enforces core coverage, and builds/inspects the setup MSI. It uploads TRX results, Cobertura coverage, JSON/Markdown summaries and installer logs as `windows-test-results`, and MSI/bootstrapper files as `windows-installer`, retained for 14 days.

A separate `CodeQL (C#)` job traces a clean rebuild and runs the `security-extended` suite. It needs `security-events: write` to upload findings; the test job has read-only repository permissions. No stored secrets are needed. The scan job passing means analysis completed; findings still need review.

Coverage minimums are recorded in `.github/coverage-baseline.json`. Each run publishes a class-level table in the Actions summary and fails for a missing report/class or a class below its minimum. The async update-check state machine is monitored separately so generated async code is not hidden by the wrapper's coverage. Overall application coverage is reported without a threshold because it includes forms and generated code. Changes to async method ordering can rename compiler-generated classes; inspect and update the baseline explicitly if that happens.

`.github/branch-ruleset.json` provides the same default-branch protection proposal as TrashVanish. Committing it does not activate repository settings. Apply it through GitHub only after approving the exact policy: PRs, resolved conversations, up-to-date passing tests/coverage and CodeQL, no force pushes/deletion, zero human approvals and no bypass actors.

## Verified locally

111 xUnit cases passed, including actual `.doc` and `.docx` integration tests. The monitored core classes and the `CheckUsing` async state machine reached 100% line coverage; overall application line coverage was approximately 54.6%. The public `Check()` wrapper intentionally remains uncalled because it contacts the real release server.
