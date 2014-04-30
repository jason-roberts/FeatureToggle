#FeatureToggle

##Simple, reliable feature toggles in .NET

Install FeatureToggle easily via NuGet:

PM> Install-Package FeatureToggle

PM> Install-Package FeatureToggle.WPFExtensions

##Design Goals

###Flexible Provider Model
Can get the configured toggle value from the built-in default providers, or easily create and supply your own providers

###No Magic Strings
Toggles should be real things (objects) not just a loosly typed string.
This helps with removing the toggle after use:

- Can perform a "find uses" of the Toggle class to see where it's used
- Can just delete the Toggle class and see where build fails.

###No Default Fallback Value
If a toggle gets its value from a config file, and the config doesn't exist or is invalid, then you'll get an exception.

FeatueToggle will never attempt to guess whether a toggle is enabled or not.