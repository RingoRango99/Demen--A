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

    // Start is called before the first frame update
    void Start()
    {

        spawnLoc = GameObject.FindGameObjectsWithTag("SpawnPoint");

        List<GameObject> remainingSpawnLoc = new List<GameObject>(spawnLoc);
        List<GameObject> remainingItems = new List<GameObject>(items);

        for (int i = 0; i < spawnLoc.Length; i++)
        {
            spawn = Random.Range(0, remainingSpawnLoc.Count);
            item = Random.Range(0, remainingItems.Count);

            spawnItem = remainingItems[item];
            spawnItem.transform.position = remainingSpawnLoc[spawn].transform.position;
            Spawnitem(spawnItem);
            remainingSpawnLoc.RemoveAt(spawn);
            remainingItems.RemoveAt(item);
            
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawnitem(GameObject sItem)
    {

        Instantiate(sItem);
    }
}
