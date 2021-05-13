using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverScreen;

    public GameManager gamemanager;

    public TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = GameObject.Find("GameOver");
        gamemanager = GameObject.Find("GameController").GetComponent<GameManager>();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }


    public void GameOver()
    {
        // unlock cursor, set time to 0 to pause the game
        // and show game over screen
        gamemanager.UnlockCursor();
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);

    }
}
