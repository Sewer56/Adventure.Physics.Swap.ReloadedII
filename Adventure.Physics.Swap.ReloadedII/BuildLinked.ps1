# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/sonicadventureengine.physicsswap/*" -Force -Recurse
dotnet publish "./Adventure.Physics.Swap.ReloadedII.csproj" -c Release -o "$env:RELOADEDIIMODS/sonicadventureengine.physicsswap" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location