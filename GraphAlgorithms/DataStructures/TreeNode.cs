using GraphAlgorithms.SearchMethods;

namespace GraphAlgorithms.DataStructures;

public class TreeNode()
{
    public int depth = 0;
    
    public string Name = "Name not set";
    public TreeNode? Parent = null;
    public List<TreeNode> Children = [];

    public TreeNode(string name) : this()
    {
        Name = name;
    }

    public TreeNode AddChild(string name)
    {
        var node = new TreeNode
        {
            Name = name,
            Parent = this,
            depth = depth + 1,
        };
        Children.Add(node);
        return node;
    }

    public override string ToString()
    {
        return Name;
    }
}
