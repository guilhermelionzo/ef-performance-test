@echo off
setlocal EnableDelayedExpansion
setlocal EnableExtensions

rem Fibonacci
set /a i=0
set /a limit=10
:WHILE_0
if !i! LSS !limit! (
   
   dotnet run 0 !i!
   dotnet run 1 !i!
   
   set /a i=^(!i! + 1^)
   goto WHILE_0
)