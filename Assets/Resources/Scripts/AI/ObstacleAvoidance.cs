using UnityEngine;
using System.Collections;

public class ObstacleAvoidance : MonoBehaviour {

    /*
    private int range;
    private float speed;
    private bool obstacleToAvoid = false;
    // Specify the target for the enemy.
    public GameObject target;
    private float rotationSpeed;
    private RaycastHit hit;
    // Use this for initialization
    void Start()
    {
        range = 80;
        speed = 10f;
        rotationSpeed = 15f;
    }
    // Update is called once per frame
    void Update()
    {
       
        //Checking for any Obstacle in front.
        // Two rays left and right to the object to detect the obstacle.
        Transform leftRay = transform;
        Transform rightRay = transform;
        //Use Phyics.RayCast to detect the obstacle
        if (Physics.Raycast(leftRay.position + (transform.right * 1), transform.forward, out hit, range) || Physics.Raycast(rightRay.position - (transform.right * 1), transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                obstacleToAvoid = true;
                transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                Debug.Log("Obstacle in front HIt detected");
            }
        }
        // Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
        // Just making this boolean variable false it means there is nothing in front of object.
        if (Physics.Raycast(transform.position - (transform.forward * 1), transform.right, out hit, 30) ||
        Physics.Raycast(transform.position - (transform.forward * 1), -transform.right, out hit, 30))
        {
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                obstacleToAvoid = false;
            }
        }
        // Use to debug the Physics.RayCast.
        Debug.DrawRay(transform.position + (transform.right * 7), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.right * 7), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.forward * 4), -transform.right * 20, Color.yellow);
        Debug.DrawRay(transform.position - (transform.forward * 4), transform.right * 20, Color.yellow);
    }*/
}
