using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // variables //
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    [SerializeField] private float speed = 0;
    void Start()
    {
        // gets the rigidbody to the player component //
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        // Movement vector using x & y variables //
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // moves the player
        rb.AddForce(force: movement * speed);
    }

    void OnMove(InputValue movementValue)
    {
        // convert input value into a vector2 for movement 
        Vector2 movementVector = movementValue.Get<Vector2>();

        // X & Y components = movement 
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    void OnTriggerEnter(Collider other) 
    {
     // Check if the object the player collided with has the "PickUp" tag.
 if (other.gameObject.CompareTag("PickUp")) 
        {
           // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
        }
    }
}
