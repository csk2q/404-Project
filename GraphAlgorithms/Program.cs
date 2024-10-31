using GraphAlgorithms;
using GraphAlgorithms.DataStructures;
using GraphAlgorithms.Logic;
using GraphAlgorithms.SearchAlgorithms;

namespace GraphAlgorithms;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Generating Graph...!");
        // Note best first only seems to work for this set of values some how.
        var graph = GraphGenerator.OLDGenerateRandomGraph(1234, 1000, 1, 20, 1, 10);
        // var graph = GraphGenerator.OLDGenerateRandomGraph(1234, 500, 1, 3, 1, 10);
        Console.WriteLine("Graph Generated!");

        var pathLengths = new int[1000];

        /*Parallel.For(0, pathLengths.Length, i =>
        {
            Parallel.For(0, pathLengths.Length, j =>
            {
                // Console.WriteLine("Starting best first search...");
                // var resultPath = new BestFirstSearch().RunSearch(i.ToString(), j.ToString(), graph);
                // Console.WriteLine($"Best First Search Result: {string.Join("->", resultPath)}");
                pathLengths[i] = new BestFirstSearch().RunSearch(i.ToString(), j.ToString(), graph).Length;
            });
            Console.WriteLine("Finished i=" + i);
        });*/

        for (var i = 0; i < pathLengths.Length; i++)
        {
            for (var j = 0; j < pathLengths.Length; j++)
            {
                pathLengths[i] = new BreathFirstSearch().RunSearch(i.ToString(), j.ToString(), graph).Length;
            }

            Console.WriteLine("Finished i=" + i + $" {pathLengths[i]}");
        }

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


        Console.WriteLine("Exiting...");
    }
}