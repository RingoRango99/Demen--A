using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    // Variables
    public Transform targetC;
    public GameObject itemDestination;
    public GameObject dropLocation;
    public GameObject pickupItem;
    public GameObject invItem;

    public TMP_Text dropInfo;

    public float maxReach = 3f;

    public UIManager uiManager;



    // Start is called before the first frame update
    void Start()
    {
        // pre set world game objects necessary for locations of items
        itemDestination = GameObject.Find("ItemHolder");
        dropLocation = GameObject.Find("DropLocation");
        targetC = GameObject.Find("Main Camera").transform;

        dropInfo.enabled = false;

        uiManager = GameObject.Find("GameController").GetComponent<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {

        // run the looking raycast
        LookCast();

        if (Input.GetKeyDown(KeyCode.Mouse0) && invItem == null && pickupItem != null && pickupItem.tag == "Item")
        {
            // if lmb is pressed and player is holding nothing and the looked at item has a tag of "Item"
            // run pick up script

                PickupItem();
            
            

        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            // if rmb is pressed drop any item held
            DropItem();
        }

        if (pickupItem != null && invItem == null && pickupItem.tag == "Item")
        {
            // if looked at item isnt null and inventory item is null
            // and looked at item tag is item activate mouse info
            uiManager.SetMouseInfo(pickupItem);
            uiManager.crosshair.color = Color.green;
        }
        else
        {
            // else disable mouse info
            uiManager.DisableMouseInfo();
            uiManager.crosshair.color = Color.red;
        }
    }




    void LookCast()
    {
        // declare raycast variable to store hit information
        RaycastHit hit;

        // for debug purposes declare forward direction by max reach distance
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxReach;
        // draw a visible ray to see how far the reach is for the raycast
        Debug.DrawRay(transform.position, fwd, Color.blue);

        // if from the origin point which is camera and transform forward there is a hit within reach
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxReach))
        {
            // put the raycast hit item into variable
            pickupItem = hit.transform.gameObject;

            

        }
        else
        {
            // if nothing is hit run reset item script
            ResetItem();
            
        }

    }

    void PickupItem()
    {
        // store looked at item into inventory item 
        invItem = pickupItem;

        // change the objects kinematics to stop it from moving whilst being held
        // set the position and parent of the object to the item destination object so it appears in bottom right
        // set object angle to face the player to make item identification easier
        invItem.GetComponent<Rigidbody>().isKinematic = true;
        invItem.transform.position = itemDestination.transform.position;
        invItem.transform.parent = itemDestination.transform;
        invItem.transform.LookAt(targetC);

        dropInfo.enabled = true;
        

    }

    void DropItem()
    {
        //set object drop location to where the object will be dropped
        invItem.transform.position = dropLocation.transform.position;
        //un-parent the object from the hold location
        invItem.transform.parent = null;
        //allow the object to have physichs
        invItem.GetComponent<Rigidbody>().isKinematic = false;
        // make the object face the player when dropped
        invItem.transform.LookAt(targetC);
        // set held item variable to nothing
        invItem = null;

        dropInfo.enabled = false;
    }

    void ResetItem()
    {
        // set looking at item to nothing
        pickupItem = null;
        

    }
}
