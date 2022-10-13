using System.Diagnostics;
using math_fun_of_sys_anal;

var gen = new SLAEGenerator(20);

File.WriteAllText("generated_answers.txt",
    string.Join('\n', gen.Answers));
File.WriteAllText("generated_input.txt", gen.Generate());

var arr = File
    .ReadAllLines("generated_input.txt") // если вдруг это не будет работать -- поменять путь на "input.txt" 
    .Select(l => l.Split(' ') // если же и перемена пути не поможет -- молиться.
        .Select(double.Parse).ToArray()).ToArray();

var arr2 = new double[arr.Length][];
for (var i = 0; i < arr.Length; i++)
{
    arr2[i] = new double[arr[i].Length];
    arr[i].CopyTo(arr2[i], 0);
}

var stopWatch = new Stopwatch();
stopWatch.Start();
var g = new GaussMethod(arr);
var resultGauss = g.Solve();
stopWatch.Stop();
var durationGauss = stopWatch.ElapsedMilliseconds;

File.WriteAllText("output_gauss.txt",
    $"{resultGauss}\nДлительность: {durationGauss} мс");

var stopWatch2 = new Stopwatch();
var x = new double[arr2.Length];
var random = new Random();
for (var i = 0; i < arr2.Length; i++)
    for (var k = 0; k < arr2[i].Length; k++)
        x[i] = 0;
stopWatch2.Start();
var j = new JacobiMethod(arr2, x);
j.X = x;
var resultJacobi = j.Solve();
stopWatch2.Stop();
var durationJacobi = stopWatch2.ElapsedMilliseconds;

File.WriteAllText("output_jacobi.txt",
    $"{resultJacobi}\nДлительность: {durationJacobi} мс");