echo off
echo deleting any old NuGet package binaries



if exist NuGet\FeatureToggle\lib\net451 del NuGet\FeatureToggle\lib\net451\*.* /q
if exist NuGet\FeatureToggle\lib\uap del NuGet\FeatureToggle\lib\uap\*.* /q 
if exist NuGet\FeatureToggle\lib\netcore del NuGet\FeatureToggle\lib\netcore\*.* /q
if exist NuGet\FeatureToggle\lib rd NuGet\FeatureToggle\lib /s /q


echo building solution
msbuild src\FeatureToggle.sln /t:Clean,Build /p:Configuration=Release /verbosity:m


echo packaging
NuGet\nuget pack NuGet\FeatureToggle\FeatureToggle.nuspec -OutputDirectory NuGet\FeatureToggle
