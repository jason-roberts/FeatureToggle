using System.Linq;
using FeatureToggle.Toggles;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;
using TestStack.ConventionTests.Conventions;
using Xunit;

namespace FeatureToggle.Tests.ConventionTests
{
    public class ToggleConventionTests
    {
        private const string TogglesNamespace = "FeatureToggle.Toggles";

        [Fact]
        public void TogglesInCorrectNamespace()
        {
            var typesToCheck = Types.InAssemblyOf<SimpleFeatureToggle>();           

            Convention.Is(new ClassTypeHasSpecificNamespace(
                x => x.Name.EndsWith("Toggle"),
                TogglesNamespace,
                "Toggle Base Classes"), typesToCheck);
        }

        [Fact]
        public void TogglesAreAbstract()
        {
            var typesToCheck = Types.InAssemblyOf<SimpleFeatureToggle>("Toggle base classes", types => types.Where(x => x.Namespace == TogglesNamespace));

            Convention.Is(new TypeIsAbstractConvention(), typesToCheck);
        }
    }
}
