using System.Collections.Frozen;
using System.Numerics;
using GraphAlgorithms;
using GraphAlgorithms.DataStructures;

namespace GraphAlgorithms.Logic;

public record Graph(FrozenDictionary<string, int> nodeAndCost, AdjacencyMatrix<string> Edges);

public class GraphGenerator
{
    private const bool DEBUG_Prints = false;
    
    private GraphGenerator()
    {
        
        
    }
    
    /*
     * https://stackoverflow.com/questions/39112540/randomly-connected-graph-generator
     * https://en.wikipedia.org/wiki/Fisher–Yates_shuffle#The_modern_algorithm
     * https://networkx.org/documentation/stable/reference/generators.html
     * a, b = b, a
     */
    public static Graph GenerateRandomGraph(int seed, int nodeCount,
        int extraEdgeCount,
        int minCost, int maxCost)
    {
        if (minCost > maxCost)
            throw new ArgumentException($"minCost must be smaller than minCost!");
        
        var rand = new Random(seed);
        var nodes = new NeoNode[nodeCount];
        
        // Create nodes
        for (var i = 0; i < nodeCount; i++)
        {
            nodes[i] = new NeoNode(i.ToString(), rand.Next(minCost, maxCost));
        }

        // Create undirected/symmetric adjacency matrix
        AdjacencyMatrix<string> adjMatrix = new(nodes.Select(n => n.Name).ToArray(), true);
        
        // Create edges for every pair
        for (var i = 0; i < nodes.Length - 1; i++)
        {
            adjMatrix.AddAdjacency(nodes[i].Name, nodes[i + 1].Name);
            if(DEBUG_Prints)
            Console.WriteLine($"{i}, {i+1}");
        }
        
        Random random = new(seed);
        FisherShuffle.ShuffleArray(ref nodes, random);
        
        // Create edges for every pair
        for (var i = 0; i < extraEdgeCount; i++)
        {
            adjMatrix.AddAdjacency(nodes[i].Name, nodes[i + 1].Name);
            
            if(DEBUG_Prints)
            Console.WriteLine($"{i}, {i+1}");
        }


        Dictionary<string, int> toBeFrozenNodes = new (nodes.Count());
        foreach (var completeNode in nodes)
            toBeFrozenNodes.Add(completeNode.Name, completeNode.Cost);


        
        return new Graph(toBeFrozenNodes.ToFrozenDictionary(), adjMatrix);
    }
}