using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] roadSpawnPoints;

    private bool gameHasEnded;
    public int winCount;

    public Camera firstPersonCam;
    public Camera thirdPersonCam;

    public GameObject Player;
    [SerializeField] public GameObject enemyPrefab;

    [SerializeField] private string enemyTag = "Enemy";

    public int roundThreshold = 2;

    void Start()
    {
        gameHasEnded = false;
        firstPersonCam.enabled = false;
        thirdPersonCam.enabled = true;

        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name == "FinalLevel") // check if this is the infinite level
        {
            StartCoroutine(SpawnWhilePlayerAlive());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            firstPersonCam.enabled = !firstPersonCam.enabled;
            thirdPersonCam.enabled = !thirdPersonCam.enabled;
        }
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the current scene
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Reloads the current scene

    }

    IEnumerator SpawnWhilePlayerAlive()
    {
        while (Player != null && Player.activeSelf)
        {
            int enemyCount = GameObject.FindGameObjectsWithTag(enemyTag).Length;

            if (enemyCount < 5)
            {
                SpawnEnemy();
            }

            SpawnPickup(); // your logic placeholder
            yield return new WaitForSeconds(2f); // Wait before checking again
        }
    }
    public void SpawnEnemy()
    {
        if (roadSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No road spawn points assigned!");
            return;
        }

        Transform point = roadSpawnPoints[Random.Range(0, roadSpawnPoints.Length)];

        // Snap to NavMesh or ground
        NavMeshHit hit;
        if (NavMesh.SamplePosition(point.position, out hit, 5f, NavMesh.AllAreas))
        {
            Instantiate(enemyPrefab, hit.position, point.rotation);
            Debug.Log("Enemy spawned at: " + point.name + " (snapped to NavMesh)");
        }
        else
        {
            Debug.LogWarning("No NavMesh found near spawn point: " + point.name);
        }
    }


    public void SpawnPickup()
    {
        // Placeholder – Add your pickup logic here if needed
    }
}