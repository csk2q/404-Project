using System.Numerics;

namespace GraphAlgorithms.DataStructures;


public class NeoNode
{
    /* TODO Make a node with the following features:
     * Name
     * Links to other nodes
     * Position?
     * Cost?
     */
    
    public string Name;
    // public List<NeoNode> LinkedNodes;
    // public Vector2 Position;
    public decimal Cost;

    // public NeoNode(string name, Vector2 position, decimal cost, List<NeoNode>?  linkedNodes = null)
    public NeoNode(string name, decimal cost)
    {
        this.Name = name;
        // this.LinkedNodes = linkedNodes ?? [];
        // this.Position = position;
        this.Cost = cost;
    }


}