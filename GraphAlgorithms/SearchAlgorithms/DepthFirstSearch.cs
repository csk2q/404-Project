using GraphAlgorithms.Logic;

namespace GraphAlgorithms.SearchAlgorithms;

public class DepthFirstSearch : ISearchAlgorithm
{
    public string[] RunSearch(string start, string goal, in Graph graph)
    {
        if (start == goal)
            return [start];
        
        //Use a stack to keep track of past paths.
        HashSet<string> failedNodes = [];
        Stack<string> path = [];
        path.Push(start);

        string curNode = start;
        while (path.Count > 0 && path.Peek() != goal)
        {
            var adjNodes = graph.Edges.GetAdjacentNodes(curNode);
            List<string> newAdjNodes = [];

            //For each adjacent node
            foreach (var adjNode in adjNodes)
            {
                //Add undiscovered nodes
                if (!failedNodes.Contains(adjNode) && !path.Contains(adjNode))
                    newAdjNodes.Add(adjNode);
            }

            if (newAdjNodes.Count > 0)
            {
                // Step forward
                curNode = newAdjNodes[0];
                path.Push(curNode);
            }
            else
            {
                // Step back
                failedNodes.Add(curNode);
                path.Pop();
                curNode = path.Peek();
            }

        }

        return path.Reverse().ToArray();
    }
}