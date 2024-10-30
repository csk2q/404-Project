using System.Numerics;
using GraphAlgorithms;
using GraphAlgorithms.DataStructures;

namespace GraphAlgorithms.Logic;

public record Graph(NeoNode[] Nodes, AdjacencyMatrix<NeoNode> Edges);

public class GraphGenerator
{
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
        AdjacencyMatrix<NeoNode> adjMatrix = new(nodes, true);

        // Create a minimum spanning tree from the existing nodes
        for (var i = 1; i < nodeCount; i++)
        {
            var randPreviousNodeIndex = rand.Next(0, i);
            adjMatrix.SetAdjacency(nodes[i], nodes[randPreviousNodeIndex], true);
        }

        // Add edges to nodes
        for (var i = 1; i < nodeCount; i++)
        {
            var localMaxEdges = (int)Math.Max(Math.Ceiling(Math.Sqrt(maxEdges + 1)), minEdges);
            var targetEdgeCount = rand.Next(minEdges, localMaxEdges);
            int currentEdgeCount = adjMatrix.CountEdgesFrom(nodes[i]);
            int attemptCount = 0;
            int maxAttempts = nodeCount * 2;

            while (currentEdgeCount - targetEdgeCount > 0 && attemptCount < maxAttempts)
            {
                var otherNodeIndex = rand.Next(0, nodeCount);
                attemptCount++;

                // Prevent self loops
                // Prevent adding edges to nodes that already have the maximum number of edges
                // Prevent adding an edge that already exists
                if (otherNodeIndex == i
                    || adjMatrix.CountEdgesFrom(nodes[otherNodeIndex]) >= maxEdges
                    || adjMatrix.GetAdjacency(nodes[i], nodes[otherNodeIndex]))
                    continue;
                

                adjMatrix.AddAdjacency(nodes[i], nodes[otherNodeIndex]);
                currentEdgeCount++;
                Console.WriteLine($"Attempt count: {attemptCount}");
                attemptCount = 0;
            }

            if (attemptCount > 0)
                Console.WriteLine($"Abandoned due to attempt count: {attemptCount}");
        }

        return new Graph(nodes, adjMatrix);
    }
}