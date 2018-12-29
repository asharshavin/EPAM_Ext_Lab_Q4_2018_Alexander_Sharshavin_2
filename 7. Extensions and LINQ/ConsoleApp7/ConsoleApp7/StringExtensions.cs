using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public static class StringExtensions
    {
        public static bool IsPositive(this string stringNumber)
        {
            for (int i = 0; i < stringNumber.Length; i++)
                if (!char.IsDigit(stringNumber[i]))
                    return false;

            return true;
        }

        private static bool IsNumber(char symbol) => char.IsDigit(symbol);
    }
}
