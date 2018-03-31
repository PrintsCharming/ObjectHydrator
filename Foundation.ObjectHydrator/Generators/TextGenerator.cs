using System;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class TextGenerator : IGenerator<string>
    {
        private static readonly string sampleText =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean non enim felis. Donec et leo vel lacus fermentum luctus ut in metus. Vivamus sagittis lacus vel mi sagittis sit amet molestie eros faucibus. Maecenas diam metus, scelerisque sed imperdiet nec, dignissim in dui. Aliquam erat volutpat. Maecenas malesuada gravida leo ac porttitor. Aliquam sed purus sit amet nisl ultrices accumsan at non ante. Duis lobortis, leo et viverra vestibulum, eros metus imperdiet justo, vel feugiat mi metus suscipit enim. Donec sed dui mi, vehicula malesuada leo. In pellentesque velit et diam aliquam vel facilisis metus faucibus. Curabitur a ipsum nulla. Suspendisse vel mi vel lacus fermentum rhoncus eget vestibulum ante. Morbi dictum sem id dui vulputate bibendum. Fusce quis faucibus leo. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Suspendisse at malesuada mi."
            ;

        private int _length;
        private readonly Random random;

        public TextGenerator()
            : this(25)
        {
        }

        public TextGenerator(int length)
        {
            Length = length;
            random = RandomSingleton.Instance.Random;
        }

        public int Length
        {
            get => _length;
            set => _length = value > sampleText.Length ? sampleText.Length : value;
        }

        #region IGenerator Members

        public string Generate()
        {
            return sampleText.Substring(0, random.Next(1, Length - 1)).Trim();
        }

        #endregion
    }
}