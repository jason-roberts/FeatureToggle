using System.Collections.Generic;
using System.Web.Http;
using FeatureToggle.Providers;

namespace FeatureToggle.Integration.Tests.TestApiServer
{
    public class TestController : ApiController
    {
        public JsonEnabledResponse GetEnabled()
        {
            return new JsonEnabledResponse{Enabled=true};
        }

        public JsonEnabledResponse GetDisabled()
        {
            return new JsonEnabledResponse { Enabled = false };
        }

        public KittyKat GetBadJson()
        {
            return new KittyKat {Name = "SnowyWowy"};
        }

        public class KittyKat
        {
            public string Name { get; set; }
        }
    }
}