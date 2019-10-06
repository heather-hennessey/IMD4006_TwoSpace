using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool GameHasEnded = false;
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    
    public void StartGame()
    {
        Debug.Log("START GAME");
        SceneManager.LoadScene("Demo");
        Time.timeScale = 1f;
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
        Debug.Log("pause");
    }

   public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Resume");
    }

    public void MainMenu()
    {
        Debug.Log("MAIN MENU");
        SceneManager.LoadScene("MainMenu");
    }
    
    public void EndGame()
    {
        if(GameHasEnded == false)
        {
            GameHasEnded = true;
            Debug.Log("GAME OVER");

            //show end screen and score
            SceneManager.LoadScene("EndMenu");
        }
    }

    public void QuitApplication()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
