internal class Program
{
    private static void Main(string[] args)
    {
        float[][] arr = File.ReadAllLines("input.txt")
                   .Select(l => l.Split(' ').Select(i => float.Parse(i)).ToArray())
                   .ToArray();
        var g = new GaussMethod(arr);
        Console.WriteLine($"{g.Solve()}");
    }
}

// реализация метода Гаусса с выбором главного элемента
// для первой лабораторной
public class GaussMethod
{
    private float[][] array;
    public GaussMethod(float[][] arr)
    {
        array = arr;
    }

    public float[][] Solve()
    {
        for (var i = 0; i < array[0].Length - 1; i++)
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
                var el = array[k][i];
                for (var j = 0; j < array[k].Length; j++)
                {
                    array[k][j] -= array[indexMax][j] * el;
                }
            }
        }

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
}
