using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float playerSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float grav = 20.0f;
    private Vector3 moveDirection; // = Vector3.zero;
    private CharacterController playerController;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (playerController.isGrounded)
        { 
       
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * playerSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y = moveDirection.y - (grav * Time.deltaTime);
        playerController.Move(moveDirection * Time.deltaTime);

        if (Input.GetKey(KeyCode.O))
        {
            playerController.transform.Rotate(0, -2, 0);
        }
        if (Input.GetKey(KeyCode.P))
        {
            playerController.transform.Rotate(0, 2, 0);
        }
        if (Input.GetKey(KeyCode.K))
        {
            playerController.transform.Rotate(-2, 0, 0);
        }
        if (Input.GetKey(KeyCode.L))
        {
            playerController.transform.Rotate(2, 0, 0);
        }
        if (Input.GetKey(KeyCode.N))
        {
            playerController.transform.Rotate(0, 0, -2);
        }
        if (Input.GetKey(KeyCode.M))
        {
            playerController.transform.Rotate(0, 0, 2);
        }
    }
}