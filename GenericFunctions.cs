namespace math_fun_of_sys_anal;

public static class GenericFunctions
{
    // округление до целых чисел, когда это возможно
    public static double[][] Round(double[][] arr)
    {
        var l = arr[0].Length - 1;
        foreach (var row in arr)
        {
            row[l] = Math.Round(row[l], 2, MidpointRounding.ToEven);
            var error = Math.Abs(row[l] - Math.Round(row[l], 0, MidpointRounding.ToEven));
            var relativeError = error / row[l];
            if (relativeError < 1e-5 || error < 1e-5) row[l] = (int)Math.Round(row[l], 0, MidpointRounding.ToEven);
        }

        return arr;
    }

    // тоже округление, но чисто для итерационных методов
    public static double[] Round(double[] arr)
    {
        var l = arr.Length;
        for (var i = 0; i < arr.Length; i++)
        {
            arr[i] = Math.Round(arr[i], 2, MidpointRounding.ToEven);
            var error = Math.Abs(arr[i] - Math.Round(arr[i], 0, MidpointRounding.ToEven));
            var relativeError = error / arr[i];
            if (relativeError < 1e-5 || error < 1e-5) arr[i] = (int)Math.Round(arr[i], 0, MidpointRounding.ToEven);
        }

        return arr;
    }

    // функция просто для того, чтобы результаты
    // выполнения алгоритма выглядели красивее
    public static string BetterLookingResults(double[][] arr)
    {
        var l = arr.Length;
        for (var i = 0; i < l; i++)
        for (var j = 0; j < arr[0].Length; j++)
        {
            if ((int)arr[i][j] != 1 || j >= i) continue;
            var temp = new double[arr[i].Length];
            arr[i].CopyTo(temp, 0);
            arr[j].CopyTo(arr[i], 0);
            temp.CopyTo(arr[j], 0);
        }

        var res = string.Concat(Enumerable.Range(0, l)
            .Select(i => $"x{i + 1} = {arr[i].Last()}; \n"));
        return res;
    }

    // то же, что выше, только -- опять же -- для итерационных методов
    public static string BetterLookingResults(double[] arr)
    {
        var l = arr.Length;
        var res = string.Concat(Enumerable.Range(0, l)
            .Select(i => $"x{i + 1} = {arr[i]}; \n"));
        return res;
    }

    // запись иксов в файл -- опять же для итерационных методов
    public static void WriteXsToFile(string path, double[] x)
    {
        File.AppendAllText(path, $"{string.Join(' ', x)}\n");
    }
}