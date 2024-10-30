namespace GraphAlgorithms.DataStructures;

public class AdjacencyMatrix<T> where T : notnull
{

    private Dictionary<T, int> edgeCount;
    private readonly Dictionary<T, Dictionary<T, bool>> matrix;

    public readonly bool Symmetric;
    public int Size => matrix.Count;

    public AdjacencyMatrix(T[] nodes, bool isSymmetric = false)
    {
        if (nodes.Length < 1)
            throw new ArgumentException("Not enough nodes. Please provide at least one node.");

        Symmetric = isSymmetric;

        // Create matrix
        matrix = new Dictionary<T, Dictionary<T, bool>>();


        edgeCount = new Dictionary<T, int>();
        var column = new Dictionary<T, bool>();
        foreach (var node in nodes)
        {
            column.Add(node, false);
            edgeCount.Add(node, 0);
        }

        // Copy the blank column to fill out the rows
        foreach (var node in nodes)
            matrix.Add(node, new Dictionary<T, bool>(column));
    }
    
    public void AddAdjacency(T x, T y) => SetAdjacency(x, y, true);

    public void SetAdjacency(T x, T y, bool value)
    {
        matrix[x][y] = value;
        edgeCount[x] += 1;
        if (Symmetric)
        {
            matrix[y][x] = value;
            edgeCount[y] += 1;
        }
    }

    public bool GetAdjacency(T x, T y) => matrix[x][y];

    public int CountEdgesFrom(T x)
    {
        return edgeCount[x];
        // int count = 0;
        //
        // foreach (var edge in matrix[x])
        //     if (edge.Value)
        //         count++;
        //
        // return count;
    }
}