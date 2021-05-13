using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour
{
    public GameManager gamemanager;
    public ItemSpawn itemSpawnManager;

    public GameObject collected;
    public Transform collLocation;

    void Start()
    {
        gamemanager = GameObject.Find("GameController").GetComponent<GameManager>();
        itemSpawnManager = GameObject.Find("RandomSpawnHandler").GetComponent<ItemSpawn>();
        collLocation = GameObject.Find("Discard").transform;
    }
    // when collision is detected and the collider game object tag is Item
    // Add item to a list of "UsedItems" array and destroy item
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            collected = collision.gameObject;
            // tell game manager script to store collected item
            gamemanager.Collect(collected);
            // remove collected item from remaining item list
            itemSpawnManager.remainingItems2.RemoveAll(x=>x == collected.gameObject);
            // change item position to be off screen so it can be re-used later
            collected.gameObject.transform.position = collLocation.transform.position;
            collected.transform.parent = collLocation.transform;

        }
    }
}
