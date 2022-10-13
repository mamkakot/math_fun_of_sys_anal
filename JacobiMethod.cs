namespace math_fun_of_sys_anal;

public class JacobiMethod
{
    private const double Eps = 0.0001;
    private readonly double[][] _array;
    private const string Path = "C:/Users/Dmitry/Documents/x_array.txt";

    public JacobiMethod(double[][] arr, double[] x)
    {
        _array = arr;
        X = x;
    }
    
    public double[] X { get; set;  }

    public string Solve()
    {
        File.WriteAllText(Path, "");
        var l = _array.Length;
        var tempX = new double[l];

        double err = 1;
        while (err >= Eps)
        {
            for (int i = 0; i < l; i++)
            {
                tempX[i] = GetX(i, X);
            }
            err = CalculateError(X, tempX);
            GenericFunctions.WriteXsToFile(Path, X);
            tempX.CopyTo(X, 0);
        }

        var res = GenericFunctions.BetterLookingResults(GenericFunctions.Round(X));
        return res;
    }

    private double CalculateError(double[] x1, double[] x2)
    {
        var len = x1.Length;
        var errors = new double[len];
        for (int i = 0; i < len; i++)
        {
            errors[i] = Math.Abs(x1[i] - x2[i]);
        }
        return errors.Max();
    }

    private double GetX(int xNum, double[] x)
    {
        double temp = _array[xNum].Last();
        for (int i = 0; i < _array.Length; i++)
        {
            if (i == xNum)
            {
                continue;
            }

            temp -= _array[xNum][i] * x[i];
        }

        temp /= _array[xNum][xNum];

        return temp;
    }
}