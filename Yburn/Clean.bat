@echo off
set error=0

for /D /R %%d in (*) do (
	call :rdifexist "%%~d\bin"
	call :rdifexist "%%~d\obj"
)

if %error%==0 echo Cleaning completed successfully. && timeout /T 5
if %error%==1 echo Operation has failed! See above for details. && pause
goto :eof

:rdifexist
if exist "%~1" rd /S /Q "%~1"
if exist "%~1" set error=1
goto :eof