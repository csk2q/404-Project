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
    private const bool DEBUG_PRINT = false;
    
    private readonly Graph graph;
    public MyBenchmark()
    {
        Console.WriteLine("Generating Graph...!");

        graph = GraphGenerator.GenerateRandomGraph(1234, 1000, 250, 1, 10);
        Console.WriteLine("Graph Generated!");
        
        
    }

    [Benchmark]
    public void DepthFirstSearch() => dfs(1);
    void dfs(int searchCount, int seed = 100)
    {
        Random rnd = new Random(seed);
        var pathLengths = new int[searchCount];

        for (var i = 0; i < searchCount; i++)
        {
            var x = rnd.Next(0, graph.nodeAndCost.Count);
            
            for (var j = 0; j < searchCount; j++)
            {
                var y = rnd.Next(0, graph.nodeAndCost.Count);

                
                pathLengths[i] = new BreathFirstSearch().RunSearch(x.ToString(), y.ToString(), graph).Length;
            }

            if(DEBUG_PRINT)
            Console.WriteLine("Finished i=" + i + $" {pathLengths[i]}");
        }

        if (DEBUG_PRINT)
        {
            Console.WriteLine($"Max path length: {pathLengths.Max()}");
            Dictionary<int, int> pathLengthBuckets = new Dictionary<int, int>();

            foreach (var pathLength in pathLengths)
            {
                if (pathLengthBuckets.ContainsKey(pathLength))
                    pathLengthBuckets[pathLength]++;
                else
                    pathLengthBuckets.Add(pathLength, 1);
            }

            foreach (var pathLength in pathLengthBuckets)
                Console.WriteLine($"{pathLength.Key}: {pathLength.Value}");

        }
    }
}