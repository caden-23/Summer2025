using UnityEngine;
using System.Collections.Generic;

public class PickupManager : MonoBehaviour
{
    public GameObject pickupPrefab;
    public Transform[] roadSpawnPoints;
    public int totalPickups = 5;

    private List<GameObject> activePickups = new List<GameObject>();

    void Start()
    {
        SpawnAllPickups();
    }

    public void OnPickupCollected(GameObject pickup)
    {
        activePickups.Remove(pickup);

        if (activePickups.Count == 0)
        {
            Invoke(nameof(SpawnAllPickups), 2f); // Optional delay
        }
    }

    void SpawnAllPickups()
    {
        activePickups.Clear();

        List<int> usedIndices = new List<int>();

        for (int i = 0; i < totalPickups; i++)
        {
            // Choose random unused spawn point
            int index;
            do {
                index = Random.Range(0, roadSpawnPoints.Length);
            } while (usedIndices.Contains(index) && usedIndices.Count < roadSpawnPoints.Length);

            usedIndices.Add(index);
            Transform spawnPoint = roadSpawnPoints[index];

            GameObject pickup = Instantiate(pickupPrefab, spawnPoint.position, Quaternion.identity);
            pickup.GetComponent<PickUps>().SetManager(this);
            activePickups.Add(pickup);
        }
    }
}
