using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public RecipeUI recipeUI;
    public ItemSpawn itemSpawnM;
    public UIManager uiManager;

    public List<GameObject> collectedItems = new List<GameObject>();
    public List<GameObject> recipeList = new List<GameObject>();

    public GameObject[] recipe;
    public GameObject[] items;

    public TMP_Text timerText;
    public float secondsC;
    public int minutesC;
    public int hoursC;

    public bool cursorstate;

    public float timeUntilConfusion;

    // Start is called before the first frame update
    void Start()
    {
        recipeUI = GameObject.Find("GameController").GetComponent<RecipeUI>();
        itemSpawnM = GameObject.Find("RandomSpawnHandler").GetComponent<ItemSpawn>();
        uiManager = GameObject.Find("GameController").GetComponent<UIManager>();

        cursorstate = false;

        RandomTimePicker();
    }

    void RandomTimePicker()
    {
        // pick a random time between 15 seconds and 3 minutes
        timeUntilConfusion = UnityEngine.Random.Range(15, 180);
        

    }

    private void FixedUpdate()
    {
        if (timeUntilConfusion > 0)
        {
            // if timer is more than 0
            // start countdown to 0
            timeUntilConfusion -= Time.deltaTime;
        }

        if (timeUntilConfusion <= 0)
        {
            // if timer until confusion is 0 
            // initiate confusion mechanic
            // reset timer
            RandomTimePicker();
            itemSpawnM.ConfuseActivate();

        }
        // if cursor state is changed
        // make cursor visible/hidden
        if (cursorstate == true)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }

        UpdateTimer();
    }

    void UpdateTimer()
    {
        // timer counting up to indicate time spent in game
        // with time being formatted to look like a clock
        secondsC += Time.deltaTime;
        timerText.text = hoursC + "h:" + minutesC + "m:" + (int)secondsC + "s";
        uiManager.timerText.text = "Time: " + hoursC + "h:" + minutesC + "m:" + (int)secondsC + "s";
        if (secondsC >= 60)
        {
            minutesC++;
            secondsC = 0;
        }
        else if (minutesC >= 60)
        {
            hoursC++;
            minutesC = 0;
        }


    }

    public bool compare (GameObject[] rec, GameObject[] item)
    {
        // if recipe length isnt the same as item length then return false
        if (rec.Length != item.Length)
        {
            return false;
        }


        // for every slot of recipe length
        for (int i = 0; i < rec.Length; i++)
        {
            // it recipe slot isnt equal to item slot return false
            if (!rec[i].name.Equals(item[i].name))
            {

                return false;

            } 
            else
            {

                return true;// else return true

            }
        }

        return true;

    }

    public void Collect(GameObject collect)
    {
        // add collected item into list
        collectedItems.Add(collect);
        // convert item list to array
        items = collectedItems.ToArray();

        if (compare(recipe, items) == true)
        {
            // compares item list to recipe and if true
            // tell ui manager script its game over
            uiManager.GameOver();
        }

    }

    public void Updatelist(GameObject recipeadd)
    {
        // add items to recipe as they are spawned
        recipeList.Add(recipeadd);
        // convert list to array
        recipe = recipeList.ToArray();
    }

    public void LockCursor()
    {
        // locks cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("cursor locked");

    }

    public void UnlockCursor()
    {
        // unlocks cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("cursor Unlocked");

    }

    public void ResetCollection()
    {
        // resets collection lists when
        // items are being reset
        collectedItems.Clear();
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = null;
        }
        

    }

    public void RestartGame()
    {
        // restarts the game
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        // quits the gamme
        Application.Quit();
    }

}
