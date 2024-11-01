using GraphAlgorithms.DataStructures;
using GraphAlgorithms.Logic;

namespace GraphAlgorithms.SearchAlgorithms;

public class BreathFirstSearch : ISearchAlgorithm
{
    private const bool DEBUG_PRINT = false;
    
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

    public static void DebugRun(int searchCount, Graph graph, int seed = 100)
    {
        Random rnd = new Random(seed);
        var pathLengths = new int[searchCount];

        for (var i = 0; i < searchCount; i++)
        {
            var x = rnd.Next(0, graph.nodeAndCost.Count);

            for (var j = 0; j < searchCount; j++)
            {
                var y = rnd.Next(0, graph.nodeAndCost.Count);


                pathLengths[i] = new BreathFirstSearch().RunSearch(x.ToString(), y.ToString(), graph).Length;
            }

            if (DEBUG_PRINT)
                Console.WriteLine("Finished i=" + i + $" {pathLengths[i]}");
        }

        if (DEBUG_PRINT)
        {
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

        }
    }
}