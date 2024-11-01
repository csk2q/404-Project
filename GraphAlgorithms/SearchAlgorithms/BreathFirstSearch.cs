using GraphAlgorithms.DataStructures;
using GraphAlgorithms.Logic;

namespace GraphAlgorithms.SearchAlgorithms;

public class BreathFirstSearch : ISearchAlgorithm
{
    public string[] RunSearch(string start, string goal, in Graph graph)
    {
        if (start == goal)
            return [start];
        
        TreeNode root = new(start);
        TreeNode? goalNode = null;
        HashSet<string> seenNodes = [start];
        List<TreeNode> leafNodes = [root];

        while (goalNode is null && leafNodes.Count > 0)
        {
            List<TreeNode> newLeaves = [];
            foreach (TreeNode leafNode in leafNodes)
            {
                var adjNodes = graph.Edges.GetAdjacentNodes(leafNode.Name);
                foreach (var adjNodeName in adjNodes)
                {
                    // Add new leaf nodes
                    if (!seenNodes.Contains(adjNodeName))
                    {
                        newLeaves.Add(leafNode.AddChild(adjNodeName));
                        seenNodes.Add(adjNodeName);
                    }

                    // Break out if we found our goal
                    if (adjNodeName == goal)
                    {
                        goalNode = newLeaves.Last();
                        break;
                    }
                }
                
                // Break out if we found our goal
                if(goalNode is not null)
                    break;
            }
            
            // Update new leaf nodes
            leafNodes = newLeaves;
        }

        //Travers node chain to start
        LinkedList<string> path = [];
        for (TreeNode? curNode = goalNode; curNode is not null; curNode = curNode.Parent)
            path.AddFirst(curNode.Name);
        //Return path
        return path.ToArray();
    }
}