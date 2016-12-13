echo building solution


echo Building core PCL package
NuGet\nuget pack NuGet\FeatureToggle.Core\FeatureToggle.Core.nuspec


echo Building main package
NuGet\nuget pack NuGet\FeatureToggle\FeatureToggle.nuspec


echo delete packaging dlls
del /s "NuGet\*.dll"

pause
