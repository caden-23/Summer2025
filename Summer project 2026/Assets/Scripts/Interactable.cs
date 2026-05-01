using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Info")]
    public string promptMessage = "Press E to Interact";
    public virtual void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
    
}
