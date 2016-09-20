#
# nuget_push.ps1
#
Clear-Host

$nugetPack = "$PSScriptRoot\bin\Release\f14.AutoVersion.1.0.0.nupkg"
nuget.exe push $nugetPack -Source https://www.nuget.org/api/v2/package
