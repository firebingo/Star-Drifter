using UnityEngine;
using System.Collections;
public class PriorityQueue
{
    private ArrayList nodes = new ArrayList();
    public int Length
    {
        get { return this.nodes.Count; }
    }
    public bool Contains(object node)
    {
        return this.nodes.Contains(node);
    }
    public Node2 First()
    {
        if (this.nodes.Count > 0)
        {
            return (Node2)this.nodes[0];
        }
        return null;
    }
    public void Push(Node2 node)
    {
        this.nodes.Add(node);
        this.nodes.Sort();
    }
    public void Remove(Node2 node)
    {
        this.nodes.Remove(node);
        //Ensure the list is sorted
        this.nodes.Sort();
    }
}