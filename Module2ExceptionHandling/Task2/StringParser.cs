using System;
using System.Linq;

namespace Task2
{
    public static class StringParser
    {
        public static int StrToInt(string str)
        {
            int number = 0;
            bool positive = true;

            switch (str[0])
            {
                case '+':
                    str = str.Remove(0, 1);
                    positive = true;
                    break;
                case '-':
                    str = str.Remove(0, 1);
                    positive = false;
                    break;
            }

            int length = str.Count();

            for (int i = 0; i < length; i++)
            {
                var strNumber = str[i] - '0';
                if (strNumber > 9 || strNumber < 0)
                {
                    throw new InvalidCastException("String doesn't contain number.");
                }
                number += strNumber * (int)Math.Pow(10, length - i - 1);
            }

            return positive ? number : -number;
        }
    }
}
