echo packaging
NuGet\nuget pack NuGet\FeatureToggle\FeatureToggle.nuspec -OutputDirectory NuGet\FeatureToggle

echo copying NuGet packages for manual NuGet testing
del NuGet\FeatureToggleNugetTestFeed\*.nupkg /q

copy src\FeatureToggle.NetStandard\bin\Release\*.nupkg NuGet\FeatureToggleNugetTestFeed /y

copy src\FeatureToggle.NetCore\bin\Release\*.nupkg NuGet\FeatureToggleNugetTestFeed /y

copy NuGet\FeatureToggle\*.nupkg NuGet\FeatureToggleNugetTestFeed /y





echo clearing local NuGet caches for manual nuget package testing

nuget\nuget.exe locals all -clear

