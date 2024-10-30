using GraphAlgorithms;
using GraphAlgorithms.DataStructures;
using GraphAlgorithms.Logic;

namespace GraphAlgorithms;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Generating Graph...!");
        var graph = GraphGenerator.GenerateRandomGraph(1234, 1000, 1, 20, 1, 10);
        Console.WriteLine("Graph Generated!");
        
        Console.WriteLine("Exiting...");
    }
}