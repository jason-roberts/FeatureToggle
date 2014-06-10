#Release Notes 

# Version 2.1 In Development

## New Features

* New toggle to allow enabling on specific day(s) of the week [#32](https://github.com/jason-roberts/FeatureToggle/issues/32)
* New randomly enabled feature toggle [#43](https://github.com/jason-roberts/FeatureToggle/issues/43)

# Version 2

## Breaking Changes

* Namespace changes, root namespace is now simply FeatureToggle [#22](https://github.com/jason-roberts/FeatureToggle/issues/22)
* Removed Windows Phone 7 support
* Date-based toggle values in app.config now expect dd-MMM-yyyy HH:mm:ss format to disambiguate values, e.g. 01-Jan-2000 00:00:00
* SQLServer values column now must be a bit field
* INowDateAndTime removed, replaced with simpler Func<DateTime>
* Windows store installation is now a binary reference, rather than a NuGet code-only install. After updating to V2 you may want to delete the old code folder in Visual Studio.

## Improvements

* Added CLSCompliant attribute [#8](https://github.com/jason-roberts/FeatureToggle/pull/8)



##New Features

* New portable class library core will define simplest of toggles plus the interfaces, but will not contain any providers. This enables use in other PCL places where custom providers can be built by implementers. 
* Added Windows Phone 8 support
* Windows Store NuGet now comes with ARM, x86, and x64 DLLs. You will need to select a specific build platform, rather than AnyCPU.
* New RavenDB support in additional NuGet package


-------------------------------------------------

## Previous Versions

### 1.2

* Added code only initial WinRT support