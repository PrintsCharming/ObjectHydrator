using System;

namespace Foundation.ObjectHydrator
{
    public sealed class RandomSingleton
    {
        private static readonly object SyncRoot = new object();
        private static volatile RandomSingleton instance;

        private RandomSingleton()
        {
            Random = new Random();
        }

        public Random Random { get; }

        public static RandomSingleton Instance
        {
            get
            {
                if (instance == null)
                    lock (SyncRoot)
                    {
                        if (instance == null)
                            instance = new RandomSingleton();
                    }

                return instance;
            }
        }
    }
}