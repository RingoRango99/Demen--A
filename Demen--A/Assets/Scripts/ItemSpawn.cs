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

    // Start is called before the first frame update
    void Start()
    {

        gamemanager = GameObject.Find("GameController").GetComponent<GameManager>();

        // searches through game world to find any objects with tag spawn point and stores them in array
        spawnLoc = GameObject.FindGameObjectsWithTag("SpawnPoint");

        // makes new list containing all game objects within the arrays
        List<GameObject> remainingSpawnLoc = new List<GameObject>(spawnLoc);
        List<GameObject> remainingItems = new List<GameObject>(items);

        // creates a loop that goes through the length of the spawn point array
        for (int i = 0; i < spawnLoc.Length; i++)
        {
            // chooses random number for spawn location and item
            spawn = Random.Range(0, remainingSpawnLoc.Count);
            item = Random.Range(0, remainingItems.Count);

            // selects the item to be spawned from currently chosen remainingitem list
            spawnItem = remainingItems[item];
            // sets item spawn to the position of the currently chosen remainingspawnloc list
            spawnItem.transform.position = remainingSpawnLoc[spawn].transform.position;
            // spawns chosen item
            Spawnitem(spawnItem);
            // removes item and spawn point from list to avoid duplicates
            remainingSpawnLoc.RemoveAt(spawn);
            remainingItems.RemoveAt(item);
            
        }
       
    }

    void Spawnitem(GameObject sItem)
    {
        // spawn item without the name Clone

        GameObject spawnedItem = (GameObject)Instantiate(sItem);

        spawnedItem.name = sItem.name;

        // put chosen item into recipe list
        gamemanager.Updatelist(spawnedItem);



    }
}
