using System;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp7
{
    internal class Program
    {
        public delegate int[] Function(int[] array);

        private static void Main(string[] args)
        {
            int numberOfTask = 1;
            string strNumberOfTask = string.Empty;
            do
            {
                Console.Write("Введите номер задания 1-3, 0-выход (по умолчанию <{0}>):", numberOfTask);
                strNumberOfTask = Console.ReadLine();
                {
                    if (strNumberOfTask != string.Empty)
                    {
                        try
                        {
                            numberOfTask = int.Parse(strNumberOfTask);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод!");
                        }
                    }
                    switch (numberOfTask)
                    {
                        case 1: Task1_SumArray(); break;
                        case 2: Task2_TestString(); break;
                        case 3: Task3_FindInArray(); break;
                        default: Console.WriteLine("задание отсутствует!"); break;
                    }
                }
            } while (strNumberOfTask != null || numberOfTask != 0);
        }

        public static void Task1_SumArray()
        {
            Console.Write("Введите не отрицательное целое число: ");
            string stringNumber = Console.ReadLine();
            if (stringNumber != null)
            {
                Console.WriteLine(stringNumber.IsPositive() ? "Все верно" : "Ошибка ввода числа");
            }

            Console.ReadLine();
        }

        public static void Task2_TestString()
        {
            int[] arrayOfNumber = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            Console.Write("Массив: ");
            Print(arrayOfNumber);
            Console.WriteLine();
            Console.WriteLine("Сумма элементов массива: {0}", arrayOfNumber.Summa());
            Console.ReadLine();
        }

        public static void Task3_FindInArray()
        {

            var stopWatch = new Stopwatch();

            int currentLengthArray = 1;
            int cycles = 100;
            int maxLengthArray = 10000000;

            Console.Write("{0, 15}", "Размер массива  ");
            Console.Write("{0, 13}", "1. Прямой метод");
            Console.Write("{0, 13}", "2. Делегат");
            Console.Write("{0, 13}", "3. Аноним. мет.");
            Console.Write("{0, 13}", "4. Лямбда выражени");
            Console.Write("{0, 13}", "5. LINQ выражения");
            Console.WriteLine();


            while (currentLengthArray <= maxLengthArray)
            {
                Console.Write("{0, 15}  ", currentLengthArray);

                int[] arrayOfNumber = new int[currentLengthArray];
                FillRandom(arrayOfNumber);


                // 1. Прямой метод
                StartTimer(stopWatch);
                for (int i = 0; i < cycles; i++)
                {
                    FindInArray(arrayOfNumber);
                }

                StopTimer(stopWatch);

                // 2. Через делегат
                var FindPositive = new Function(FindInArray);
                for (int i = 0; i < cycles; i++)
                {
                    FindPositive.Invoke(arrayOfNumber);
                }

                StopTimer(stopWatch);

                // 3. Через делегат в виде анонимного метода
                StartTimer(stopWatch);
                Function FindPositiveAnon = delegate(int[] array)
                {
                    var result = new int[array.Length];
                    int i = 0;
                    foreach (int elem in array)
                    {
                        if (elem > 0)
                        {
                            result[i] = elem;
                            i++;
                        }
                    }

                    return result;
                };
                for (int i = 0; i < cycles; i++)
                {
                    FindPositiveAnon.Invoke(arrayOfNumber);
                }

                StopTimer(stopWatch);

                // 4. Через делегат в виде лямбда выражения
                StartTimer(stopWatch);
                Function FindPositiveLambda = (int[] array) =>
                {
                    var result = new int[array.Length];
                    int i = 0;
                    foreach (int elem in array)
                    {
                        if (elem > 0)
                        {
                            result[i] = elem;
                            i++;
                        }
                    }

                    return result;
                };
                for (int i = 0; i < cycles; i++)
                {
                    FindPositiveLambda.Invoke(arrayOfNumber);
                }

                StopTimer(stopWatch);

                // 5. Через LINQ выражения
                var count = 0;
                StartTimer(stopWatch);
                var arrayResult = from elem in arrayOfNumber where elem > 0 select elem;
                for (int i = 0; i < cycles; i++)
                {
                    count = arrayResult.Count();
                }

                StopTimer(stopWatch);

                Console.WriteLine();

                currentLengthArray = currentLengthArray * 10;
            }
            Console.ReadLine();
        }

        private static void Print(int[] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.Write("{0,3} ", array[i]);
            }
        }

        private static void StartTimer(Stopwatch stopWatch)
        {
            stopWatch.Reset();
            stopWatch.Start();
        }

        private static void StopTimer(Stopwatch stopWatch)
        {
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format(
                "{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, 
                ts.Minutes, 
                ts.Seconds,
                ts.Milliseconds);
            Console.Write(elapsedTime + "    ");
        }

        private static void FillRandom(int[] a)
        {
            Random rnd = new Random();

            for (int i = 0; i < a.GetLength(0); i++)
            {
                a[i] = rnd.Next(-99, 99);
            }
        }

        public static int[] FindInArray(int[] array)
        {
            var result = new int[array.Length];
            int i = 0;
            foreach (int elem in array)
            {
                if (elem > 0)
                {
                    result[i] = elem;
                    i++;
                }
            }

            return result;
        }

    }
}
