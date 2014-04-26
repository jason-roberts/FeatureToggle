echo Building core PCL package
nuget pack FeatureToggle.Core\FeatureToggle.Core.nuspec

echo Building .net 4 package
nuget pack FeatureToggle\FeatureToggle.nuspec


pause
