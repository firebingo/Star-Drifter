using UnityEngine;
using System.Collections;

using System;

public class Node2 : IComparable
{
    public float nodeTotalCost;
    public float estimatedCost;
    public bool bObstacle;
    public Node2 parent;
    public Vector3 position;
    public Node2()
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
    }
    public Node2(Vector3 pos)
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
        this.position = pos;
    }
    public void MarkAsObstacle()
    {
        this.bObstacle = true;

    }


    public int CompareTo(object obj)
    {
        Node2 node = (Node2)obj;
        //Negative value means object comes before this in the sort
        //order.
        if (this.estimatedCost < node.estimatedCost)
            //Positive value means object comes after this in the sort
            //order.
            if (this.estimatedCost > node.estimatedCost) return 1;
        return 0;
    }
}