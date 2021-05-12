﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RecipeUI recipeUI;

    public List<GameObject> collectedItems = new List<GameObject>();
    public List<GameObject> recipeList = new List<GameObject>();

    public GameObject[] recipe;
    public GameObject[] items;

    public bool cursorstate;

    // Start is called before the first frame update
    void Start()
    {
        recipeUI = GameObject.Find("GameController").GetComponent<RecipeUI>();

        cursorstate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (compare(recipe, items) == true)
        {
            Debug.Log("GameOver");
        }

        if (cursorstate == true)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("cursor locked");

    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("cursor Unlocked");

    }

    public void ResetCollection()
    {

        collectedItems.Clear();
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = null;
        }
        

    }
}
