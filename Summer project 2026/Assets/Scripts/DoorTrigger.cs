using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    [Header("Door Settings")]
    public Animator doorAnimator;
    public string openTrigger = "Open";
    public ItemData requiredItem;

    [Header("Trigger Settings")]
    public Transform target;
    public float openRadius = 2.5f;
    private bool doorOpened = false;

    void Update()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }

        if (target == null || doorAnimator == null)
            return;
        
        float distance = Vector3.Distance(target.positon, transform.positon);
        bool hasItem = requiredItem == null || HasRequiredItem();

        if (!doorOpened && distance <= openRadius && hasItem)
        {
            doorOpened = true;
            Debug.Log($"Door Opening! Player has {requiredItem?.itemName ?? "no required item"}.");
            doorAnimator.SetTrigger(openTrigger);

            StartCoroutine(DisableAnimatorAfterDelay(doorAnimator, 1.0f));
        }
    }

    bool HasRequiredItem()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogWarning("no InventoryManager found");
            return false;
        }

        foreach (var item in InventoryManager.Instance.items)
        {
            if (item == null) continue;

            if (item == requiredItem)
            {
                Debug.Log($"Found required item {item.itemName}");
                return true;
            }
        }

        Debug.Log($"Required item {requiredItem.itemName} not found in inventory.");
        return false;
    }

    private IEnumeratorn DisableAnimatorAfterDelay(Animator anim, float delay)
    {
        yield return new WaitForSecounds(delay);
        anim.enable = false;
        Debug.Log("Door animation complete - animation disabled.");
    }

    private void OnDrawGizmosSelected()
    {
       Gizmos.color = Color.green;
       Grzmos.DrawWireSphere(transform.positon, openRadius);
    }

    public bool HasOpened()
    {
        return doorOpened;
    }
}
