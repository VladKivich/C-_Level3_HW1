using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson6
{
    class Program
    {
        public enum Matrix
        {
            String = 1,
            Column = 0
        }

        /// <summary>
        /// Заполняем матрицу "случайными" цифрами
        /// </summary>
        /// <param name="Strings">Количество строк матрицы</param>
        /// <param name="Columns">Количество столбцов матрицы</param>
        /// <returns></returns>
        public static int[,] FillMatrix(int Strings, int Columns)
        {
            Random Rnd;

            int[,] Matrix = new int[Strings, Columns];

            for (int i = 0; i<Matrix.GetLength(0);i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    //Thread.Sleep(1);
                    Rnd = new Random((int)DateTime.Now.Ticks);
                    Matrix[i, j] = Rnd.Next(1, 10);
                }
            }

            return Matrix;
        }

        /// <summary>
        /// Выводим данные матрица в консоль
        /// </summary>
        /// <param name="Matrix">Матрица для вывода</param>
        public static void PrintMatrix(int[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                string ResultString = "";

                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    ResultString += Matrix[i, j] + "|";
                }
                Console.WriteLine(ResultString);
                Console.WriteLine(new string('-',ResultString.Length));
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Умножаем двумерные матрицы
        /// </summary>
        /// <param name="MatrixFirst">Первая матрица</param>
        /// <param name="MatrixSecond">Вторая матрица</param>
        public static void MultiMatrix(int[,] MatrixFirst, int[,] MatrixSecond)
        {
            if(MatrixFirst.GetLength(1) != MatrixSecond.GetLength(0))
            {
                Console.WriteLine("Умножение невозможно!");
                return;
            }
            else
            {
               Multi(MatrixFirst, MatrixSecond);
            }
        }

        /// <summary>
        /// Умножает две матрицы и возвращает результат
        /// </summary>
        /// <param name="M1">Первая матрица</param>
        /// <param name="M2">Вторая матрица</param>
        /// <returns></returns>
        private static int[,] Multi(int[,] M1, int[,] M2)
        {
            int[,] Result = new int[M1.GetLength(0), M2.GetLength(1)];

            for (int i = 0; i < Result.GetLength(0); i++)
            {
                List<int> IntList = new List<int>(500);

                ParallelLoopResult ParallelResult = Parallel.For(0, Result.GetLength(1), number =>
                {
                    IntList.Add(MatrixCell(GetColumnOrString(M1, i, Matrix.String), GetColumnOrString(M2, number, Matrix.Column)));
                }
                );

                if (ParallelResult.IsCompleted & IntList.Count == 500)
                {
                    for (int j = 0; j < Result.GetLength(1); j++)
                    {
                        Result[i, j] = IntList[j];
                    }
                    IntList.Clear();
                }

                //for (int j = 0; j < Result.GetLength(1); j++)
                //{
                //    Result[i, j] = MatrixCell(GetColumnOrString(M1, i, Matrix.String), GetColumnOrString(M2, j, Matrix.Column));
                //}

            }
            return Result;
        }
        

        /// <summary>
        /// Получаем нужный столбец или строку
        /// </summary>
        /// <param name="Array">Двумерная исходная матрица</param>
        /// <param name="StartIndex">Стартовый индекс строки или столбца</param>
        /// <param name="Demension">Выбранное измерение</param>
        /// <returns></returns>
        private static int[] GetColumnOrString(int[,] Array, int StartIndex, Matrix Demension)
        {
            int[] Result = new int[Array.GetLength((int)Demension)];

            for (int i = 0; i < Array.GetLength((int)Demension); i++)
            {
                if((int)Demension == 1)
                {
                    Result[i] = Array[StartIndex, i];
                }
                else
                {
                    Result[i] = Array[i,StartIndex];
                }
            }

            return Result;
        }

        /// <summary>
        /// Получаем значение ячейки матрицы
        /// </summary>
        /// <param name="String">Строка</param>
        /// <param name="Column">Столбец</param>
        /// <returns></returns>
        private static int MatrixCell(int [] String, int [] Column)
        {
            int Result = 0;

            for (int i = 0; i < String.Length; i++)
            {
                Result += (String[i] * Column[i]);
            }
            return Result;
        }

        static void Main(string[] args)
        {
            #region Task_1
            //1. Даны 2 двумерных матрицы. Размерность 100х100 каждая. Напишите приложение, производящее параллельное умножение матриц. 
            //Матрицы заполняются случайными целыми числами от 0 до10.

            DateTime Start = DateTime.Now;

            int[,] M1 = FillMatrix(500, 500);

            int[,] M2 = FillMatrix(500, 500);

            //PrintMatrix(M1); PrintMatrix(M2);

            MultiMatrix(M1, M2);

            TimeSpan Result = DateTime.Now - Start;

            Console.WriteLine($"Seconds: {Result.TotalSeconds}");

            #endregion

            Console.ReadLine();
        }
    }
}
