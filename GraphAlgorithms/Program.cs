using GraphAlgorithms;
using GraphAlgorithms.DataStructures;
using GraphAlgorithms.Logic;

namespace GraphAlgorithms;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var graph = GraphGenerator.GenerateRandomGraph(1234, 1000, 3, 5, 1, 10);
        
        
        Console.WriteLine("Exiting...");
    }
}