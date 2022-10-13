using System.Diagnostics;
using math_fun_of_sys_anal;

var generator = new SLAEGenerator(80);

File.WriteAllText("generated_answers.txt",
    string.Join('\n', generator.Answers));
File.WriteAllText("generated_input.txt", generator.Generate());

var arrayGauss = File.ReadAllLines("generated_input.txt").Select(l => l.Split(' ').Select(double.Parse).ToArray()).ToArray();
var arrayJacobi = new double[arrayGauss.Length][];

for (var i = 0; i < arrayGauss.Length; i++)
{
    arrayJacobi[i] = new double[arrayGauss[i].Length];
    arrayGauss[i].CopyTo(arrayJacobi[i], 0);
}

var stopWatch = new Stopwatch();
stopWatch.Start();

var g = new GaussMethod(arrayGauss);
var resultGauss = g.Solve();

stopWatch.Stop();
var durationGauss = stopWatch.ElapsedMilliseconds;

File.WriteAllText("output_gauss.txt",
    $"{resultGauss}\nДлительность: {durationGauss} мс");


var x = new double[arrayJacobi.Length]; // он так и так нулями заполняется

stopWatch.Restart();

var j = new JacobiMethod(arrayJacobi, x)
{
    // дабы иксы в файл не писались
    WriteXsIntoFile = false
};

var resultJacobi = j.Solve();
stopWatch.Stop();
var durationJacobi = stopWatch.ElapsedMilliseconds;

File.WriteAllText("output_jacobi.txt",
    $"{resultJacobi}\nДлительность: {durationJacobi} мс");