Write-Output "Removing results.xml.";
Remove-Item -Path ".\results.xml" -Force -ErrorAction SilentlyContinue;

Write-Output "Clearing Report directory.";
Get-ChildItem -Path ".\Report" -Recurse -Force | Remove-Item -Force -Recurse -ErrorAction SilentlyContinue

Write-Output "Clearing TestResults directory.";
Get-ChildItem -Path ".\TestResults" -Recurse -Force | Remove-Item -Force -Recurse -ErrorAction SilentlyContinue;

Write-Output "Done."