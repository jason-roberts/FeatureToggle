using System.Linq;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace FeatureToggle.Tests.ConventionTests
{
    class TypeIsAbstractConvention : IConvention<Types>
    {
        public void Execute(Types data, IConventionResultContext result)
        {
            var invalidTypes = data.TypesToVerify.Where(type => !type.IsAbstract).ToList();

            result.Is("Types should be abstract", invalidTypes);
        }

        public string ConventionReason
        {
            get
            {
                return "All classes must be abstract";
            }
        }
    }
}
