@echo off
setlocal enabledelayedexpansion

powershell -Command "$serverDateTime = Invoke-RestMethod 'http://192.168.0.1:8000'; Set-Date -Date $serverDateTime; Write-Output $serverDateTime"

pause
endlocal
