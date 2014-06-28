echo building solution

msbuild src\FeatureToggleSolution.sln /t:Rebuild /p:Configuration=Release /verbosity:q

echo building platform specific windows 8

msbuild src\WindowsStoreBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=x86 /verbosity:q
msbuild src\WindowsStoreBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=x64 /verbosity:q
msbuild src\WindowsStoreBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=ARM /verbosity:q


echo building platform specific windows phone
msbuild src\WindowsPhoneBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=ARM /verbosity:q
msbuild src\WindowsPhoneBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform=x86 /verbosity:q


echo copying nuget package files

copy src\FeatureToggle.Core\bin\Release\FeatureToggle.Core.dll "NuGet\FeatureToggle.Core\lib\portable-net40+sl50+wp80+win"

copy src\FeatureToggle\bin\Release\FeatureToggle.dll NuGet\FeatureToggle\lib\net40

copy src\FeatureToggle.WindowsStore\bin\x86\Release\FeatureToggle.WindowsStore.dll NuGet\FeatureToggle\lib\netcore451
copy src\FeatureToggle.WindowsStore\bin\x86\Release\FeatureToggle.WindowsStore.dll NuGet\FeatureToggle\build\netcore451\x86
copy src\FeatureToggle.WindowsStore\bin\x64\Release\FeatureToggle.WindowsStore.dll NuGet\FeatureToggle\build\netcore451\x64
copy src\FeatureToggle.WindowsStore\bin\ARM\Release\FeatureToggle.WindowsStore.dll NuGet\FeatureToggle\build\netcore451\ARM

copy src\FeatureToggle.WindowsPhone\bin\x86\Release\FeatureToggle.WindowsPhone.dll NuGet\FeatureToggle\lib\windowsphone8
copy src\FeatureToggle.WindowsPhone\bin\x86\Release\FeatureToggle.WindowsPhone.dll NuGet\FeatureToggle\build\windowsphone8\x86
copy src\FeatureToggle.WindowsPhone\bin\ARM\Release\FeatureToggle.WindowsPhone.dll NuGet\FeatureToggle\build\windowsphone8\ARM

copy src\FeatureToggle.WpfExtensions\bin\Release\FeatureToggle.WpfExtensions.dll NuGet\FeatureToggle.WpfExtensions\lib\net40

copy src\FeatureToggle.RavenDB\bin\Release\FeatureToggle.RavenDB.dll NuGet\FeatureToggle.RavenDB\lib\net40
     
build-nuget.bat

