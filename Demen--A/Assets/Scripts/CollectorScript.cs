using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour
{
    public GameManager gamemanager;

    public GameObject collected;
    public Transform collLocation;

    void Start()
    {
        gamemanager = GameObject.Find("GameController").GetComponent<GameManager>();
        collLocation = GameObject.Find("Discard").transform;
    }
    // when collision is detected and the collider game object tag is Item
    // Add item to a list of "UsedItems" array and destroy item
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            collected = collision.gameObject;

            gamemanager.Collect(collected);

            collected.gameObject.transform.position = collLocation.transform.position;
            collected.transform.parent = collLocation.transform;
        }
    }
}
