using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float playerSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float grav = 20.0f;
    private Joystick joystick;
    private Button leftButton;
    private Button rightButton;
    private Button jumpButton;
    private Vector3 moveDirection; 
    private CharacterController playerController;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        joystick = FindObjectOfType<Joystick>();
        jumpButton = FindObjectsOfType<Button>()[0];
        rightButton = FindObjectsOfType<Button>()[1];
        leftButton = FindObjectsOfType<Button>()[2]; 
    }

    void Update()
    {
        if (playerController.isGrounded)
        {     
            moveDirection = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical); //get input from joysticks
            moveDirection = transform.TransformDirection(moveDirection); //compare it to where character is facing 
            moveDirection = moveDirection * playerSpeed; 

            if (jumpButton.pressed)
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y = moveDirection.y - (grav * Time.deltaTime);
        playerController.Move(moveDirection * Time.deltaTime);
          
        //Turn with O and P
        if (rightButton.pressed)
        {
            playerController.transform.Rotate(0, -2, 0);
        }
        if (leftButton.pressed)
        {
            playerController.transform.Rotate(0, 2, 0);
        }
       
    }
}