#!/bin/bash

echo "Bash Simulation"

x=1
limit=$((10))

while [ $x -le $limit ]
do
    sleep 3
    # dotnet run 0 1000
    sleep 3
    echo ----------------------------
    x=$(( $x + 1 ))
done
