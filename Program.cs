using System.Diagnostics;
using math_fun_of_sys_anal;

var gen = new SLAEGenerator(3);

File.WriteAllText("C:/Users/Dmitry/Documents/math_fun_of_sys_anal/generated_answers.txt",
    string.Join('\n', gen.Answers));
File.WriteAllText("C:/Users/Dmitry/Documents/math_fun_of_sys_anal/generated_input.txt", gen.Generate());

var arr = File
    .ReadAllLines("../../../generated_input.txt") // если вдруг это не будет работать -- поменять путь на "input.txt" 
    .Select(l => l.Split(' ') // если же и перемена пути не поможет -- молиться.
        .Select(double.Parse).ToArray()).ToArray();

var arr2 = File
    .ReadAllLines("../../../generated_input.txt") // если вдруг это не будет работать -- поменять путь на "input.txt" 
    .Select(l => l.Split(' ') // если же и перемена пути не поможет -- молиться.
        .Select(double.Parse).ToArray()).ToArray();

var stopWatch = new Stopwatch();
stopWatch.Start();
var g = new GaussMethod(arr);
var resultGauss = g.Solve();
stopWatch.Stop();
var durationGauss = stopWatch.ElapsedMilliseconds;

File.WriteAllText("C:/Users/Dmitry/Documents/math_fun_of_sys_anal/output_gauss.txt",
    $"{resultGauss}\nДлительность: {durationGauss} мс");

var stopWatch2 = new Stopwatch();
var x = new double[arr2.Length];
for (var i = 0; i < arr2.Length; i++)
for (var k = 0; k < arr2[i].Length; k++)
    x[i] = 0;
stopWatch2.Start();
var j = new JacobiMethod(arr2);
var resultJacobi = j.Solve(x);
stopWatch2.Stop();
var durationJacobi = stopWatch2.ElapsedMilliseconds;

File.WriteAllText("C:/Users/Dmitry/Documents/math_fun_of_sys_anal/output_jacobi.txt",
    $"{resultJacobi}\nДлительность: {durationJacobi} мс");