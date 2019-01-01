using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] myArray = new string[15];
            FillRandom(myArray);
            Console.WriteLine("Первоначальный массив:");
            Print(myArray);
            Sort(myArray, IsSmaller);
            Console.WriteLine();
            Console.WriteLine("Отсортированный массив: ");
            Print(myArray);
            Console.WriteLine();
            Console.ReadLine();
        }

        delegate bool Operation(string Value1, string Value2);

        static bool IsSmaller(string Value1, string Value2)
        {
            if (Value1.Length == Value2.Length)
                return String.Compare(Value1, Value2) < 0;
            else
                return Value1.Length < Value2.Length;
        }

        static void Print(string[] a)
        {
            foreach (string elem in a)
                Console.WriteLine("{0} ", elem);
        }

        static void FillRandom(string[] array)
        {
            array[0] = "По мостовой";
            array[1] = "моей души изъезженной";
            array[2] = "шаги помешанных";
            array[3] = "вьют жестких фраз пяты.";
            array[4] = "Где города";
            array[5] = "повешены";
            array[6] = "и в петле облака";
            array[7] = "застыли";
            array[8] = "башен";
            array[9] = "кривые выи —";
            array[10] = "иду";
            array[11] = "один рыдать,";
            array[12] = "что перекрестком";
            array[13] = "распяты";
            array[14] = "городовые.";
        }

        static void Sort(string[] array, Operation op)
        {
            string temp;
            for (int i = 1; i < array.Length; i++)
            {
                int j = i;
                while (j >= 1 && op.Invoke(array[j], array[j - 1]))
                {
                    temp = array[j];
                    array[j] = array[j - 1];
                    array[j - 1] = temp;
                    j--;
                }
            }
        }
    }

    /*
    class Program
    {
        static void Main(string[] args)
        {
            int[] myArray = new int[10];
            FillRandom(myArray);
            Console.WriteLine("Первоначальный массив:");
            Print(myArray);
            Sort(myArray, IsSmaller);
            Console.WriteLine("Отсортированный массив: ");
            Print(myArray);
            Console.WriteLine();
            Console.ReadLine();
        }

        delegate bool Operation(int Value1, int Value2);

        static bool IsSmaller(int Value1, int Value2)
        {
            return Value1 < Value2;
        }

        static void Print(int[] a)
        {
            foreach (int elem in a)
                Console.Write("{0,3} ", elem);
        }
        static void FillRandom(int[] a)
        {
            Random rnd = new Random();

            for (int i = 0; i < a.GetLength(0); i++)
            {
                a[i] = rnd.Next(-99, 99);
            }
        }

        static void Sort(int[] array, Operation op)
        {
            int temp;
            for (int i = 1; i < array.Length; i++)
            {
                int j = i;
                while (j >= 1 && op.Invoke(array[j], array[j - 1]))
                {
                    temp = array[j];
                    array[j] = array[j - 1];
                    array[j - 1] = temp;
                    j--;
                }
            }
        }
    }
    */
}
