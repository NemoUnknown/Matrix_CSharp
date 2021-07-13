using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary
{
    public class Matrix<T> where T : struct
    {

        public int Rows { get; }
        public int Columns { get; }
        private T[,] table;

        public Matrix(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;

            table = new T[rows, columns];
        }

        public Matrix(T[,] array2D)
        {
            this.Rows = array2D.GetLength(0);
            this.Columns = array2D.GetLength(1);

            table = array2D;
        }

        public int ElementCount => Rows * Columns;

        public void AddItem(T item, int row, int col)
        {
            table[row, col] = item;
        }

        static public Random rand = new Random();
        public static Matrix<T> RandomizeInt(int rows, int cols)
        {
            Matrix<T> matrix = new Matrix<T>(rows, cols);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                    matrix.AddItem((T)Convert.ChangeType(rand.Next(), typeof(T)), i, j);

            }

            return matrix;
        }

        public static Matrix<T> RandomizeInt(int rows, int cols, int maxValue)
        {
            Matrix<T> matrix = new Matrix<T>(rows, cols);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                    matrix.AddItem((T)Convert.ChangeType(rand.Next(maxValue), typeof(T)), i, j);

            }

            return matrix;
        }

        public static Matrix<T> RandomizeInt(int rows, int cols, int minValue, int maxValue)
        {
            Matrix<T> matrix = new Matrix<T>(rows, cols);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                    matrix.AddItem((T)Convert.ChangeType(rand.Next(minValue, maxValue), typeof(T)), i, j);

            }

            return matrix;
        }

        public static Matrix<T> RandomizeDouble(int rows, int cols)
        {
            Matrix<T> matrix = new Matrix<T>(rows, cols);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                    matrix.AddItem((T)Convert.ChangeType(rand.NextDouble(), typeof(T)), i, j);

            }

            return matrix;
        }

        public T this[int row, int col]
        {
            get => table[row, col];
            set => table[row, col] = value;
        }

        public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.Rows != m2.Rows && m1.Columns != m2.Columns)
                throw new InvalidOperationException("Ошибка! Не равное количество строк и столбцов");
            Matrix<T> sum = new Matrix<T>(m1.Rows, m1.Columns);
            for (int i = 0; i < sum.Rows; i++)
            {
                for (int j = 0; j < sum.Columns; j++)
                {
                    dynamic dm1 = m1[i, j];
                    dynamic dm2 = m2[i, j];
                    sum[i, j] = dm1 + dm2;
                }
            }
            return sum;
        }

        public static Matrix<T> operator -(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.Rows != m2.Rows && m1.Columns != m2.Columns)
                throw new InvalidOperationException("Ошибка! Не равное количество строк и столбцов");
            Matrix<T> sub = new Matrix<T>(m1.Rows, m1.Columns);
            for (int i = 0; i < sub.Rows; i++)
            {
                for (int j = 0; j < sub.Columns; j++)
                {
                    dynamic dm1 = m1[i, j];
                    dynamic dm2 = m2[i, j];
                    sub[i, j] = dm1 - dm2;
                }
            }
            return sub;
        }

        public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.Columns != m2.Rows)
                throw new InvalidOperationException("Ошибка! Матрицы не совместимы");
            Matrix<T> mult = new Matrix<T>(m1.Rows, m2.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Columns; j++)
                {
                    for (int k = 0; k < m1.Columns; k++)
                    {
                        dynamic dm1 = m1[i, k];
                        dynamic dm2 = m2[k, j];
                        mult.table[i, j] += dm1 * dm2;
                    }
                }
            }
            return mult;
        }

        public static Matrix<T> operator *(Matrix<T> matrix, int multiplier)
        {
            Matrix<T> mult = new Matrix<T>(matrix.Rows, matrix.Columns);
            for (int i = 0; i < mult.Rows; i++)
            {
                for (int j = 0; j < mult.Columns; j++)
                {
                    dynamic dMatrix = matrix[i, j];
                    mult.table[i, j] = dMatrix * multiplier;
                }
            }
            return mult;
        }

        public static Matrix<T> operator *(int multiplier, Matrix<T> matrix)
        {
            Matrix<T> mult = new Matrix<T>(matrix.Rows, matrix.Columns);
            for (int i = 0; i < mult.Rows; i++)
            {
                for (int j = 0; j < mult.Columns; j++)
                {
                    dynamic dMatrix = matrix[i, j];
                    mult.table[i, j] = multiplier * dMatrix;
                }
            }
            return mult;
        }

        public static Matrix<T> operator *(Matrix<T> matrix, double multiplier)
        {
            Matrix<T> mult = new Matrix<T>(matrix.Rows, matrix.Columns);
            for (int i = 0; i < mult.Rows; i++)
            {
                for (int j = 0; j < mult.Columns; j++)
                {
                    dynamic dMatrix = matrix[i, j];
                    mult.table[i, j] = dMatrix * multiplier;
                }
            }
            return mult;
        }

        public static Matrix<T> operator *(double multiplier, Matrix<T> matrix)
        {
            Matrix<T> mult = new Matrix<T>(matrix.Rows, matrix.Columns);
            for (int i = 0; i < mult.Rows; i++)
            {
                for (int j = 0; j < mult.Columns; j++)
                {
                    dynamic dMatrix = matrix[i, j];
                    mult.table[i, j] = multiplier * dMatrix;
                }
            }
            return mult;
        }

        public static Matrix<T> operator -(Matrix<T> matrix)
        {
            return -1 * matrix;
        }

        public override string ToString()
        {
            StringBuilder matrixToString = new StringBuilder();
            matrixToString.Clear();


            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    dynamic item = table[i, j];
                    if ((int)(item * 100) % 100 == 0)
                        matrixToString.Append($"{table[i, j],4:F0} ");
                    else
                        matrixToString.Append($"{table[i, j],5:F2} ");
                }
                matrixToString.Append("\n");
            }

            return matrixToString.ToString();
        }


    }
}
