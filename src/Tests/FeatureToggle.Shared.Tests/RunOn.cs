//#if NETFX_CORE

//using System;
//using System.Threading.Tasks;
//using Windows.UI.Core;



//// ReSharper disable CheckNamespace
//namespace FeatureToggle.Shared.Tests
//// ReSharper restore CheckNamespace
//{
//    public static class RunOn
//    {

//        public static async Task Dispatcher(Action a, int timeOut = 5000)
//        {
//            await
//                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
//                    CoreDispatcherPriority.Normal, a.Invoke);
//        }

//    }
//}
//#endif
