echo building solution



echo Building core PCL package
nuget pack FeatureToggle.Core\FeatureToggle.Core.nuspec

echo Building other package
nuget pack FeatureToggle\FeatureToggle.nuspec


pause
