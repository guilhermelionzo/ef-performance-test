@echo off
setlocal EnableDelayedExpansion
setlocal EnableExtensions

rem Simulation
set /a i=0
set /a limit=5
:WHILE_0
if !i! LSS !limit! (
   
   timeout /t 3 /nobreak > NUL
   dotnet run 0 1000
   timeout /t 3 /nobreak > NUL
   dotnet run 1 1000
   echo ----------------------------
   set /a i=^(!i! + 1^)
   goto WHILE_0
)