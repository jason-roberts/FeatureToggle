using System.Collections.Generic;
using FeatureToggle;
using Xunit;

namespace FeatureToggle.Tests
{    
    public class RandomFeatureToggleShould
    {
        [Fact]
        public void BeRandomlyEnabled()
        {
            var sut = new MyRandomFeatureToggle();

            var results = new List<bool>();


            for (var i = 0; i < 100; i++)
            {
                results.Add(sut.FeatureEnabled);
            }

            Assert.Contains(true, results);
            Assert.Contains(false, results);
        }

        private class MyRandomFeatureToggle : RandomFeatureToggle {}
    }   
}
