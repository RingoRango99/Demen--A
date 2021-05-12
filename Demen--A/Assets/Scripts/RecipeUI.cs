using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeUI : MonoBehaviour
{

    public GameManager gamemanager;
    public PlayerMove playermove;

    public TMP_Text recipeText;

    public Camera playerCam;
    public Camera recipeCam;

    public bool active;

    public GameObject recipeBoard;

    // Start is called before the first frame update
    void Start()
    {
        playerCam.enabled = true;
        recipeCam.enabled = false;


        gamemanager = GameObject.Find("GameController").GetComponent<GameManager>();
        playermove = GameObject.Find("Player").GetComponent<PlayerMove>();
        recipeBoard = GameObject.Find("RecipeBoard");

        recipeBoard.SetActive(false);
        // start a co-routine to populate recipe board but wait for 2 seconds
        // before populating the text
        StartCoroutine(RecipeListText(2));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            // when R is pressed disable/enable recipe board game object
            // disable/enable player and recipe cams to either show
            // recipe board or the game world
            gamemanager.cursorstate = !gamemanager.cursorstate;
            active = !active;
            recipeBoard.SetActive(active);
            playermove.enabled = !playermove.enabled;
            playerCam.enabled = !playerCam.enabled;
            recipeCam.enabled = !recipeCam.enabled;
        }

        
    }

    IEnumerator RecipeListText(float waitTime)
    {
        // updates recipe board text with items from recipe array
        // after 2 seconds of wait time when initiated at the start of the script
        yield return new WaitForSeconds(waitTime);
        recipeText.text = "Recipe:" + "\n" + "1: " + gamemanager.recipe[0].name.ToString() + "\n" + "2: " + gamemanager.recipe[1].name.ToString() + "\n" + "3: " + gamemanager.recipe[2].name.ToString()
            + "\n" + "4: " + gamemanager.recipe[3].name.ToString() + "\n" + "5: " + gamemanager.recipe[4].name.ToString() + "\n" + "6: " + gamemanager.recipe[5].name.ToString()
            + "\n" + "7: " + gamemanager.recipe[6].name.ToString();

    }


}
