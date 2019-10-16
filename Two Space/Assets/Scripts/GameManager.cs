using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool GameHasEnded = false;
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    public KeyCode ResetKey;
    public KeyCode ResetKey2;
    
    public void StartGame()
    {
        SceneManager.LoadScene("TwoSpace");
        Time.timeScale = 1f;
        FindObjectOfType<ScoreController>().ResetScore();

        FindObjectOfType<SoundManagerScript>().PlaySound("ambient");
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

        if (Input.GetKeyDown(ResetKey) || Input.GetKeyDown(ResetKey2))
        {
            StartGame();
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
        FindObjectOfType<ScoreController>().ResetScore();
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
