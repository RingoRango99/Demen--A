using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    public GameObject playerUI;
    public GameObject blackoutImage;
    public GameObject player;

    public GameManager gamemanager;
    public MouseInfo mouseInfo;

    public TMP_Text timerText;
    public TMP_Text blackoutText;

    public Image crosshair;

    public bool confused;

    public Material matToChange;

    public AudioClip heavyBreathing;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = GameObject.Find("GameOver");
        pauseMenuScreen = GameObject.Find("PauseMenu");
        gamemanager = GameObject.Find("GameController").GetComponent<GameManager>();
        playerUI = GameObject.Find("PlayerUI");
        mouseInfo = GameObject.Find("GameController").GetComponent<MouseInfo>();

        StartCoroutine(GettingConfused(false));

        gameOverScreen.SetActive(false);
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1;

        playerUI.SetActive(true);
        confused = false;

        player.GetComponent<AudioSource>().playOnAwake = false;
        player.GetComponent<AudioSource>().clip = heavyBreathing;
    }

    public IEnumerator GettingConfused(bool fadeToBlack = true, int fadeSpeed = 3)
    {
        // takes color from blackout image
        Color imageColor = blackoutImage.GetComponent<Image>().color;
        // variable for fade ammount
        float fadeAmount;


        if (fadeToBlack)
        {
            //play audio clip
            player.GetComponent<AudioSource>().PlayOneShot(heavyBreathing);

            blackoutText.enabled = true;
            // if fade to black is true and while blackout image color a is less than 1
            while (blackoutImage.GetComponent<Image>().color.a < 1)
            {
                
                // set fade ammount by colour a ammount and fadespeed 
                fadeAmount = imageColor.a + (fadeSpeed * Time.deltaTime);
                // set image color as new color by fade ammount
                imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, fadeAmount);
                // set blackout image colour to it's new faded color
                blackoutImage.GetComponent<Image>().color = imageColor;
                yield return null;
            }

        }
        else
        {
            blackoutText.enabled = false;

            while (blackoutImage.GetComponent<Image>().color.a > 0)
            {
                // samme as previous while but reveresed to undo the black out
                fadeAmount = imageColor.a - (fadeSpeed * Time.deltaTime);
                imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, fadeAmount);
                blackoutImage.GetComponent<Image>().color = imageColor;
                yield return null;
            }
        }
        yield return new WaitForEndOfFrame();

    }

    public void Confused()
    {
        // change item materials when confuse is activated
        for (int i = 0; i < gamemanager.recipe.Length; i++)
        {
            gamemanager.recipe[i].GetComponent<MeshRenderer>().material = matToChange;
        }

        StartCoroutine(GettingConfused());

        StartCoroutine(BackToNormal());

        

    }

    public IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(6);
        // returns player back to normal and sets
        // and sets blackout back so the screen is normal again
        StartCoroutine(GettingConfused(false));

    }

    public void GameOver()
    {
        // unlock cursor, set time to 0 to pause the game
        // and show game over screen
        gamemanager.UnlockCursor();
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        playerUI.SetActive(false);

    }

    public void PauseGame()
    {
        // pause game
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
        gamemanager.UnlockCursor();
        playerUI.SetActive(false);

    }

    public void UnPauseGame()
    {
        // unpause game
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
        gamemanager.LockCursor();
        playerUI.SetActive(true);
    }

    public void SetMouseInfo(GameObject item)
    {

        mouseInfo.mouseInfoLoc.transform.position = item.transform.position;
        if (confused == false)
        {
            mouseInfo.mouseInfoText.text = "Press left click to pick up " + item.name;
        }
        else
        {
            mouseInfo.mouseInfoText.text = "Press left click to pick up ingredient";
        }
        
        mouseInfo.mouseInfo.enabled = true;

    }

    public void DisableMouseInfo()
    {

        mouseInfo.mouseInfo.enabled = false;

    }
}
