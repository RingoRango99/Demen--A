using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadNextScene()
    {
        // loads the game scene when start is pressed
        SceneManager.LoadScene("Game");

    }

    public void QuitGame()
    {
        // quit game
        Application.Quit();

    }
}
