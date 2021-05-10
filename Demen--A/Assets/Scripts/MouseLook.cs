using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float sensitivity = 100f;
    public Transform pBody;
    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {

        // Lock cursor when game starts
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        // gets mouse inputs and adds the mouse sensitivity to it
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // transform get axis data into rotational data used to rotate the player using the mouse
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // calculate the rotation to be carried out and apply to the transform
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // rotate player body the the amount moved by mouse X
        pBody.Rotate(Vector3.up * mouseX);


    }
}
