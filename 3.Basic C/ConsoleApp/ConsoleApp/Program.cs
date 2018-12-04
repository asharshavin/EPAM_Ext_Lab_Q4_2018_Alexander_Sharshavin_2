using System;
using System.Collections.Generic;
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
            int x = 6;// rnd.Next(1, 13);
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
                        default: Console.WriteLine("задание не выполнено!");  break;
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
        bold = 0x01,
        italic = 0x02,
        underline = 0x04,
        none = 0x00,
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


//class Task7_Sort
//{
//    public static void DoIt()
//    {
//        const int length = 5;
//        int array[length] = { 30, 50, 20, 10, 40 };

//        // Перебираем каждый элемент массива
//        // (кроме последнего, он уже будет отсортирован к тому времени, когда мы до него доберемся)
//        for (int const int length = 5;
//        int array[length] = { 30, 50, 20, 10, 40 };

//        // Перебираем каждый элемент массива
//        // (кроме последнего, он уже будет отсортирован к тому времени, когда мы до него доберемся)
//        for (int startIndex = 0; startIndex < length - 1; ++startIndex)
//        {
//            // В переменной smallestIndex хранится индекс наименьшего значения, которое мы нашли в этой итерации
//            // Начинаем с того, что наименьший элемент в этой итерации - это первый элемент (индекс 0)
//            int smallestIndex = startIndex;

//            // Затем ищем элемент поменьше в остальной части массива
//            for (int currentIndex = startIndex + 1; currentIndex < length; ++currentIndex)
//            {
//                // Если мы нашли элемент, который меньше нашего наименьшего элемента
//                if (array[currentIndex] < array[smallestIndex])
//                    // запоминаем его
//                    smallestIndex = currentIndex;
//            }

//            // smallestIndex теперь наименьший элемент 
//            // меняем местами наше начальное наименьшее число с тем, которое мы обнаружили
//            std::swap(array[startIndex], array[smallestIndex]);
//        }

//        // Теперь, когда весь массив отсортирован - выводим его на экран
//        for (int index = 0; index < length; ++index)
//            std::cout << array[index] << ' ';

//        return 0; = 0; startIndex < length - 1; ++startIndex)
//        {
//            // В переменной smallestIndex хранится индекс наименьшего значения, которое мы нашли в этой итерации
//            // Начинаем с того, что наименьший элемент в этой итерации - это первый элемент (индекс 0)
//            int smallestIndex = startIndex;

//            // Затем ищем элемент поменьше в остальной части массива
//            for (int currentIndex = startIndex + 1; currentIndex < length; ++currentIndex)
//            {
//                // Если мы нашли элемент, который меньше нашего наименьшего элемента
//                if (array[currentIndex] < array[smallestIndex])
//                    // запоминаем его
//                    smallestIndex = currentIndex;
//            }

//            // smallestIndex теперь наименьший элемент 
//            // меняем местами наше начальное наименьшее число с тем, которое мы обнаружили
//            std::swap(array[startIndex], array[smallestIndex]);
//        }

//        // Теперь, когда весь массив отсортирован - выводим его на экран
//        for (int index = 0; index < length; ++index)
//            std::cout << array[index] << ' ';

//        return 0;
//    }

//    private static int Parse(string str, int x)
//    {
//        if (str == null) return -1;
//        if (str == "")
//        {
//            //Console.WriteLine(x);
//            return x;
//        };
//        try
//        {
//            x = int.Parse(str);
//        }
//        catch (FormatException)
//        {
//            Console.WriteLine("Неверный ввод!");
//            return -1;
//        }
//        if (x < 0)
//        {
//            Console.WriteLine("Число не может быть меньше 0!");
//            return -1;
//        }
//        if (x > 1000)
//        {
//            Console.WriteLine("Число не может быть больше 1000!");
//            return -1;
//        }
//        return x;
//    }
//}
