namespace math_fun_of_sys_anal;

public class SLAEGenerator
{
    private readonly double[][] _arr;
    private readonly Random _random;
    private double _min = 1.5, _max = 17.3;
    private double _xMin = -223.4, _xMax = 993.12;


    public SLAEGenerator(int n)
    {
        _arr = new double[n][];
        _random = new Random();
        Answers = new double[n];
        for (var i = 0; i < n; i++) Answers[i] = Math.Round(_random.NextDouble() * (_xMax - _xMin) + _xMin, 2);
    }

    // всякие сеттеры для регуляции разброса, отдельно для коэффициентов при независимых переменных
    public double Min
    {
        set => _min = value;
    }

    public double Max
    {
        set => _max = value;
    }

    // и для самих независимых переменных
    public double XMin
    {
        set => _xMin = value;
    }

    public double XMax
    {
        set => _xMax = value;
    }

    // массив независимых переменных
    public double[] Answers { get; }

    public string Generate()
    {
        for (var i = 0; i < _arr.Length; i++)
        {
            _arr[i] = GenerateRow(_arr.Length, i);
            while (i > 0 && !CompareRows(_arr[i], _arr[i - 1]))
                _arr[i] = GenerateRow(_arr.Length, i);
        }

        var res = string.Join('\n', _arr.Select(a => string.Join(' ', a)));
        return res;
    }

    private double[] GenerateRow(int n, int rowNumber = 0)
    {
        var row = new double[n + 1];
        for (var i = 0; i < n; i++)
            // вот эта дрянь ниже нужна для того, чтобы генерировались такие системы, что метод Якоби
            // вообще может их решить, причём гарантированно (ниже написано, почему)
            if (i != rowNumber)
                row[i] = Math.Round(_random.NextDouble() * (_max - _min) + _min, 2);

        double sum = 0;
        for (var i = 0; i < n; i++)
        {
            if (i == rowNumber) continue;

            sum += Math.Abs(row[i]);
        }

        var sign = _random.NextDouble() > 0.5 ? 1 : -1;

        // вот тут продолжение той же дрянной идеи, что описана выше:
        // элемент на главной диагонали создаётся таким, чтобы модуль его был больше либо равен сумме модулей
        // других членов строки, то есть возникало преобладание диагональных элементов
        row[rowNumber] = Math.Round(sum + Math.Abs(_random.NextDouble() * (_max - _min) + _min), 2) * sign;

        var f = 0.0;
        for (var i = 0; i < n; i++)
            f += Answers[i] * row[i];

        row[n] = Math.Round(f, 4, MidpointRounding.ToEven);
        return row;
    }

    // это чтобы ранг матрицы всегда был равен размерности матрицы
    private bool CompareRows(double[] row1, double[] row2)
    {
        var c = row1[0] / row2[0];
        return row1.Where((t, i) => Math.Abs(t - row2[i] * c) < 0.0001).Any();
    }
}