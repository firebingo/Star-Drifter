using UnityEngine;
using System.Collections;

public class ObstacleAvoidance : MonoBehaviour
{


    private int range;
    private float speed;
    private bool obstacleToAvoid = false;
    // Specify the target for the enemy.
    public GameObject targetObj;
    private Vector3 target;
    private float rotationSpeed;
    private RaycastHit hit;
    Vector3 targetHeading;
    Vector3 targetDirection;
    Vector3 targetPosition = new Vector3(0, 0, 0);
    public Rigidbody2D rb;


    //pathfiding
    Vector3 start;
    Vector3 end;

   

    // Use this for initialization
    void Start()
    {
        range = 80;
        speed = 10f;
        rotationSpeed = 15f;
        rb = GetComponent<Rigidbody2D>();
        targetObj = GameObject.FindGameObjectWithTag("Player");
        target = targetObj.transform.position;

        start = transform.position;
        end = target;
    }
    // Update is called once per frame
    void Update()
    {



    }

}
