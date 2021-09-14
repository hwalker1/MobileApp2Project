using UnityEngine;
using System.Collections;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public GameObject gun1, gun2;
    public float playerSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float grav = 20.0f;
    public float gunDamage = 1;
    public float fireRate = 15f;
    public float timeToNextFire = 0f;
    private Joystick moveJoyStick;
    private Joystick lookJoyStick;
    private Button jumpButton;
    private Button shootButton;
	private Button leftButton;
	private Button rightButton;
	private Button upButton;
	private Button downButton;
	public float lookSpeed = 1;
	public Transform camera;
    public float currentHealth = 99;
    public float maxhealth = 100;

    public Slider healthFill;
    //public Transform healthBar;
    private float healthRatio;

    private Vector3 moveDirection;
    private Vector3 lookDirection;
    private float cameraXDir;
    private float cameraYDir;
    private float cameraSpeed = 1;
    private CharacterController playerController;
    private float xAxisClamp = 0.0f;


    void Start()
    {
        playerController = GetComponent<CharacterController>();
        moveJoyStick = FindObjectsOfType<Joystick>()[1];
        lookJoyStick = FindObjectsOfType<Joystick>()[0];

        //jumpButton = FindObjectsOfType<Button>()[0];  
		//swapButton = FindObjectsOfType<Button>()[1];  
		//upButton = FindObjectsOfType<Button>()[1]; 
		//downButton = FindObjectsOfType<Button>()[0]; 
		//leftButton = FindObjectsOfType<Button>()[3]; 
		//rightButton = FindObjectsOfType<Button>()[2]; 
        shootButton = FindObjectsOfType<Button>()[0];    
    }

    void Update()
    {
        if (playerController.isGrounded)
        {     
            moveDirection = new Vector3(moveJoyStick.Horizontal, 0.0f, moveJoyStick.Vertical); //get input from joysticks
            moveDirection = transform.TransformDirection(moveDirection); //compare it to where character is facing 
            moveDirection = moveDirection * playerSpeed; 

           /* if (jumpButton.pressed)
            {
                moveDirection.y = jumpSpeed;
            }*/
        }
        moveDirection.y = moveDirection.y - (grav * Time.deltaTime);
        playerController.Move(moveDirection * Time.deltaTime);

        RotateCamera();


		//Debug.Log("BEFORE TIME " + Time.time + " >= " + timeToNextFire);
		if (shootButton.pressed && Time.time >= timeToNextFire)
         {
			Debug.Log("AFTER TIME " + Time.time + " >= " + timeToNextFire);
         	 timeToNextFire =  Time.time + 1f/ fireRate;
       		 
             RaycastHit shot;
             if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
             {
     				Debug.Log(shot.transform.gameObject);
                 	shot.transform.SendMessage("get_shot", gunDamage);
             }
         }


		/*
        cameraXDir += lookJoyStick.Horizontal; // * cameraSpeed;
        cameraYDir += lookJoyStick.Vertical;

		if(lookJoyStick.Horizontal > 0.1){
			transform.Rotate(Vector3.up * lookSpeed);
		}
		else if(lookJoyStick.Horizontal < -0.1){
			transform.Rotate(Vector3.down * lookSpeed);
		}

		if(lookJoyStick.Vertical > 0.1){
			transform.Rotate(Vector3.left * lookSpeed);
		}
		else if(lookJoyStick.Vertical < -0.1){
			transform.Rotate(Vector3.right * lookSpeed);
		}

		if(swapButton.pressed){
			gun1.enabled = !gun1.enabled;
			gun2.enabled = !gun2.enabled;
		}
		*/

        /*
        if(upButton.pressed){
		
        		transform.Rotate(Vector3.left * lookSpeed);
        }
		if(downButton.pressed){
			
        		transform.Rotate(Vector3.right * lookSpeed);
        }
		if(leftButton.pressed){
        	transform.Rotate(Vector3.up * lookSpeed);
        }
		if(rightButton.pressed){
        	transform.Rotate(Vector3.down * lookSpeed);
        }
        */ 


   		/*
        lookDirection = new Vector3(cameraXDir,  0.0f, cameraYDir); //get input from joysticks
        transform.LookAt(lookDirection);
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        */


    }

    void RotateCamera(){
		float rotationX = lookJoyStick.Horizontal * cameraSpeed;
		float rotationY = lookJoyStick.Vertical * cameraSpeed;
		Vector3 bodyRotation = transform.rotation.eulerAngles;
		Vector3 cameraRotation = camera.rotation.eulerAngles;

		cameraRotation.y += rotationX;
		cameraRotation.x -= rotationY;
		cameraRotation.z = 0;

		bodyRotation.y += rotationX;
		bodyRotation.x -= rotationY;
		bodyRotation.z = 0;

		xAxisClamp -= rotationY;

		if(xAxisClamp > 90){
			xAxisClamp = 90;
			bodyRotation.x = 90;
		}
		else if(xAxisClamp < -90){
			xAxisClamp = -90;
			bodyRotation.x = 270;
		}

		transform.rotation = Quaternion.Euler(bodyRotation);
		camera.rotation = Quaternion.Euler(cameraRotation); 
    }

    public void TakeDamage(int dam)
    {
        currentHealth -= dam;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxhealth);
        healthRatio = currentHealth / maxhealth;
        Debug.Log(healthRatio);
        healthFill.value = healthRatio;
    }
}