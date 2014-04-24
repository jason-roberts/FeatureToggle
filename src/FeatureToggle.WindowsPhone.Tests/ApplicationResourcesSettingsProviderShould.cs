using System;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace FeatureToggle.WindowsPhone.Tests
{
    [TestClass]
    public class ApplicationResourcesSettingsProviderShould
    {
        [TestMethod]        
        public void ReadBooleanTrue()
        {
            bool result = false;

            var sut = new ApplicationResourcesSettingsProvider();

            RunOn.Dispatcher(() => { result = sut.EvaluateBooleanToggleValue(new BooleanTrue()); });

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void ReadBooleanFalse()
        {
            bool result = true;

            var sut = new ApplicationResourcesSettingsProvider();

            RunOn.Dispatcher(() => { result = sut.EvaluateBooleanToggleValue(new BooleanFalse()); });            

            Assert.IsFalse(result);
        }



        //[TestMethod]
        //public void ErrorWhenKeyNotInConfig()
        //{
        //    try
        //    {
        //        RunOn.Dispatcher(() => new ApplicationResourcesSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig()));
        //        Assert.Fail("Exception expected");
        //    }
        //    catch (ToggleConfigurationError expected)
        //    {
        //        // Pass
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail("Exception expected");
        //    }                            
        //}        


        private class BooleanTrue : SimpleFeatureToggle
        {
        }

        private class BooleanFalse : SimpleFeatureToggle
        {
        }

        private class NotInConfig : SimpleFeatureToggle
        {
        }
    }


 
}
