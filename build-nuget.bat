echo building solution


echo Building core PCL package
nuget pack NuGet\FeatureToggle.Core\FeatureToggle.Core.nuspec



echo Building main package
nuget pack NuGet\FeatureToggle\FeatureToggle.nuspec


echo Building RavenDB
nuget pack NuGet\FeatureToggle.RavenDB\FeatureToggle.RavenDB.nuspec

pause
