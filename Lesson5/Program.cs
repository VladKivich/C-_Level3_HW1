using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson5
{
    class Program
    {
        public static class Results
        {
            public static Factorial F1 { get; set; }
            public static Factorial F2 { get; set; }

            public static void PrintResult()
            {
                if(F1 != null)
                {
                    Console.WriteLine(F1.Result);
                }
                if (F2 != null)
                {
                    Console.WriteLine(F2.Result);
                }
            }
        }

        public class ParsingExample
        {
            public List<string> Strings { get; set; }
            public string Parsing { get; set; }

            public ParsingExample(List<string> Strings, string Parsing)
            {
                this.Strings = Strings;
                this.Parsing = Parsing;
            }
        }

        public class Factorial
        {
            public ulong Number { get; set; }
            public ulong Result { get; set; }

            public Factorial(ulong Number)
            {
                this.Number = Number;
                this.Result = Number;
            }
        }

        static void FactorialMethod(object factorial)
        {
            lock(factorial)
            {
                if (factorial is Factorial == false) return;
                Factorial F = factorial as Factorial;
                if (F.Number > 1)
                {
                    F.Result *= --F.Number;
                    FactorialMethod(F);
                    return;
                }
                else
                {
                    Results.F1 = F;
                }
            }
        }

        static void AmountMethod(object factorial)
        {
            lock (factorial)
            {
                if (factorial is Factorial == false) return;
                Factorial F = factorial as Factorial;
                if (F.Number > 0)
                {
                    F.Result += --F.Number;
                    AmountMethod(F);
                    return;
                }
                else
                {
                    Results.F2 = F;
                }
            }
        }

        static ulong NumberInput()
        {
            ulong N = 0;
            do
            {
                Console.Write("Введите число: ");
                UInt64.TryParse(Console.ReadLine(), out N);
            }
            while (N <= 0);
            return N;
        }

        static void FindAndWrite(object str)
        {
            lock (str)
            {
                string path = @"Files\Test.txt";

                ParsingExample PE = (ParsingExample)str;

                using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                {
                    while(PE.Strings.Find(e => e.Contains(PE.Parsing)) != null)
                    {
                        string temp = PE.Strings.Find(e => e.Contains(PE.Parsing));
                        sw.WriteLine(temp);
                        PE.Strings.Remove(temp);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(5, 5);
            ThreadPool.SetMinThreads(2, 2);

            #region Задание_1

            ////а) факториал числа N, которое вводится с клавиатуры;

            //Factorial F = new Factorial(NumberInput());
            //Factorial F1 = new Factorial(F.Number);

            //Thread A = new Thread(FactorialMethod);
            //A.Start(F);

            ////b) сумму целых чисел до N;

            //Thread B = new Thread(AmountMethod);
            //B.Start(F1);

            //B.Join();
            //A.Join();

            //Results.PrintResult();

            #endregion

            #region Задание_2

            //Написать приложение, выполняющее парсинг CSV-файла произвольной структуры и
            //сохраняющего его в обычный txt - файл.Все операции проходят в потоках. CSV - файл заведомо
            //имеет большой объем.

            //string path = @"Files\vacancy.csv";

            //List<string> SL = new List<string>();

            //try
            //{
            //    Console.WriteLine("Read File!");
            //    using (StreamReader sr = new StreamReader(path))
            //    {
            //        while(sr.ReadLine() != null)
            //        {
            //            SL.Add(sr.ReadLine());
            //        }
            //    }
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //ParsingExample PE = new ParsingExample(SL, "Повар");

            //for (int i = 0; i < 4; i++)
            //{
            //    lock(PE)
            //    {
            //        ThreadPool.QueueUserWorkItem(FindAndWrite, PE);
            //    }
            //}

            #endregion

            Console.ReadLine();
        }
    }
}
