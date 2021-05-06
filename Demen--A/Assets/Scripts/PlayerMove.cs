using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController ctrl;

    public float moveSpeed = 12f;

    void Update()
    {

        // store horizontal and vertical axis into respective variables
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // use vector 3 to decalre the direction the player will move
        Vector3 direction = transform.right * x + transform.forward * z;
        // tell character controller to move 
        ctrl.Move(direction * moveSpeed * Time.deltaTime);
    }
}
