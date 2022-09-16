float[][] arr = File.ReadAllLines("../../../input.txt") // если вдруг это не будет работать -- поменять путь на "input.txt" 
    .Select(l => l.Split(',')                   // если же и перемена пути не поможет -- молиться.
        .Select(i => float.Parse(i)).ToArray()).ToArray();

var g = new GaussMethod(arr);

File.WriteAllText("output.txt", g.Solve());


// реализация метода Гаусса с выбором главного элемента
// для первой лабораторной
public class GaussMethod
{
    private float[][] _array;

    public GaussMethod(float[][] arr)
    {
        _array = arr;
    }

    // основная функция, соединяющая в себе все функции, описанные ниже
    public string Solve()
    {
        for (var i = 0; i < _array[0].Length - 1; i++)
        {
            int indexMax = FindIndexMaxAbsNumber(i);
            var maxEl = _array[indexMax][i];

            // нормировка строки
            for (int j = i; j < _array[indexMax].Length; j++)
            {
                _array[indexMax][j] /= maxEl;
            }

            // изменение значений других строк
            for (int k = 0; k < _array.Length; k++)
            {
                if (k == indexMax) continue;
                var el = _array[k][i];
                for (var j = 0; j < _array[k].Length; j++)
                {
                    _array[k][j] -= _array[indexMax][j] * el;
                }
            }
        }

        Round();
        var res = BetterLookingResults();
        return res;
    }

    // функция нахождения индексов максимального и минимального элементов столбца
    private int FindIndexMaxAbsNumber(int col)
    {
        int indexMax = 0;
        float max = _array[indexMax][col];
        for (int i = 0; i < _array.Length; i++)
        {
            var el = Math.Abs(_array[i][col]);
            if (el > max)
            {
                max = el;
                indexMax = i;
            }
        }

        return indexMax;
    }

    // округление до целых чисел, когда это возможно
    private void Round()
    {
        var l = _array[0].Length - 1;
        for (var i = 0; i < _array.Length; i++)
        {
            var error = Math.Abs(_array[i][l] - Math.Round(_array[i][l], 0, MidpointRounding.ToEven));
            var relativeError = error / _array[i][l];
            if (relativeError < 1e-5 || error < 1e-5)
            {
                _array[i][l] = (int)(_array[i][l]);
            }
        }
    }

    // функция просто для того, чтобы результаты
    // выполнения алгоритма выглядели красивее
    private string BetterLookingResults()
    {
        var l = _array.Length;
        for (var i = 0; i < l; i++)
        {
            for (int j = 0; j < l; j++)
            {
                if ((int)_array[i][j] == 1 && j < i)
                {
                    Swap(i, j);
                }
            }
        }

        var res = string.Concat(Enumerable.Range(0, l)
            .Select(i => string.Format("x{0} = {1}; ", i + 1, _array[i].Last())));
        return res;
    }

    // функция, меняющая две строки местами
    private void Swap(int i1, int i2)
    {
        var temp = new float[_array[i1].Length];
        _array[i1].CopyTo(temp, 0);
        _array[i2].CopyTo(_array[i1], 0);
        temp.CopyTo(_array[i2], 0);
    }
}