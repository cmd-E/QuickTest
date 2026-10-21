Run ./tests/run-core-checks.ps1 in PowerShell on Windows. It compiles the production parser/session/version logic with the installed .NET Framework compiler and runs it without opening windows or contacting GitHub.

Full application build requires the existing Syncfusion packages. Keep .doc and .docx fixtures for adapter tests; pure parser tests cannot validate vendor-specific Word behavior.
