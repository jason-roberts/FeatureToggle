using System;

namespace FeatureToggle
{
    public class RandomFeatureToggle : IFeatureToggle
    {        
        public bool FeatureEnabled
        {
            get
            {               
                return RandomGenerator.Next() % 2 == 0;                
            }
        }


        // Based on: http://blogs.msdn.com/b/pfxteam/archive/2009/02/19/9434171.aspx
        static class RandomGenerator
        {
            private static readonly Random NonThreadLocalInstance = new Random();

            [ThreadStatic]
            private static Random _threadLocalInstance;

            public static int Next()
            {
                var rnd = _threadLocalInstance;

                if (rnd != null)
                {
                    return rnd.Next();
                }

                int seed;

                lock (NonThreadLocalInstance) seed = NonThreadLocalInstance.Next();

                _threadLocalInstance = rnd = new Random(seed);

                return rnd.Next();
            }
        }
    }
}