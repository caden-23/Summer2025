using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    private bool gameHasEnded;
    void Start()
    {
        gameHasEnded = false;
    }

    public void EndGame()
    {
        if (gameHasEnded != true)
        {   
            
            gameHasEnded = true;
            Debug.Log("Game Over!");
            Invoke("ResetLevel", 2f);
        }
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the current scene
        Debug.Log("working");
        
    }
}
