@echo off
setlocal EnableDelayedExpansion
setlocal EnableExtensions

rem Fibonacci
set /a i=0
set /a limit=5
:WHILE_0
if !i! LSS !limit! (
   
   timeout /t 3 /nobreak > NUL
   dotnet run 0 20
   timeout /t 3 /nobreak > NUL
   dotnet run 1 20
   echo ----------------------------
   set /a i=^(!i! + 1^)
   goto WHILE_0
)