using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public void Pause()
    {
        if(GameIsPaused)
        {
            Time.timeScale = 1.0f;
            GameIsPaused = false;
        }
        else{
            Time.timeScale = 0.0f;
            GameIsPaused = true;
        }
    }

    public void BackToMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
