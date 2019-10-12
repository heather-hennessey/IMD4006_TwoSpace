using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool GameHasEnded = false;
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Demo");
        Time.timeScale = 1f;
        FindObjectOfType<ScoreController>().ResetScore();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

   public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void EndGame()
    {
        if(GameHasEnded == false)
        {
            GameHasEnded = true;
            SceneManager.LoadScene("EndMenu");
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
