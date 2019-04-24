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
            public int Number { get; set; }
            public int Result { get; set; }

            public Factorial(int Number)
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
                    Console.WriteLine("Факториал: {0}", F.Result);
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
                    Console.WriteLine("Сумма целых чисел: {0}", F.Result);
                }
            }
        }

        static int NumberInput()
        {
            int N = 0;
            do
            {
                Console.Write("Введите число: ");
                Int32.TryParse(Console.ReadLine(), out N);
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

            //а) факториал числа N, которое вводится с клавиатуры;

            Factorial F = new Factorial(NumberInput());
            Factorial F1 = new Factorial(F.Number);

            ThreadPool.QueueUserWorkItem(FactorialMethod, F);

            //b) сумму целых чисел до N;

            ThreadPool.QueueUserWorkItem(AmountMethod, F1);
            
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
