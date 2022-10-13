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

    public double Min
    {
        set => _min = value;
    }

    public double Max
    {
        set => _max = value;
    }
    
    public double XMin
    {
        set => _xMin = value;
    }
    
    public double XMax
    {
        set => _xMax = value;
    }

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
        {
            if (i == rowNumber)
            {
                continue;
            }

            row[i] = Math.Round(_random.NextDouble() * (_max - _min) + _min, 2);
        }

        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            if (i == rowNumber)
            {
                continue;
            }

            sum += Math.Abs(row[i]);
        }

        var sign = _random.NextDouble() > 0.5 ? 1 : -1;

        row[rowNumber] = Math.Round(sum + Math.Abs(_random.NextDouble() * (_max - _min) + _min), 2) * sign;
        
        var f = 0.0;
        for (var i = 0; i < n; i++)
            f += Answers[i] * row[i];

        row[n] = Math.Round(f, 4, MidpointRounding.ToEven);
        return row;
    }

    private bool CompareRows(double[] row1, double[] row2)
    {
        var c = row1[0] / row2[0];
        return row1.Where((t, i) => Math.Abs(t - row2[i] * c) < 0.0001).Any();
    }

    private bool CheckRow(int index, double[] row)
    {
        double sum = 0;
        for (int i = 0; i < row.Length - 1; i++)
        {
            if (i == index)
            {
                continue;
            }

            sum += Math.Abs(row[i]);
        }

        return Math.Abs(row[index]) >= sum;
    }
}