using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class PasswordGenerator : IGenerator<string>
    {
        private readonly char[] _legalchars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()".ToCharArray();

        private readonly int _length;

        public PasswordGenerator()
            : this(10)
        {
        }

        public PasswordGenerator(int pwlength)
        {
            _length = pwlength;
        }

        public string Generate()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _length; i++)
                sb.Append(_legalchars[RandomSingleton.Instance.Random.Next(0, _legalchars.Length - 1)]);
            return sb.ToString();
        }
    }
}