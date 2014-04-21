#Release Notes 

# Version 2 in Development

## Breaking Changes

* Namespace changes, root namespace is now simply FeatureToggle [#22](https://github.com/jason-roberts/FeatureToggle/issues/22)
* Removed Windows Phone 7 support
* Date-based toggle values in app.config now expect dd-MMM-yyyy HH:mm:ss format to disambiguate values, e.g. 01-Jan-2000 00:00:00

## Improvements

* Added CLSCompliant attribute [#8](https://github.com/jason-roberts/FeatureToggle/pull/8)


##New Features

* New portable PCL core will define simplest of toggles plus the interfaces, but will not contain any providers. This enables use in other PCL places where custom providers can be built by implementers. 

# Previous Versions

## 1.2

* Added code only initial WinRT support