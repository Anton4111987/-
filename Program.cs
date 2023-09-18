using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS
{
    internal class Program
    {
        public static Matrices matrices;
        public static Matrices matrices2;
        public static Matrices matricesRes;
        public static int CountRows;
        public static int CountColumns;
        public static int NumberCreateMatrix;

        public static Matrices CreateMatrix()
        {
            Matrices Matr;
            // цикл заполнения матрицы
            while (true)
            {
                try
                {
                    Console.Write($"Введите количество строк матрицы: ");
                    CountRows = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Введите количество столбцов матрицы: ");
                    CountColumns = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Если желаете чтобы матрица заполнилась рандомно введите 1");
                    Console.WriteLine($"Если желаете заполнить самостоятельно введите 2");
                    NumberCreateMatrix = Convert.ToInt32(Console.ReadLine());
                    if (NumberCreateMatrix == 1)
                    {
                        Matr = new Matrices(CountRows, CountColumns);
                        break;
                    }
                    if (NumberCreateMatrix == 2)
                    {
                        Console.WriteLine($"Заполните матрицу числами: ");
                        int[,] ArrMatrix = new int[CountRows, CountColumns];
                        for (int i = 0; i < CountRows; i++)
                        {
                            for (int j = 0; j < CountColumns; j++)
                            {
                                Console.Write($"Введите число {i} строки {j} столбца: ");
                                ArrMatrix[i, j] = Convert.ToInt32(Console.ReadLine());
                            }
                        }
                        Matr = new Matrices(ArrMatrix);
                        break;
                    }
                    else
                        Console.WriteLine($"Введено неверное значение, попробуйте еще раз");
                }
                catch (FormatException fe)
                {
                    Console.WriteLine($"{fe.Message}");
                }                
            }
            Console.WriteLine($"Полученная матрица");
            Matr.Print();
            return Matr;
        }

        static void Main(string[] args)
        {            
            int NumberMenu; // переменная для меню
            int number; // переменная для подменю
            while (true)
            {               
                Console.WriteLine($"Программа работы с матрицами!");
                // ---------------------------------------------------------------------------------               
                matrices=CreateMatrix();// функция создания матриц
                // ---------------------------------------------------------------------------------
                Console.WriteLine($"Выберете необходимое действие:");
                Console.WriteLine($"1 - Для вычисления определителя");
                Console.WriteLine($"2 - Для записи матрицы в файл");
                Console.WriteLine($"3 - Для чтения матрицы из файла");
                Console.WriteLine($"4 - Для умножения матрицы на число");
                Console.WriteLine($"5 - Для добавления второй матрицы и работой с двумя матрицыми");
                Console.WriteLine($"0 - Для завершения программы");
                NumberMenu =Convert.ToInt32(Console.ReadLine());
                switch(NumberMenu) 
                {
                    case 0:
                        Console.WriteLine("Вы ввели 0, программа будет закрыта!");
                        break;
                    case 1:
                        Console.WriteLine($"Детерминатор = {matrices.DeterminantCalculation()}");
                        break;
                    case 2:
                        Console.WriteLine($"Запись в файл по адресу C:\\MatrixFile.txt");
                        matrices.WriteMatrix();
                        break;
                    case 3:
                        matrices.ReadMatrix();
                        break;
                    case 4:
                        Console.WriteLine($"Введите целое число на которое нужно умножить матрицу");
                        while (true)
                        {
                            try
                            {
                                number = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Полученная матрица:");
                                matrices.MultiplicationOnNum(number).Print();
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Вы ввели неверное число");
                                Console.WriteLine("Введите правильное число");
                            }                            
                        }
                        break;
                    case 5:
                        
                            // ---------------------------------------------------------------------------------               
                            matrices2 =CreateMatrix();
                        // ---------------------------------------------------------------------------------
                        while (true)
                        {
                            Console.WriteLine($"Выберете необходимое действие для матриц:");
                            Console.WriteLine($"1 - Сложить матрицы");
                            Console.WriteLine($"2 - Вычесть из первой матрицы вторую");
                            Console.WriteLine($"3 - Умножить первую матрицу на вторую");
                            Console.WriteLine($"0 -Выход из подменю");
                            int NumberSubMenu = Convert.ToInt32(Console.ReadLine());
                            switch (NumberSubMenu)
                            {
                                case 0:
                                    Console.WriteLine("Вы ввели 0, будете перенаправлены на основное меню!");
                                    break;
                                case 1:
                                    Console.WriteLine($"Сумма матриц:");
                                    try
                                    {
                                        matricesRes = matrices + matrices2;
                                        matricesRes.Print();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"{ex.Message}");
                                        Console. WriteLine("Введите заново вторую матрицу "); 
                                    }
                                    matrices2 = CreateMatrix();
                                    break;
                                   
                                case 2:
                                    Console.WriteLine($"Разность матриц:");
                                    try
                                    {
                                        matricesRes = matrices - matrices2;
                                        matricesRes.Print();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"{ex.Message}");
                                        Console.WriteLine("Введите заново вторую матрицу ");
                                    }
                                    matrices2 = CreateMatrix();
                                    break;                                    
                                case 3:
                                    Console.WriteLine($"Умножение матриц:");
                                    try
                                    {
                                        matricesRes = matrices * matrices2;
                                        matricesRes.Print();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"{ex.Message}");
                                        Console.WriteLine("Введите заново вторую матрицу ");
                                    }
                                    matrices2 = CreateMatrix();                                                                        
                                    break;
                                default:
                                    Console.WriteLine("Введено неверное значение!");
                                    break;
                            }
                            if (NumberSubMenu == 0)
                                break;
                        }
                        break;

                    default:
                        Console.WriteLine("Введено неверное значение!");
                        break;

                }

                if (NumberMenu == 0)
                    break;

            } // конец 
            Console.ReadLine();
        }
    }

}
