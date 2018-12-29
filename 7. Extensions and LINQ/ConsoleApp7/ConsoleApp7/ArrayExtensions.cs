using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public static class ArrayExtensions
    {
        public static int Summa(this int[] array)
        {
            int result = 0;
            foreach (int value in array)
                result += value;

            return result;
        }
    }
}
