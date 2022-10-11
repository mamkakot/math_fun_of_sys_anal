namespace math_fun_of_sys_anal;

public class SLAEGenerator
{
    private readonly double[][] _arr;
    private readonly Random _random;
    private double _min = 1.5, _max = 17.3;

    public SLAEGenerator(int n)
    {
        _arr = new double[n][];
        _random = new Random();
        Answers = new double[n];
        for (var i = 0; i < n; i++) Answers[i] = Math.Round(_random.NextDouble() * (_max - _min) + _min, 2);
    }

    public double Min
    {
        set => _min = value;
    }

    public double Max
    {
        set => _max = value;
    }

    public double[] Answers { get; }

    public string Generate()
    {
        for (var i = 0; i < _arr.Length; i++)
        {
            _arr[i] = GenerateRow(_arr.Length);
            while (i > 0 && CompareRows(_arr[i], _arr[i - 1])) _arr[i] = GenerateRow(_arr.Length);
        }

        var res = string.Join('\n', _arr.Select(a => string.Join(' ', a)));
        return res;
    }

    private double[] GenerateRow(int n)
    {
        var row = new double[n + 1];
        for (var i = 0; i < n; i++) row[i] = Math.Round(_random.NextDouble() * (_max - _min) + _min, 2);
        var f = 0.0;
        for (var i = 0; i < n; i++) f += Answers[i] * row[i];

        row[n] = Math.Round(f, 4, MidpointRounding.ToEven);
        return row;
    }

    private bool CompareRows(double[] row1, double[] row2)
    {
        var c = row1[0] / row2[0];
        return !row1.Where((t, i) => Math.Abs(t - row2[i] * c) < 0.0001).Any();
    }
}