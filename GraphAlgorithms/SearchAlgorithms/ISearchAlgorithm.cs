using GraphAlgorithms.Logic;

namespace GraphAlgorithms.SearchAlgorithms;

public interface ISearchAlgorithm
{
    public string[] RunSearch(string start, string goal, in Graph graph);
}