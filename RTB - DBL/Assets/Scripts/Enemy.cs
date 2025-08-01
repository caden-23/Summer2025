using UnityEngine;
using UnityEngine.SceneManagement; // for reloading the scene

public class Enemy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
    /*void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Roads"))
        {
            Destroy(this.gameObject);
        }
    }
    
    /*void KillPlayer(GameObject player)
    {
        Debug.Log("Player has been killed!");
        // You can do either:
        // 1. Destroy the player:
        // Destroy(player);
        Destroy(.gameObject);
        // 2. Reload the scene (simulate death)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
}