# Release Notes 

# Version 4.0.2

Added initial .NET Core support and removed support for some older platforms.

[v4 issues / work log](https://github.com/jason-roberts/FeatureToggle/issues?utf8=%E2%9C%93&q=milestone%3A%22Version%204.0%22%20)

This project follows semantic versioning, an increase in the major release number (in this case from 3 to 4) indicates there are breaking changes.

## Breaking Changes

* Min framework now 4.6.1 / .NET Standard 1.4
* Windows 8.n, Windows phone 8.n, Windows Phone Silverlight 8.n no longer supported
* Namespace changes: most types needed for application developers are now under root FeatureToggle namespace
* Types not usually required by client code moved to FeatureToggle.Internal
* Windows UWP now supported explicitly from build 14393 

## .Net Core Limitations/Specifics

* No HttpJsonFeatureToggle
* No SqlFeatureToggle




# Version 3.5.1

* Updated NuGet version to set exact version [#127](https://github.com/jason-roberts/FeatureToggle/issues/127)

# Version 3.5

* Updated NuGet installer, no longer installs code-only sample toggles [#117](https://github.com/jason-roberts/FeatureToggle/issues/117)

# Version 3.4

* New fluent static way of getting value of a toggle [#72](https://github.com/jason-roberts/FeatureToggle/issues/72) without needing to manually create an instance of the toggle.

E.g. 
```
using FeatureToggle.Core.Fluent;
...
Is<Printing>.Enabled;
Is<Printing>.Disabled;
```

# Version 3.3

* New CompositeOrDecorator [#87](https://github.com/jason-roberts/FeatureToggle/issues/87)
* New CompositeOFallbackValueDecoratorrDecorator allows the specification of a primary toggle, if the primary toggle fails or is not configured the specified fallback toggle will be used. Optionally a "logging" action can be specified if the primary toggle fails. [#111](https://github.com/jason-roberts/FeatureToggle/issues/111)

# Version 3.2

* Multiple Boolean Sql server toggles can now share a single <connectionStrings> named connection string [#96](https://github.com/jason-roberts/FeatureToggle/issues/96) (thanks to @craig-wagner)

# Version 3.1

* The BooleanSqlServerProvider will now look for a connection string in both app settings or connection string settings [#88](https://github.com/jason-roberts/FeatureToggle/pull/88) (thanks to @bastianjohn)

# Version 3.0

## Breaking Changes

* Removed the WPF, etc Visibility converters. [#67](https://github.com/jason-roberts/FeatureToggle/issues/67)
* Removed Windows phone 8.0 support [#80](https://github.com/jason-roberts/FeatureToggle/issues/80)

## Enhancements / New Features

* Windows Phone 8.1 Silverlight support [#81](https://github.com/jason-roberts/FeatureToggle/issues/81)
* Windows Universal Apps 8.1 support [#21](https://github.com/jason-roberts/FeatureToggle/issues/21)
* New toggle decorator that defaults to true when a toggle (configuration) error occurs [#61](https://github.com/jason-roberts/FeatureToggle/issues/61)
* New toggle decorator that defaults to false when a toggle (configuration) error occurs [#60](https://github.com/jason-roberts/FeatureToggle/issues/60)
* New toggle that becomes enabled once specific assembly version number is reached [#63](https://github.com/jason-roberts/FeatureToggle/issues/63)
* New toggle decorator that becomes enabled if all wrapped toggles are enabled [#68](https://github.com/jason-roberts/FeatureToggle/issues/68)
* New toggle decorator that caches underlying toggle value for a fixed period of time [#64](https://github.com/jason-roberts/FeatureToggle/issues/64)


# Version 2.2.1

* Fixed .Net 4 support [#62](https://github.com/jason-roberts/FeatureToggle/issues/62)

# Version 2.2

## New Features

* New HttpJsonFeatureToggle [#50](https://github.com/jason-roberts/FeatureToggle/issues/50)

# Version 2.1

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



## New Features

* New portable class library core will define simplest of toggles plus the interfaces, but will not contain any providers. This enables use in other PCL places where custom providers can be built by implementers. 
* Added Windows Phone 8 support
* Windows Store NuGet now comes with ARM, x86, and x64 DLLs. You will need to select a specific build platform, rather than AnyCPU.
* New RavenDB support in additional NuGet package


-------------------------------------------------

## Previous Versions

### 1.2

* Added code only initial WinRT support
