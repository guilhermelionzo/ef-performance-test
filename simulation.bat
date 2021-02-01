for /l %%x in (0, 1, 1) do (
   cp /bin/Debug/net5.0/entity-framework-performace.exe
   entity-framework-performace.exe %%x 10
)