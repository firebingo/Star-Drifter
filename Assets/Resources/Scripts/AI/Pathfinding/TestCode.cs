using UnityEngine;
using System.Collections;
public class TestCode : MonoBehaviour
{
    private Transform startPos, endPos;
    public Node2 startNode { get; set; }
    public Node2 goalNode { get; set; }
    public ArrayList pathArray;
    GameObject startPosition, endPosition;
    private float elapsedTime = 0.0f;
    //Interval time between pathfinding
    public float intervalTime = 1.0f;
    //getting current state 
    public AlienDefender state;


    void Start()
    {
        state = gameObject.GetComponent<AlienDefender>();
        //startPosition = GameObject.FindGameObjectWithTag("Start");
     //    endPosition = GameObject.FindGameObjectWithTag("End");
        startPosition = gameObject.gameObject;
        endPosition = GameObject.FindGameObjectWithTag("Player");
        pathArray = new ArrayList();
       // FindPath();
    }
    void Update()
    {
        state = gameObject.GetComponent<AlienDefender>();

        if (endPosition == null)
        {
            Debug.Log("no player present");
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            elapsedTime = 0.0f;
            if (state.AIState == AlienDefender.AIFSM.ObAv)
            {
                FindPath();
            }
           
        }
    }
    void FindPath()
    {
        startPos = startPosition.transform;
        endPos = endPosition.transform;
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
