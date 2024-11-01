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
    
    private readonly Graph graph;
    public MyBenchmark()
    {
        Console.WriteLine("Generating Graph...!");

        graph = GraphGenerator.GenerateRandomGraph(1234, 1000, 250, 1, 10);
        Console.WriteLine("Graph Generated!");
    }

    [Benchmark]
    public void DepthFirstSearch()
    {
        Random rnd = new Random(Seed);
        var x = rnd.Next(0, graph.nodeAndCost.Count);
        var y = rnd.Next(0, graph.nodeAndCost.Count);
        
        _ = new BreathFirstSearch().RunSearch(x.ToString(), y.ToString(), graph);
    }

    
}