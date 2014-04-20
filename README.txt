Install FeatureToggle easily via NuGet:

PM> Install-Package FeatureToggle

PM> Install-Package FeatureToggle.WPFExtensions

Design Goals

* Flexible provider model - can get the configured toggle value from build-in sources of easily create your own providers
* No magic strings - Toggles should be real things (objects) not just a loosly typed string (e.g. by name). This helps with removing the toggle after use.
* No default fallback values - if a toggle gets its value from a config file, and the config doesn't exist or is invalid, then you'll get an exception. FeatueToggle will never attempt to guess whether a toggle is enabled.