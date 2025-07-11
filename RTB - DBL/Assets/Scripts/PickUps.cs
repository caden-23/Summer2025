using UnityEngine;

public class PickUps : MonoBehaviour
{
    private Vector3 initialPosition;
    private float despawnTime;
    public float respawnDelay = 5f; // Time before respawn



    void Start()
    {
        initialPosition = transform.position; // Store initial position
    }

    // Called when pickup is collected
    public void Collect()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        despawnTime = Time.time;
        // Add additional logic here (e.g., inventory, sound)
    }

    // Resets the pickup
    public void ResetPickup()
    {
        transform.position = initialPosition;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    void Update()
    {
        if (Time.time - despawnTime > respawnDelay && !GetComponent<MeshRenderer>().enabled)
        {
            ResetPickup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}
