using System;

namespace Foundation.ObjectHydrator
{
    public sealed class RandomSingleton
    {
        private static readonly object SyncRoot = new object();
        private static volatile RandomSingleton _instance;

        private RandomSingleton()
        {
            Random = new Random();
        }

        public Random Random { get; }

        public static RandomSingleton Instance
        {
            get
            {
                if (_instance == null)
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new RandomSingleton();
                    }

                return _instance;
            }
        }
    }
}