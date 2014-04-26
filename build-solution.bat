echo building solution

msbuild src\FeatureToggleSolution.sln /t:Rebuild /p:Configuration=Release

echo building platform specific windows 8

msbuild src\FeatureToggleSolution.sln /t:Rebuild /p:Configuration=WindowsStoreOnly /p:BuildProjectReferences=false /p:Platform=x86
msbuild src\FeatureToggleSolution.sln /t:Rebuild /p:Configuration=WindowsStoreOnly /p:BuildProjectReferences=false /p:Platform=x64
msbuild src\FeatureToggleSolution.sln /t:Rebuild /p:Configuration=WindowsStoreOnly /p:BuildProjectReferences=false /p:Platform=ARM


pause
