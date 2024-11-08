using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using GraphAlgorithms;
using GraphAlgorithms.DataStructures;
using GraphAlgorithms.Logic;
using GraphAlgorithms.SearchAlgorithms;

namespace GraphAlgorithms;

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<MyBenchmark>();

        // new MyBenchmark().RunBenchmark();


        Console.WriteLine("Exiting...");
    }
}

[MemoryDiagnoser]
// [RPlotExporter]
public class MyBenchmark
{
    [Params(100, 250, 750, 1000, 2500, 5000, 7500, 10000)]
    // [Params(1000, 2500, 5000, 7500, 10000, 25000, 50000, 75000, 100000)]
    // [BenchmarkDotNet.Attributes.]
    public int vertexCount;
    
    private const int Seed = 1245;
    Random rnd = new Random(Seed);
    string x;
    string y;

    private Graph graph;

    public MyBenchmark()
    {

    }
    
    [GlobalSetup]
    public void Setup()
    {
        Console.WriteLine("Generating Graph...!");

        graph = GraphGenerator.GenerateRandomGraph(1234, vertexCount, vertexCount/4, 1, 10);
        Console.WriteLine("Graph Generated!");
    }

    [Benchmark]
    public void BreathFirstSearch()
    {
        x = rnd.Next(0, graph.nodeAndCost.Count).ToString();
        y = rnd.Next(0, graph.nodeAndCost.Count).ToString();
        _ = new BreathFirstSearch().RunSearch(x, y, graph);
    }

    [Benchmark]
    public void DepthFirstSearch()
    {
        x = rnd.Next(0, graph.nodeAndCost.Count).ToString();
        y = rnd.Next(0, graph.nodeAndCost.Count).ToString();
        _ = new DepthFirstSearch().RunSearch(x, y, graph);
    }


    [Benchmark]
    public void IterativeDeepeningDFS()
    {
        x = rnd.Next(0, graph.nodeAndCost.Count).ToString();
        y = rnd.Next(0, graph.nodeAndCost.Count).ToString();
        _ = new IterativeDeepeningDFS().RunSearch(x, y, graph);
    }

}