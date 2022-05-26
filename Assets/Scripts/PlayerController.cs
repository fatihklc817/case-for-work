using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController),typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
     
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed= 5f;

    Shooting shooting;                                                   //to acces shooting scripts variables
    bool cursorOn = false;
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;


    //input actions

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction showCursor;


    





    

    private void Awake()
    {
        
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        shooting = GetComponent<Shooting>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        showCursor = playerInput.actions["ShowCursor"];

        
        
         Cursor.lockState = CursorLockMode.Locked;


                                                                    //if mouse input starts ,change isFiring .
        shootAction.started += _ => shooting.isFiring= true;
        shootAction.canceled += _ => shooting.isFiring= false;
    }


  


    


    void Update()
    {
        CursorLocker();
        

        // GROUND CHECK

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //GET INPUT FROM INPUT SYSTEM AND MOVE CHARACTER

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        

        // JUMP and add gravity

        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    
    
    
        // rotate body to where camera looking

        float targetAngle = cameraTransform.eulerAngles.y;  
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);
    }



    //change the cursor lock mode by input
    void CursorLocker()
    {
        
            
        if(showCursor.triggered && !cursorOn)
        {
            Cursor.lockState = CursorLockMode.None;
            cursorOn = true;
        }

        else if(showCursor.triggered && cursorOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorOn= false;
        }



    }

}
