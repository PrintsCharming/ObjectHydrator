using System.Text;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class PasswordGenerator:IGenerator<string>
    {
        private char[] legalchars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()".ToCharArray();
        int length;

        public PasswordGenerator()
            : this(10)
        { }

        public PasswordGenerator(int pwlength)
        {
            length = pwlength;
        }

        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(legalchars[RandomSingleton.Instance.Random.Next(0, legalchars.Length - 1)]);
            }
            return sb.ToString();
        }



    }
}
