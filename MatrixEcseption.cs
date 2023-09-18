using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS
{
    class MatrixEcseption: ApplicationException // исключение по несоответствию матриц для действия умножения
    {
        public MatrixEcseption():base("Матрицы нельзя перемножать! Количество " +
            "столбцов первой матрицы не равно количесву строк второй матрицы"){}       
    }

    class ReadMatrixEcseption : ApplicationException // исключение при отсутствии файла по заданному пути
    {
        public ReadMatrixEcseption() : base("По данному пути матрица отсутствует") { }
    }

    class MatrixDifferentSize: ApplicationException // исключение ввиду разноразмерных матриц
    {
        public MatrixDifferentSize(string message) : base(message) { }         
    }

    class SizeEcseption : ApplicationException // исключение по несоответствию матриц для действия умножения
    {
        public SizeEcseption() : base("Определитель данной матрицы посчитать невозможно, так как " +
            "количество строк не равно количеству столбцов ")
        { }
        
    }
}


