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
public class MyBenchmark
{
    private const int Seed = 1245;
    Random rnd = new Random(Seed);
    string x;
    string y;

    private readonly Graph graph;

    public MyBenchmark()
    {
        Console.WriteLine("Generating Graph...!");

        graph = GraphGenerator.GenerateRandomGraph(1234, 1000, 250, 1, 10);
        Console.WriteLine("Graph Generated!");

        Random rnd = new Random(Seed);

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