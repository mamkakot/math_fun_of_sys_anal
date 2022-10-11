namespace math_fun_of_sys_anal;

// реализация метода Гаусса с выбором главного элемента
// для первой лабораторной
public class GaussMethod
{
    private readonly double[][] _array;
    private readonly int[] _indexes;

    public GaussMethod(double[][] arr)
    {
        _array = arr;
        _indexes = new int[_array.Length];
    }

    // основная функция, соединяющая в себе все функции, описанные ниже
    public string Solve()
    {
        for (var i = 0; i < _array[0].Length - 1; i++)
        {
            var indexMax = FindIndexMaxAbsNumber(i);
            var maxEl = _array[indexMax][i];


            // нормировка строки
            for (var j = i; j < _array[indexMax].Length; j++) _array[indexMax][j] /= maxEl;

            // изменение значений других строк
            for (var k = 0; k < _array.Length; k++)
            {
                if (k == indexMax) continue;
                var el = _array[k][i];
                for (var j = 0; j < _array[k].Length; j++) _array[k][j] -= _array[indexMax][j] * el;
            }

            _indexes[i] = indexMax;
        }

        var res = GenericFunctions.BetterLookingResults(GenericFunctions.Round(_array));
        return res;
    }

    // функция нахождения индексов максимального и минимального элементов столбца
    private int FindIndexMaxAbsNumber(int col)
    {
        var indexMax = 0;
        var max = _array[indexMax][col];
        for (var i = 0; i < _array.Length; i++)
        {
            var el = Math.Abs(_array[i][col]);
            if (!(el > max) || _indexes.Contains(i)) continue;
            max = el;
            indexMax = i;
        }

        return indexMax;
    }
}