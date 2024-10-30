using GraphAlgorithms.DataStructures;
using static GraphAlgorithms.GlobalData;

namespace GraphAlgorithms.OldSearchAlgorithms;

public class BestFirstOldSearch() : OldSearchMethod()
{
    // Keep and open list of frontier nodes and always pick the closest option
    public override string[] RunSearch(string start, string goal)
    {
        if (start == goal)
            return [start];

        TreeNode root = new(start);
        TreeNode? goalNode = null;
        HashSet<string> seenNodes = [start];

        // Priority queue is a min heap
        PriorityQueue<TreeNode, float> leafNodes = new();
        leafNodes.Enqueue(root, distanceBetween(start, goal));

        while (goalNode is null)
        {
            // Get the node closest to goal
            TreeNode closestToGoalNode = leafNodes.Dequeue();

            // For each adjacent node
            foreach (var adjNodeName in adjacencies[closestToGoalNode.Name])
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
                    leafNodes.Enqueue(closestToGoalNode.AddChild(adjNodeName), distanceBetween(adjNodeName, goal));
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