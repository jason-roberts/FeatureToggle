echo building solution

msbuild src\FeatureToggleSolution.sln /t:Rebuild /p:Configuration=Release /verbosity:q

echo building platform specific windows 8

msbuild src\WindowsStoreBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=x86 /verbosity:q
msbuild src\WindowsStoreBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=x64 /verbosity:q
msbuild src\WindowsStoreBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=ARM /verbosity:q


echo copying nuget package files

copy src\FeatureToggle.Core\bin\Release\FeatureToggle.Core.dll "NuGet\FeatureToggle.Core\lib\portable-net45+sl50+wp80+win"
copy src\FeatureToggle\bin\Release\FeatureToggle.dll NuGet\FeatureToggle\lib\net40
copy src\FeatureToggle.WindowsStore\bin\x86\Release\FeatureToggle.WindowsStore.dll NuGet\FeatureToggle\lib\netcore451
copy src\FeatureToggle.WindowsStore\bin\x86\Release\FeatureToggle.WindowsStore.dll NuGet\FeatureToggle\build\netcore451\x86
copy src\FeatureToggle.WindowsStore\bin\x64\Release\FeatureToggle.WindowsStore.dll NuGet\FeatureToggle\build\netcore451\x64
copy src\FeatureToggle.WindowsStore\bin\ARM\Release\FeatureToggle.WindowsStore.dll NuGet\FeatureToggle\build\netcore451\ARM


build-nuget.bat

