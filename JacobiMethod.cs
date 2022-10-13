namespace math_fun_of_sys_anal;

public class JacobiMethod
{
    // точность можно регулировать, но я её такой выставил такой, чтобы больше итераций было
    private const double Eps = 0.0001;
    private const string Path = "x_array.txt";
    private readonly double[][] _array;

    public JacobiMethod(double[][] arr, double[] x)
    {
        _array = arr;
        X = x;
    }

    public double[] X { get; }

    // костыль для того, чтобы управлять процессом -- писать решение на каждой итерации в файл или нет,
    // потому что запись в файл задерживает выполнение метода, а параллелить код я вообще не хочу.
    // Я просто графики строил по этому поводу, смотрел, как меняются иксы, как скачут их значения
    // скачут сильно, у меня где-то по соседству должен лежать jupyter notebook, в котором я это смотрел
    // а смотрел потому, что до этого просматривал видос по другим итерационным методам, и например 
    // в методе релаксации скачут иксы менее активно, там кривые более красивые выходят
    // но сюда реализацию этого метода не включал. Если захотите -- впишу сюда.
    public bool WriteXsIntoFile { get; init; }

    public string Solve()
    {
        if (WriteXsIntoFile) File.WriteAllText(Path, "");
        var l = _array.Length;
        var tempX = new double[l];

        double err = 1;
        while (err >= Eps)
        {
            for (var i = 0; i < l; i++) tempX[i] = GetX(i, X);

            err = CalculateError(X, tempX);
            if (WriteXsIntoFile) GenericFunctions.WriteXsToFile(Path, X);

            tempX.CopyTo(X, 0);
        }

        var res = GenericFunctions.BetterLookingResults(GenericFunctions.Round(X));
        return res;
    }

    // функция высчитывания "ошибки", вынес в отдельную функцию, чтобы основную функцию не захламлять
    private double CalculateError(double[] x1, double[] x2)
    {
        var len = x1.Length;
        var errors = new double[len];
        for (var i = 0; i < len; i++) errors[i] = Math.Abs(x1[i] - x2[i]);

        return errors.Max();
    }

    // функция, которая выражает определённый икс через другие иксы
    private double GetX(int xNum, double[] x)
    {
        var temp = _array[xNum].Last();
        for (var i = 0; i < _array.Length; i++)
        {
            if (i == xNum) continue;

            temp -= _array[xNum][i] * x[i];
        }

        temp /= _array[xNum][xNum];

        return temp;
    }
}