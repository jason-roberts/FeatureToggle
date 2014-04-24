using System;
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



        [TestMethod]
        public void ErrorWhenKeyNotInConfig()
        {
            Exception expectedEx =  null;

            RunOn.Dispatcher(() =>
                             {
                                 try
                                 {
                                     new ApplicationResourcesSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig());
                                 }
                                 catch (Exception ex)
                                 {
                                     expectedEx = ex;
                                 }                                 
                             });

            Assert.IsNotNull(expectedEx, "Exception expected");           
        }        


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
