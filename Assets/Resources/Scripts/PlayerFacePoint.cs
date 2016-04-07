using UnityEngine;
using System.Collections;

public class PlayerFacePoint : MonoBehaviour
{
    private Vector2 lastMousePosition; //the alst mouse position used for comparison.
    [SerializeField]
    private GameObject objectFacing; //the object that is going to face this object.

    // Use this for initialization
    void Start()
    {
        var coords = Camera.current.ScreenToWorldPoint(Input.mousePosition);
        lastMousePosition.x = coords.x;
        lastMousePosition.y = coords.y;
    }

    // Update is called once per frame
    void Update()
    {
        //if the mouse position has changed, move the pointer to that position.
        var currentCoords = (Vector2)(Camera.current.ScreenToWorldPoint(Input.mousePosition));

        if (Vector2.SqrMagnitude(currentCoords - lastMousePosition) > 0.0025)
        {
            this.transform.position = currentCoords;
            lastMousePosition = currentCoords;
        }
        //otherwise follow controller input.
        else
        {
            var horizontal = Input.GetAxis("LookHorizontal");
            var vertical = Input.GetAxis("LookVertical");
            if (horizontal < -0.025f || horizontal > 0.025f || vertical > 0.025f || vertical < -0.025f)
            {
                var basePosition = (Vector2)objectFacing.transform.position;
                this.transform.position = new Vector2(horizontal, vertical).normalized + basePosition;
            }
        }
    }
}
