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

        for (var i = 1; i < nodeCount; i++)
        {
            var targetEdgeCount = rand.Next(minEdges, maxEdges + 1);
            int edgesNeeded = adjMatrix.CountEdgesFrom(nodes[i]) - targetEdgeCount;

            for (int j = 0; j < edgesNeeded; j++)
            {
                int attemptCount = 0;
                
                var otherNodeIndex = rand.Next(0, nodeCount);
                
                // Prevent self loops
                while ((otherNodeIndex == i || adjMatrix.CountEdgesFrom(nodes[otherNodeIndex]) >= maxEdges) && attemptCount < nodeCount - 1)
                {
                    otherNodeIndex = rand.Next(0, nodeCount);
                    attemptCount++;
                }
                
                if (attemptCount < nodeCount - 1)
                    adjMatrix.AddAdjacency(nodes[i], nodes[otherNodeIndex]);
            }
            
        }

        return new Graph(nodes, adjMatrix);
    }
}