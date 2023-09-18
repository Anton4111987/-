using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;

namespace NS
{
    class Matrices
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int[,] Matrix { get; set; }
        string path = @"D:\MatrixFile.txt";
        public Matrices()
        {                
            Random value = new Random();
            Matrix =new int[Rows = value.Next(2, 7), Columns = value.Next(2, 7)];
            for (int i = 0; i < Rows; i++) 
            {                
                for (int j = 0; j < Columns; j++)
                {
                    Matrix[i, j] = value.Next(0,51);
                }                
            }
        }
        public Matrices(int Rows, int Columns)
        {
            this.Rows = Rows;
            this.Columns = Columns;
            Random value = new Random();
            Matrix = new int[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Matrix[i, j] = value.Next(0, 51);
                }
            }
        }
        public Matrices(int[,] matrix)
        {
            Matrix = matrix;
            Rows=Matrix.GetLength(0);
            Columns=Matrix.Length/Rows;        
        }
        public void Print()
        {         
            for (int i = 0; i < Rows; i++) 
            {
                Console.Write("| ");
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write("{0,4}", this.Matrix[i,j] + " ");//{0,2} устанавливает ширину
                }
                Console.WriteLine("|");
            }
        }
        //---------------------------------------------------------------------------------------------------
        // вычисление определителя матриц       
        public int Determinant2x2(int[,] array ) // функция вычисления определителя 2 на 2 
        {           
            int Det1 = 1;
            int Det2 = 1;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        Det1 *= array[i, j];
                    }
                    if (i + j == 1)
                    {
                        Det2 *= array[i, j];
                    }
                }
            }                  
            int Det = Det1 - Det2;           
            return Det;
        }
        
        public int DeterminantCalculation() // функция вычисления определителя
        {                       
            int R = Rows - 1;
            int C = Columns - 1;
            int[,] arr = new int[R, C]; // массив для новой подматрицы
            int Determ = 0; // детерминант 
            if (Rows > 2 && Columns > 2) // если матрица больше чем 2 на 2
            {                
                int element_a = 0; // переменная элемент на которую будет умножаться подматрица
                for (int k = 0; k < Rows; k++) // цикл для каждой подматрицы
                {
                    for (int i = 0; i < Rows; i++)
                    {
                        int x = 0; // переменная для записи в новый массив подматрицы
                        for (int j = 0; j < Columns; j++)
                        {

                            if (i != 0 && j != k)
                            {
                                arr[i - 1, x] = Matrix[i, j];
                                x++;
                            }
                            if (k % 2 == 0)
                                element_a = Matrix[0, k];
                            if (k % 2 != 0)
                                element_a = -Matrix[0, k];                           
                        }
                    }
                   
                    if (R > 2 && C>2) // если количество строк и столбцов более двух
                        Determ += DeterminantCalculation(arr) * element_a;
                    else
                    {                       
                        Determ += Determinant2x2(arr) * element_a;
                    }
                }
            }
            if (Rows == 2 && Columns == 2)
            {
                Determ = Determinant2x2(Matrix);
            }
            if(Rows!=Columns)
                throw new SizeEcseption();
         
            return Determ;
        }

        public int DeterminantCalculation(int[,] arr) // рекурсивная функция определения детерминанта для подмассивов
        {          
            int Rows=arr.GetLength(0); // вычисление размера массива
            int Columns = arr.GetLength(1);  // вычисление размера массива
            int R = Rows - 1;
            int C = Columns - 1;
            int[,] arr1 = new int[R, C]; // массив для новой подматрицы
            int Determ = 0;      // детерминант       
            int element_a = 0; // переменная элемент на которую будет умножаться подматрица
            for (int k = 0; k < Rows; k++) // цикл для каждой подматрицы
            {
                for (int i = 0; i < Rows; i++)
                {
                    int x = 0;
                    for (int j = 0; j < Columns; j++)
                    {
                        if (i != 0 && j != k)
                        {
                            arr1[i - 1, x] = arr[i, j];
                            x++;
                        }
                        if (k % 2 == 0)
                            element_a = arr[0, k];
                        if (k % 2 != 0)
                            element_a = -arr[0, k];                        
                    }
                }                             
                if (R > 2 && C>2)  // если количество строк и столбцов более двух
                    Determ += DeterminantCalculation(arr1)* element_a;
                else
                {
                    Determ += Determinant2x2(arr1) * element_a;
                }               
            }
            return Determ;
        }
            //---------------------------------------------------------------------------------------------------
            // запись матрицы в файл
        public void WriteMatrix()
        {
            while (true)
            {
                Console.WriteLine("Если хотите ввести путь вручную нажмите 1");
                Console.WriteLine("Если хотите оставить путь D:\\MatrixFile.txt нажмите 2");
                try
                {
                    int num = Convert.ToInt32(Console.ReadLine());
                    if(num == 1)
                        path=Console.ReadLine();
                    if (num == 2)
                    {
                        StringBuilder sb = new StringBuilder();// дле перевода массива в строку и добавления соответствующих пробелов и переносов          
                        for (int i = 0; i < Rows; i++)
                        {
                            for (int j = 0; j < Columns; j++)
                            {
                                sb.Append(this.Matrix[i, j] + "\t");//{0,2} устанавливает ширину
                            }
                            sb.Append("\n");
                        }
                        string text = sb.ToString();

                        File.WriteAllText(path, text);
                        Console.WriteLine("Матрица успешно записана");
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        //---------------------------------------------------------------------------------------------------
        // чтение матрицы из файла
        public void ReadMatrix()
        {
            bool fileExist = File.Exists(path);
            if (fileExist)
            {
                Console.WriteLine("Считывание матрицы из файла");
                Console.Write(File.ReadAllText(path));
            }
            else
            {
                throw new ReadMatrixEcseption();                
            }
        }

        //---------------------------------------------------------------------------------------------------
        // сложение матриц
        public Matrices Addition(Matrices M)
        {          
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Matrix[i, j] += M.Matrix[i, j];
                }
            }
            return new Matrices(Matrix);//, Rows, Columns);
        }
        public static Matrices operator + (Matrices M1,Matrices M2)
        {
            if (M1.Rows != M2.Rows || M1.Columns != M2.Columns)
                throw new MatrixDifferentSize("Действие запрещено! Матрицы разных размеров");
            else
            return M1.Addition(M2);
        }
        //---------------------------------------------------------------------------------------------------
        // вычитание матриц
        public Matrices Subtraction(Matrices M)
        {

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Matrix[i, j] -= M.Matrix[i, j];
                }
            }
            return new Matrices(Matrix);//, Rows, Columns);
        }
        public static Matrices operator-(Matrices M1, Matrices M2)
        {
            if (M1.Rows != M2.Rows || M1.Columns != M2.Columns)
                throw new MatrixDifferentSize("Действие запрещено! Матрицы разных размеров");
            else 
            return M1.Subtraction(M2);
        }
        //---------------------------------------------------------------------------------------------------
        // умножение матриц
        public Matrices Multiplication(Matrices M)
        {
            if (Columns != M.Rows)
                throw new MatrixEcseption();  // исключение если 
            int[,] matrixres = new int[Rows,M.Columns]; 
            for (int i = 0; i < Rows; i++) 
            {                
                for (int j = 0; j < M.Columns; j++)
                {
                    for (int k = 0; k < M.Rows; k++)
                       matrixres[i,j] += Matrix[i,k] * M.Matrix[k,j]; 
                }               
            }
            return new Matrices(matrixres);
        }
        public static Matrices operator*(Matrices M1, Matrices M2)
        {
            return M1.Multiplication(M2);
        }
        //---------------------------------------------------------------------------------------------------
        // умножение матрицы на число
        public Matrices MultiplicationOnNum(int n)
        {
           
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Matrix[i, j] *= n;
                }
            }
            return new Matrices(Matrix);//, Rows, Columns);
        }
        
    }
}
