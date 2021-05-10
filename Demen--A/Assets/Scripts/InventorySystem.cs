using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject itemDestination;
    public GameObject dropLocation;
    public GameObject pickupItem;
    public GameObject invItem;


    public float maxReach = 2f;



    // Start is called before the first frame update
    void Start()
    {
        itemDestination = GameObject.Find("ItemHolder");
        dropLocation = GameObject.Find("DropLocation");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && invItem == null && pickupItem.tag == "Item")
        {

            PickupItem();

        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            DropItem();
        }
    }


    void FixedUpdate()
    {
        LookCast();
    }



    void LookCast()
    {

        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxReach;
        Debug.DrawRay(transform.position, fwd, Color.blue);

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxReach))
        {

            pickupItem = hit.transform.gameObject;

            Debug.Log("Looking At - ", pickupItem);

        }
        else
        {

            ResetItem();

        }

    }

    void PickupItem()
    {

        invItem = pickupItem;

        invItem.GetComponent<Rigidbody>().isKinematic = true;
        invItem.transform.position = itemDestination.transform.position;
        invItem.transform.parent = itemDestination.transform;

    }

    void DropItem()
    {

        invItem.transform.position = dropLocation.transform.position;
        invItem.transform.parent = null;
        invItem.GetComponent<Rigidbody>().isKinematic = false;
        invItem = null;

    }

    void ResetItem()
    {

        pickupItem = null;

    }
}
