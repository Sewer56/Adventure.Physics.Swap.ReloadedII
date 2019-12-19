# Project Output Paths
$modOutputPath = "Release"
$solutionName = "Adventure.Physics.Swap.ReloadedII/Adventure.Physics.Swap.ReloadedII.csproj"
$configProjectName = "Adventure.Physics.Swap.Settings.ReloadedII/Adventure.Physics.Swap.Settings.ReloadedII.csproj"
$publishName = "sonicadventureengine.physicsswap.zip"
$publishDirectory = "Publish"

[Environment]::CurrentDirectory = $PWD

# Clean anything in existing Release directory.
Remove-Item $modOutputPath -Recurse
Remove-Item $publishDirectory -Recurse
New-Item $modOutputPath -ItemType Directory
New-Item $publishDirectory -ItemType Directory

# Build
dotnet restore $solutionName
dotnet clean $solutionName
dotnet publish $solutionName -c Debug --self-contained false -o "$modOutputPath"
dotnet publish $solutionName -c Debug -r win-x86 --self-contained false -o "$modOutputPath/x86" /p:PublishReadyToRun=true
dotnet publish $solutionName -c Debug -r win-x64 --self-contained false -o "$modOutputPath/x64" /p:PublishReadyToRun=true
dotnet publish $configProjectName -c Debug -r win-x64 --self-contained false -o "$modOutputPath" /p:PublishReadyToRun=true

# Remove Redundant Files
Remove-Item "$modOutputPath/x86/Preview.png"
Remove-Item "$modOutputPath/x64/Preview.png"
Remove-Item "$modOutputPath/x86/ModConfig.json"
Remove-Item "$modOutputPath/x64/ModConfig.json"

# Cleanup Unnecessary Files
Get-ChildItem $modOutputPath -Include Adventure.Physics.Swap.ReloadedII.exe -Recurse | Remove-Item -Force -Recurse
Get-ChildItem $modOutputPath -Include *.pdb -Recurse | Remove-Item -Force -Recurse
Get-ChildItem $modOutputPath -Include *.xml -Recurse | Remove-Item -Force -Recurse

# Compress
Add-Type -A System.IO.Compression.FileSystem
[IO.Compression.ZipFile]::CreateFromDirectory($modOutputPath, "$publishDirectory/$publishName")