using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создание объекта для генерации чисел
            Random rnd = new Random();
            int x = 1;// rnd.Next(1, 13);
            string str = "";
            do
            {
                Console.Write("Введите номер задания 1-13, 0-выход (по умолчанию <{0}>):", x);
                str = Console.ReadLine();
                {
                    if (str != "")
                    {
                        try
                        {
                            x = int.Parse(str);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод!");
                        }
                    }
                    switch (x)
                    {
                        case 1: Task1_Square.DoIt(); break;
                        case 2: Task2_Triangle.DoIt(); break;
                        case 3: Task3_Elka.DoIt(); break;
                        case 4: Task4_Elka_Elka.DoIt(); break;
                        case 5: Task5_Summa.DoIt(); break;
                        case 6: Task6_Shrift.DoIt(); break;
                        case 7: Task7_Sort.DoIt(); break;
                        case 8: Task8_3dArray.DoIt(); break;
                        case 9: Task9_1dArray.DoIt(); break;
                        case 10: Task10_2dArray.DoIt(); break;
                        case 11: Task11_AveragLenthOfWord.DoIt(); break;
                        case 12: Task12_DoubleTrouble.DoIt(); break;
                        case 13: Task13_VincentAndJules.DoIt(); break;
                        default: Console.WriteLine("задание отсутствует!");  break;
                    }
                }
            } while (str != null || x != 0);

        }

    }
    class Task1_Square
    {
        public static void DoIt()
        {
            //Создание объекта для генерации чисел
            Random rnd = new Random();

            //Получить случайное число (в диапазоне от 0 до ...)
            int a = rnd.Next(1, int.MaxValue);
            int b = rnd.Next(1, int.MaxValue);

            Console.WriteLine("Определим площадь прямоугольника. Введите стороны прямоугольника (a и b)");
            Console.Write("a (по умолчанию <{0}>) = ", a);
            string str = Console.ReadLine();
            a = Parse(str, a);
            if (a < 0) return;

            Console.Write("b  (по умолчанию <{0}>) = ", b);
            str = Console.ReadLine();
            b = Parse(str, b);
            if (b < 0) return;

            Console.WriteLine("Площадь прямоугольника: {0}", (long)a * b);
        }

        private static int Parse(string str, int x)
        {
            if (str == null) return -1;
            if (str == "")
            {
                //Console.WriteLine(x);
                return x;
            };
            try
            {
                x = int.Parse(str);
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный ввод!");
                return -1;
            }
            if (x < 0)
            {
                Console.WriteLine("Число не может быть меньше 0!");
                return -1;
            }
            return x;
        }

        private static bool IsCorrect(string str, int x)
        {
            if (str == null) return false;
            if (str == "")
            {
                //Console.WriteLine(x);
                return true;
            };
            try
            {
                x = int.Parse(str);
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный ввод!");
                return false;
            }
            if (x < 0)
            {
                Console.WriteLine("Число не может быть меньше 0!");
                return false;
            }
            return true;
        }
    }

    class Task2_Triangle
    {
        public static void DoIt()
        {
            //Создание объекта для генерации чисел
            Random rnd = new Random();

            //Получить случайное число (в диапазоне от 0 до ...)
            int n = rnd.Next(1, 50);

            Console.WriteLine("Нарисуем триугольник. Введите количество строк от 1 до 50 (n)");
            Console.Write("n (по умолчанию <{0}>) = ", n);
            string str = Console.ReadLine();
            n = Parse(str, n);
            if (n < 0) return;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++) Console.Write("*");
                Console.WriteLine();
            }
        }

        private static int Parse(string str, int x)
        {
            if (str == null) return -1;
            if (str == "")
            {
                //Console.WriteLine(x);
                return x;
            };
            try
            {
                x = int.Parse(str);
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный ввод!");
                return -1;
            }
            if (x < 0)
            {
                Console.WriteLine("Число не может быть меньше 0!");
                return -1;
            }
            if (x > 50)
            {
                Console.WriteLine("Число не может быть больше 50!");
                return -1;
            }
            return x;
        }
    }

    class Task3_Elka
    {
        public static void DoIt()
        {
            //Создание объекта для генерации чисел
            Random rnd = new Random();

            //Получить случайное число (в диапазоне от 0 до ...)
            int n = rnd.Next(1, 50);

            Console.WriteLine("Нарисуем ёлку. Введите количество строк от 1 до 50 (n)");
            Console.Write("n (по умолчанию <{0}>) = ", n);
            string str = Console.ReadLine();
            n = Parse(str, n);
            if (n < 0) return;

            for (int i = 0; i < n; i++)
            {
                //for (int j = 0; j <= i; j++) Console.Write("*");
                for (int j = 0; j < n - i; j++) Console.Write(" ");
                for (int j = 0; j < i * 2 + 1; j++) Console.Write("*");
                Console.WriteLine();
            }
        }

        private static int Parse(string str, int x)
        {
            if (str == null) return -1;
            if (str == "")
            {
                //Console.WriteLine(x);
                return x;
            };
            try
            {
                x = int.Parse(str);
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный ввод!");
                return -1;
            }
            if (x < 0)
            {
                Console.WriteLine("Число не может быть меньше 0!");
                return -1;
            }
            if (x > 50)
            {
                Console.WriteLine("Число не может быть больше 50!");
                return -1;
            }
            return x;
        }
    }

    class Task4_Elka_Elka
    {
        public static void DoIt()
        {
            //Создание объекта для генерации чисел
            Random rnd = new Random();

            //Получить случайное число (в диапазоне от 0 до ...)
            int n = rnd.Next(1, 50);

            Console.WriteLine("Нарисуем ёлку. Введите количество триугольников ёлки от 1 до 50 (n)");
            Console.Write("n (по умолчанию <{0}>) = ", n);
            string str = Console.ReadLine();
            n = Parse(str, n);
            if (n < 0) return;

            for (int m = 0; m < n; m++)
                for (int i = 0; i <= m; i++)
                {
                    for (int j = 0; j < n - i; j++) Console.Write(" ");
                    for (int j = 0; j < i * 2 + 1; j++) Console.Write("*");
                    Console.WriteLine();
                }
        }

        private static int Parse(string str, int x)
        {
            if (str == null) return -1;
            if (str == "")
            {
                //Console.WriteLine(x);
                return x;
            };
            try
            {
                x = int.Parse(str);
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный ввод!");
                return -1;
            }
            if (x < 0)
            {
                Console.WriteLine("Число не может быть меньше 0!");
                return -1;
            }
            if (x > 50)
            {
                Console.WriteLine("Число не может быть больше 50!");
                return -1;
            }
            return x;
        }
    }
}

class Task5_Summa
{
    public static void DoIt()
    {
        int[] b = { 3, 5 };
        int sum = 0;

        //Создание объекта для генерации чисел
        Random rnd = new Random();

        //Получить случайное число (в диапазоне от 0 до ...)
        int n = rnd.Next(1, 1000);

        Console.WriteLine("Посчитаем сумму чисел кратных 3 и 5. Введите макс число (1-1000) (n)");
        Console.Write("n (по умолчанию <{0}>) = ", n);
        string str = Console.ReadLine();
        n = Parse(str, n);
        if (n < 0) return;

        for (int i = 1; i < n; i++)
        {
            foreach (int elem in b)
                if ((i % elem) == 0)
                {
                   sum += i;
                  //Console.WriteLine(i);
                }
        }
        Console.WriteLine("Сумма равна {0}", sum);
    }

    private static int Parse(string str, int x)
    {
        if (str == null) return -1;
        if (str == "")
        {
            //Console.WriteLine(x);
            return x;
        };
        try
        {
            x = int.Parse(str);
        }
        catch (FormatException)
        {
            Console.WriteLine("Неверный ввод!");
            return -1;
        }
        if (x < 0)
        {
            Console.WriteLine("Число не может быть меньше 0!");
            return -1;
        }
        if (x > 1000)
        {
            Console.WriteLine("Число не может быть больше 1000!");
            return -1;
        }
        return x;
    }
}

class Task6_Shrift
{
    [Flags]
    public enum Shrift
    {
        bold = 0x01, //жирный 
        italic = 0x02, //курсив
        underline = 0x04, //подчеркивание
        none = 0x00, //ничего
    }
    public static void DoIt()
    {
        Shrift myShrift = Shrift.none;

        string str = "";
        do
        {
            string strShrift = "";
            if ((myShrift & Shrift.bold) != Shrift.none) strShrift += " Bold";
            if ((myShrift & Shrift.italic) != Shrift.none) strShrift += " Italic";
            if ((myShrift & Shrift.underline) != Shrift.none) strShrift += " Underline";
            Console.WriteLine("Параметры надписи: {0}", strShrift);
            Console.WriteLine("Введите:");
            Console.WriteLine("\t 1: bold");
            Console.WriteLine("\t 2: italic");
            Console.WriteLine("\t 3: underline");

            str = Console.ReadLine();
            switch (str)
            { case "1": myShrift = myShrift ^ Shrift.bold; break; 
             case "2": myShrift = myShrift ^ Shrift.italic; break; 
             case "3": myShrift = myShrift ^ Shrift.underline; break;
                default: break;
            }
        } while (str != null || str != "");
} }


class Task7_Sort
{
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

    public static void DoIt()
    {
        //инициализируем генератор случайных чисел
        int[] myArray = new int[10];
        FillRandom(myArray);
        Console.WriteLine("Первоначальный массив:");
        Print(myArray);
        Sort(myArray);
        Console.WriteLine("Отсортированный массив: ");
        Print(myArray);
        Console.WriteLine();
    }

    static void Sort(int[] a)
    {
        int temp;
        for (int i = 1; i < a.Length; i++)
        {
            int j = i;
            while (j >= 1 && a[j] < a[j - 1]) //1
            {
                temp = a[j];
                a[j] = a[j - 1];
                a[j - 1] = temp;
                j--;
            }
        }
    }
}

class Task8_3dArray
{
    static void Print(int[,,] a)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                for (int k = 0; k < a.GetLength(2); k++)
                {
                    Console.Write("{0,3} ", a[i, j, k]);
                }
                Console.Write("\t ");
            }
            Console.WriteLine();
        }
    }
    static void FillRandom(int[,,] a)
    {
        Random rnd = new Random();

        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                for (int k = 0; k < a.GetLength(2); k++)
                {
                    a[i, j, k] = rnd.Next(-99, 99);
                }
            }
        }
    }

    static void ChangePositive(int[,,] a)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                for (int k = 0; k < a.GetLength(2); k++)
                {
                    if (a[i, j, k] > 0) a[i, j, k] = 0;
                }
            }
        }
    }


    public static void DoIt()
    {
        //инициализируем генератор случайных чисел
        int[,,] myArray = new int[10, 10, 10];
        FillRandom(myArray);
        Console.WriteLine("Первоначальный массив:");
        Print(myArray);
        ChangePositive(myArray);
        Console.WriteLine("После замены положительных на нули:");
        Print(myArray);
    }
}

class Task9_1dArray
{
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

    static long SummaPositive(int[] a)
    {
        long sum = 0;

        foreach (int elem in a)
            if (elem > 0) sum += elem;

        return sum;
    }


    public static void DoIt()
    {
        //инициализируем генератор случайных чисел
        int[] myArray = new int[10];
        FillRandom(myArray);
        Console.WriteLine("Первоначальный массив:");
        Print(myArray);
        Console.WriteLine("Сумма положительных элементов массива: {0}", SummaPositive(myArray));
    }
}

class Task10_2dArray
{
    static void Print(int[,] a)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                 Console.Write("{0,3} ", a[i, j]);
            }
            Console.WriteLine();
        }
    }
    static void FillRandom(int[,] a)
    {
        Random rnd = new Random();

        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                a[i, j] = rnd.Next(-99, 99);
            }
        }
    }

    static long SummaEvenIndexes(int[,] a)
    {
        long sum = 0;

        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                if ((i+j) % 2 == 0) sum += a[i, j];
            }
        }
        return sum;
    }


    public static void DoIt()
    {
        //инициализируем генератор случайных чисел
        int[,] myArray = new int[4, 4];
        FillRandom(myArray);
        Console.WriteLine("Первоначальный массив:");
        Print(myArray);
        Console.WriteLine("Сумма элементов массива с четной суммой индексов: {0}", SummaEvenIndexes(myArray));
    }
}

class Task11_AveragLenthOfWord
{
    public static void DoIt()
    {
        string poems = "Написать программу, которая определяет среднюю длину слова во введенной текстовой строке. Учесть, что символы пунктуации на длину слов влиять не должны. Регулярные выражения не использовать. И не пытайтесь прописать все ручками. Используйте стандартные методы класса String.";
        Console.WriteLine("Введите предложение, для которого будет подсчитана средняя длина слова. ");
        Console.WriteLine("По умолчанию <{0}>", poems);
        string str = Console.ReadLine();
        if (str != "") poems = str;
        char[] div = { ' ', '.', ',', '?', '!' }; //создаем массив разделителей
        
        // Разбиваем строку на части
        string[] parts = poems.Split(div, StringSplitOptions.RemoveEmptyEntries);
        string whole = String.Join("", parts);

        if (parts.Length>0) Console.WriteLine("Средняя длина слова: "+(whole.Length / parts.Length));
    }
}

class Task12_DoubleTrouble
{
    public static void DoIt()
    {
        string str1 = "написать программу, которая";
        string str2 = "описание";
        string str = string.Empty;

        Console.WriteLine("Введите первую строку, по умолчанию <{0}>", str1);
        str = Console.ReadLine();
        if (str != "") str1 = str;

        Console.WriteLine("Введите вторую строку, по умолчанию <{0}>", str2);
        str = Console.ReadLine();
        if (str != "") str2 = str;

        StringBuilder strResult = new StringBuilder(str1);

        for (int i=0; i < str2.Length; i++)
        {
            string charChange = str2.Substring(i, 1);
            strResult = strResult.Replace(charChange, charChange + charChange);
        }

        Console.WriteLine("Результирующая строка: "+ strResult);
    }
}

class Task13_VincentAndJules
{
    public static void DoIt()
    {
        string str = "";
        StringBuilder sb = new StringBuilder();

        Stopwatch stopWatch = new Stopwatch();

        int N = 1;
        int Max = 1000000;

        while (N<=Max)
        {
            Console.Write("{0, 15}  ", N);

            stopWatch.Start();

            for (int i = 0; i < N; i++)
            {
                str += "*";
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            Console.Write("" + elapsedTime + "  ");

            stopWatch.Reset();

            stopWatch.Start();

            for (int i = 0; i < N; i++)
            {
                sb.Append("*");
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            Console.Write("  " + elapsedTime);

            Console.WriteLine();

            N = N * 10;
        }
    }
}

