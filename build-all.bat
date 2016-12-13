echo building .net framework and core PCL binaries
msbuild src\FeatureToggleSolution.sln /t:Rebuild /p:Configuration=Release /verbosity:q
copy src\FeatureToggle.Core\bin\Release\FeatureToggle.Core.dll "NuGet\FeatureToggle.Core\lib\portable-net40+sl50+wp81+wpa+win"
copy src\FeatureToggle\bin\Release\FeatureToggle.dll NuGet\FeatureToggle\lib\net40




echo building platform specific binaries for Windows Phone 8.1 Silverlight
msbuild src\WindowsPhone81SilverlightBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=x86 /verbosity:q
msbuild src\WindowsPhone81SilverlightBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=ARM /verbosity:q
copy src\FeatureToggle.WindowsPhone81Silverlight\bin\x86\Release\FeatureToggle.WindowsPhone81Silverlight.dll NuGet\FeatureToggle\lib\wp81
copy src\FeatureToggle.WindowsPhone81Silverlight\bin\x86\Release\FeatureToggle.WindowsPhone81Silverlight.dll NuGet\FeatureToggle\build\wp81\x86
copy src\FeatureToggle.WindowsPhone81Silverlight\bin\ARM\Release\FeatureToggle.WindowsPhone81Silverlight.dll NuGet\FeatureToggle\build\wp81\ARM



echo building platform specific binaries for Windows Universal Apps portable (inc Windows Phone 8.1 RT)
msbuild src\WindowsUniversalApps81Build.sln /t:Rebuild /p:Configuration=Release /p:Platform="Any CPU" /verbosity:q
copy src\FeatureToggle.UniversalApps81\bin\Release\FeatureToggle.UniversalApps81.dll "NuGet\FeatureToggle\lib\portable-net451+netcore451+wpa81"


     
build-nuget.bat