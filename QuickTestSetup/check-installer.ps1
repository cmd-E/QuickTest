param([Parameter(Mandatory = $true)][string]$MsiPath)
$ErrorActionPreference = 'Stop'
$installer = New-Object -ComObject WindowsInstaller.Installer
$database = $null
try {
    $database = $installer.OpenDatabase([IO.Path]::GetFullPath($MsiPath), 0)
    $view = $database.OpenView('SELECT `FileName` FROM `File`')
    try {
        $view.Execute()
        $files = while ($record = $view.Fetch()) {
            try { ($record.StringData(1) -split '\|')[-1] }
            finally { [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($record) }
        }
    } finally {
        $view.Close()
        [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($view)
    }
    $required = @('QuickTest.exe', 'QuickTest.exe.config', 'Syncfusion.DocIO.Base.dll',
        'Syncfusion.Compression.Base.dll', 'Syncfusion.Licensing.dll', 'Syncfusion.OfficeChart.Base.dll', 'LICENSE.rtf', 'app_icon.ico')
    foreach ($file in $required) {
        if ($file -notin $files) { throw "Installer payload is missing $file." }
    }
    foreach ($file in @('System.Text.Json.dll', 'System.Memory.dll', 'System.Buffers.dll')) {
        if ($file -in $files) { throw "Installer still contains the removed dependency $file." }
    }
    Write-Output "Installer payload verified: $($files.Count) files; application, configuration, Word libraries, icon and license included."
} finally {
    if ($database) { [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($database) }
    [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($installer)
}
