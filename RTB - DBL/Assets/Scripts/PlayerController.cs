using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // variables //
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    [SerializeField] private float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject loseTextObject;
    public GameObject introTextObject;
    public GameObject wintextObject;


    void Start()
    {

        // gets the rigidbody to the player component //
        rb = GetComponent<Rigidbody>();

        // Freeze rotation on X and Z to prevent tipping or rolling
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        count = 0;
        SetCountText();
        loseTextObject.SetActive(false);
        wintextObject.SetActive(false);
        introTextObject.SetActive(true);
        Invoke("IntroReset", 2f);

    }
    public void IntroReset()
    {
        introTextObject.SetActive(false);
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


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            wintextObject.SetActive(true);
            FindFirstObjectByType<GameManager>().EndGame();
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));

        }
    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            loseTextObject.SetActive(true);
            FindFirstObjectByType<GameManager>().EndGame();
        }
    }
}
