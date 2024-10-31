using System.Collections.Frozen;
using GraphAlgorithms.DataStructures;
using GraphAlgorithms.Logic;

namespace GraphAlgorithms.SearchAlgorithms;

public class BestFirstSearch : ISearchAlgorithm
{
    // Keep and open list of frontier nodes and always pick the closest option
    public string[] RunSearch(string start, string goal, in Graph graph)
    {
        if (start == goal)
            return [start];

        TreeNode root = new(start);
        TreeNode? goalNode = null;
        HashSet<string> seenNodes = [start];

        // Priority queue is a min heap
        PriorityQueue<TreeNode, int> leafNodes = new();

        foreach (var adjNode in graph.Edges.GetAdjacentNodes(start))
            leafNodes.Enqueue(root, graph.nodeAndCost[start]);

        while (goalNode is null)
        {
            // Get the node closest to goal
            TreeNode closestToGoalNode = leafNodes.Dequeue();

            // For each adjacent node
            foreach (var adjNodeName in graph.Edges.GetAdjacentNodes(closestToGoalNode.Name))
            {
                // If goal found, break out
                if (adjNodeName == goal)
                {
                    goalNode = closestToGoalNode.AddChild(adjNodeName);
                    break;
                }
                
                // Add node if new
                if (!seenNodes.Contains(adjNodeName))
                {
                    leafNodes.Enqueue(closestToGoalNode.AddChild(adjNodeName), graph.nodeAndCost[adjNodeName]);
                    seenNodes.Add(adjNodeName);
                }
            }
            
        }

        // Build and return path to goal
        LinkedList<string> path = [];
        for (TreeNode? curNode = goalNode; curNode is not null; curNode = curNode.Parent)
            path.AddFirst(curNode.Name);
        return path.ToArray();
    }
}