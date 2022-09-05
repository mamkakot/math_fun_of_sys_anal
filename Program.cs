internal class Program
{
    private static void Main(string[] args)
    {
        var arr = new float[][]
        {
            new float[] { 2, 3, -1 },
            new float[] { 1, -1, 6 },
            new float[] { 6, -2, 1 },
        };

        var absoluteTerms = new float[] { 7, 14, 11 };

        var g = new GaussMethod(arr, absoluteTerms);
        Console.WriteLine($"{g.Solve()}");
    }
}

// реализация метода Гаусса с выбором главного элемента
// для первой лабораторной
public class GaussMethod
{
    private float[][] array;
    private float[] absoluteTerms;
    public GaussMethod(float[][] arr, float[] absT)
    {
        array = arr;
        absoluteTerms = absT;
    }

    public float[][] Solve()
    {
        ChangeRowValues();
        Console.WriteLine($"{array[FindIndexMaxAbsNumber(0)][0]}");

        return array;
    }

    private int FindIndexMaxAbsNumber(int col)
    {
        int indexMax = 0;
        float max = array[indexMax][col];
        for (int i = 0; i < array.Length; i++)
        {
            var el = Math.Abs(array[i][col]);
            if (el > max)
            {
                max = el;
                indexMax = i;
            }
        }
        return indexMax;
    }

    private void ChangeRowValues()
    {
        for (var i = 0; i < array[0].Length; i++)
        {
            int indexMax = FindIndexMaxAbsNumber(i);
            var maxEl = array[indexMax][i];

            // нормировка строки
            for (int j = i; j < array[indexMax].Length; j++)
            {
                array[indexMax][j] /= maxEl;
            }

            // изменение значений других строк
            for (int k = 0; k < array.Length; k++)
            {
                if (k == indexMax) continue;
                for (var j = 0; j < array[i].Length; j++)
                {
                    array[k][j] -= array[indexMax][j] * array[k][i];
                }
            }
        }
    }
}


