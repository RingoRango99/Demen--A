using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    public GameObject[] spawnLoc;
    public int spawn;

    public GameObject[] items;
    public int item;

    public GameObject spawnItem;

    public GameManager gamemanager;

    public List<GameObject> remainingSpawnLoc;
    public List<GameObject> remainingSpawnLoc2;
    public List<GameObject> remainingItems;
    public List<GameObject> remainingItems2;

    public List<GameObject> respawnlocList;
    public List<GameObject> respawnitemList;

    // Start is called before the first frame update
    void Start()
    {

        gamemanager = GameObject.Find("GameController").GetComponent<GameManager>();

        // searches through game world to find any objects with tag spawn point and stores them in array
        spawnLoc = GameObject.FindGameObjectsWithTag("SpawnPoint");

        // makes new list containing all game objects within the arrays
        remainingSpawnLoc = new List<GameObject>(spawnLoc);
        remainingItems = new List<GameObject>(items);

        remainingSpawnLoc2 = new List<GameObject>(spawnLoc);
        // spawn initial set of items
        ItemSpawnLoc(remainingSpawnLoc, remainingItems);
       
    }

    public void ItemSpawnLoc(List<GameObject> spawnList, List<GameObject> itemList)
    {
        
        // creates a loop that goes through the length of the spawn point array
        for (int i = 0; i < spawnLoc.Length; i++)
        {
            // chooses random number for spawn location and item
            spawn = Random.Range(0, spawnList.Count);
            item = Random.Range(0, itemList.Count);

            // selects the item to be spawned from currently chosen remainingitem list
            spawnItem = itemList[item];
            // sets item spawn to the position of the currently chosen remainingspawnloc list
            spawnItem.transform.position = spawnList[spawn].transform.position;
            // spawns chosen item
            Spawnitem(spawnItem);
            // removes item and spawn point from list to avoid duplicates
            spawnList.RemoveAt(spawn);
            itemList.RemoveAt(item);

        }


    }

    public void ItemReSpawnLoc(List<GameObject> spawnList, List<GameObject> itemList)
    {
        remainingItems2.Clear();
        // creates a loop that goes through the length of the spawn point array
        for (int i = 0; i < remainingSpawnLoc2.Count; i++)
        {
            // chooses random number for spawn location and item
            spawn = Random.Range(0, spawnList.Count);
            item = Random.Range(0, itemList.Count);

            // selects the item to be spawned from currently chosen remainingitem list
            spawnItem = itemList[item];
            // sets item spawn to the position of the currently chosen remainingspawnloc list
            spawnItem.transform.position = spawnList[spawn].transform.position;
            remainingItems2.Add(spawnItem);
            // remove just used item and spawn point from list
            // so it doesn't get used again
            spawnList.RemoveAt(spawn);
            itemList.RemoveAt(item);
        }


    }

    public void Spawnitem(GameObject sItem)
    {
        // spawn item without the name Clone

        GameObject spawnedItem = (GameObject)Instantiate(sItem);
        // make sure instantiated item name isnt "Name"(Clone) 
        // but instead is "Name"
        spawnedItem.name = sItem.name;
        // add spawned item to remaining items list 2
        remainingItems2.Add(spawnedItem);

        // put chosen item into recipe list
        gamemanager.Updatelist(spawnedItem);



    }

    public void ResetItems()
    {
        // populates lists with objects which will be used for item respawns
        remainingItems2.Clear();
        remainingSpawnLoc2 = new List<GameObject>(spawnLoc);
        respawnlocList = new List<GameObject>(spawnLoc);
        respawnitemList = new List<GameObject>(gamemanager.recipe);

        gamemanager.ResetCollection();
        

        Debug.Log("Resetting items");
        // initiate item respawn
        ItemReSpawnLoc(respawnlocList, respawnitemList);



    }

    public void ConfuseActivate()
    {
        // populates lists with objects which will be used for item respawns
        respawnlocList = new List<GameObject>(remainingSpawnLoc2);
        respawnitemList = new List<GameObject>(remainingItems2);

        Debug.Log("Resetting items");
        // initiate item respawn
        ItemReSpawnLoc(respawnlocList, respawnitemList);

    }
}
