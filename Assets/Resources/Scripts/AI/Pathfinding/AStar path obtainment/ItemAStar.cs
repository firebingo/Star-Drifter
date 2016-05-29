using UnityEngine;
using System.Collections;

public class ItemAStar : MonoBehaviour {
    private Transform startPos, endPos;
    public Node2 startNode { get; set; }
    public Node2 goalNode { get; set; }
    public ArrayList pathArray;
    GameObject startPosition, endPosition;
    private float elapsedTime = 0.0f;
    //checking current states 
    public PirateAssassin assassin;

    //Interval time between pathfinding
    public float intervalTime = 1.0f;

    void Start()
    {
        assassin = gameObject.GetComponent<PirateAssassin>();
        
        startPosition = gameObject.gameObject;
      //  endPosition = assassin.closestItem.position;

        pathArray = new ArrayList();

       // FindPath();
    }
    void Update()
    {
        //if (assassin.AIState == PirateAssassin.AIFSM.Raid) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= intervalTime)
            {
                elapsedTime = 0.0f;

                FindPath();

            }
        //}

    }
    void FindPath()
    {
        startPos = startPosition.transform;
        endPos = assassin.closestItem;
        startNode = new Node2(GridManager.instance.GetGridCellCenter(
        GridManager.instance.GetGridIndex(startPos.position)));
        goalNode = new Node2(GridManager.instance.GetGridCellCenter(
        GridManager.instance.GetGridIndex(endPos.position)));
        pathArray = AStar.FindPath(startNode, goalNode);
    }
    
   /* void OnDrawGizmos()
    {
        if (pathArray == null)
            return;
        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach (Node2 node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node2 nextNode = (Node2)pathArray[index];
                    Debug.DrawLine(node.position, nextNode.position,
                    Color.green);
                    index++;
                }
            }
        }
    }*/
}
