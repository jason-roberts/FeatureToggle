echo building solution


echo Building core PCL package
nuget pack NuGet\FeatureToggle.Core\FeatureToggle.Core.nuspec



echo Building other package
nuget pack NuGet\FeatureToggle\FeatureToggle.nuspec


pause
