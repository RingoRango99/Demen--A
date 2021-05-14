using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController ctrl;
    public ItemSpawn itemspawn;
    public UIManager uiManager;

    public float moveSpeed = 7f;
    public float gravity = -9.8f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    bool isActive;

    private void Start()
    {
        itemspawn = GameObject.Find("RandomSpawnHandler").GetComponent<ItemSpawn>();
        uiManager = GameObject.Find("GameController").GetComponent<UIManager>();
        isActive = false;
    }

    void Update()
    {
        // check if player is touching a ground by creating a check sphere underneath
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // if player is grounded and velocity is less than 0
        if (isGrounded && velocity.y < 0)
        {
            // reset velocity to -2
            velocity.y = -2f;
        }

        // store horizontal and vertical axis into respective variables
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // use vector 3 to decalre the direction the player will move
        Vector3 direction = transform.right * x + transform.forward * z;
        // tell character controller to move 
        ctrl.Move(direction * moveSpeed * Time.deltaTime);

        // calculate gravity velocity
        velocity.y += gravity * Time.deltaTime;
        // make player fall depending on gravity velocity
        ctrl.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.T))
        {
            // when button T is pressed reset items
            itemspawn.ResetItems();

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
            // set the pause menu to active 
            // or inactive
            if (isActive == true)
            {
                uiManager.PauseGame();
            }
            else if (isActive == false)
            {
                uiManager.UnPauseGame();
            }
        }
    }
}
