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
    
    public static Graph GenerateRandomGraph(int seed, int nodeCount,
        int minEdges, int maxEdges,
        int minCost, int maxCost)
    {
        var rand = new Random(seed);
        var nodes = new NeoNode[nodeCount];

        // Create nodes
        for (var i = 0; i < nodeCount; i++)
        {
            nodes[i] = new NeoNode(i.ToString(), rand.Next(minCost, maxCost));
        }

        // Create undirected/symmetric adjacency matrix
        AdjacencyMatrix<string> adjMatrix = new(nodes.Select(n => n.Name).ToArray(), true);

        // Add an edge from every node/vertext to a random other node.
        // Note: Can generate loop
        for (var i = 1; i < nodeCount; i++)
        {
            var randPreviousNodeIndex = rand.Next(0, i);
            adjMatrix.SetAdjacency(nodes[i].Name, nodes[randPreviousNodeIndex].Name, true);
        }

        List<NeoNode> incompleteNodes = new (nodes);
        List<NeoNode> completeNodes = new (nodeCount);

        while (incompleteNodes.Count > 0)
        {
            // Pop node from list
            var node = incompleteNodes[incompleteNodes.Count - 1];
            incompleteNodes.RemoveAt(incompleteNodes.Count - 1);

            if (adjMatrix.CountEdgesFrom(node.Name) < maxEdges)
            {
                var localMaxEdges = (int)Math.Max(Math.Ceiling(Math.Sqrt(maxEdges + 1)), minEdges);
                var targetEdgeCount = rand.Next(minEdges, localMaxEdges);
                int currentEdgeCount = adjMatrix.CountEdgesFrom(node.Name);
                int attemptCount = 0;
                int maxAttempts = nodeCount * 2;

                while (currentEdgeCount - targetEdgeCount > 0 && incompleteNodes.Count > 0 &&
                       attemptCount < maxAttempts)
                {
                    var otherNodeIndex = rand.Next(0, incompleteNodes.Count);
                    attemptCount++;

                    // Prevent adding edges to nodes that already have the maximum number of edges
                    // Prevent adding an edge that already exists
                    if (adjMatrix.CountEdgesFrom(incompleteNodes[otherNodeIndex].Name) >= maxEdges
                        || adjMatrix.GetAdjacency(node.Name, incompleteNodes[otherNodeIndex].Name))
                        continue;

                    adjMatrix.AddAdjacency(node.Name, incompleteNodes[otherNodeIndex].Name);
                    currentEdgeCount++;
                    if(DEBUG_Prints)
                        Console.WriteLine($"Attempt count: {attemptCount}");
                    attemptCount = 0;
                }

                if (DEBUG_Prints && attemptCount > 0)
                    Console.WriteLine($"Abandoned due to attempt count: {attemptCount}");
            }

            completeNodes.Add(node);
        }

        Dictionary<string, int> toBeFrozenNodes = new (completeNodes.Count());
        foreach (var completeNode in completeNodes)
            toBeFrozenNodes.Add(completeNode.Name, completeNode.Cost);            
        
        return new Graph(toBeFrozenNodes.ToFrozenDictionary(), adjMatrix);
    }
}