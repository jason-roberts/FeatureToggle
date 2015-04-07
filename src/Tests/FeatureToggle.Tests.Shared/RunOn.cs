using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

#if NETFX_CORE
    using Windows.UI.Core;
#else
    using System.Windows;
#endif



// ReSharper disable CheckNamespace
namespace FeatureToggle.Tests.Shared
// ReSharper restore CheckNamespace
{
    public static class RunOn
    {
#if NETFX_CORE
      

        public static async Task Dispatcher(Action a, int timeOut = 5000)
        {
            await
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal, a.Invoke);
        }

#else

        public static void Dispatcher(Action a, int timeOut = 5000)
        {
            var waitHandle = new AutoResetEvent(false);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    a.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }
                finally
                {
                    waitHandle.Set();
                }
            });

            waitHandle.WaitOne(timeOut);
        }

#endif


    }
}
