namespace math_fun_of_sys_anal;

public class JacobiMethod
{
    private const double Eps = 0.001;
    private readonly double[][] _array;
    private readonly double[] _free;

    public JacobiMethod(double[][] arr)
    {
        _array = new double[arr.Length][];
        for (int i = 0; i < arr.Length; i++)
        {
            _array[i] = new double[arr[i].Length - 1];
            Array.Copy(arr[i], _array[i], arr[i].Length - 1);
        }

        _free = arr.Select(x => x[arr.Length]).ToArray();
    }

    public string Solve(double[] x)
    {
        PrepareArray();

        var l = _array.Length;
        var b1 = _array.Select(x => x.Sum(Math.Abs) - x[l - 1]).Max();
        // var b2 = _array.Select((x, i) => Math.Abs(x[i])).Max();
        var b2 = Enumerable.Range(0, _array[0].Length - 1)
            .Select(i => _array.Sum(a => Math.Abs(a[i])))
            .Max();
        var tempX = new double[l];

        double norm;
        do
        {
            for (var i = 0; i < l; i++)
            {
                tempX[i] = _free[i];
                for (var g = 0; g < l - 1; g++)
                    if (i != g)
                        tempX[i] += _array[i][g] * x[g];
            }

            norm = Math.Abs(x[0] - tempX[0]);
            for (var h = 0; h < l; h++)
            {
                if (Math.Abs(x[h] - tempX[h]) > norm)
                    norm = Math.Abs(x[h] - tempX[h]);
                x[h] = tempX[h];
            }
        } while (norm > Eps);

        var res = string.Join('\n', x);
        return res;
    }

    private void PrepareArray()
    {
        var l = _array.Length;
        for (var i = 0; i < l; i++)
        {
            var x = _array[i][i];
            for (var j = 0; j < l; j++) _array[i][j] /= -x;

            _array[i][l - 1] /= x;
            _array[i][i] = 0;
        }
    }
}