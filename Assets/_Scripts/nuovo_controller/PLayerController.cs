using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PLayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private PlayerInput playerInput;
    private bool _jumpKeyPress;

    LightGun light_up;

    private Transform cameraTransform;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
     
    [SerializeField] private float rotationspeed = 5f; 

    private InputAction moveAction;
    private InputAction jumpAction;

    public Animator _animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>(); 
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        //light_up = GetComponent<LightGun>();

        cameraTransform = Camera.main.transform; 

        _animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>(); 
        
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed); 

        /*if (playerSpeed <= 0f)
        {
            _animator.SetBool("isMovingForward", false);
            //changeAnimation(playerSpeed, _jumpKeyPress);
            return;
        }*/


        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);

        //changeAnimation(playerSpeed, _jumpKeyPress);
    }

    /*private void changeAnimation(float _speed, bool _jumpKeyPress)
    {
        if (_speed > 0.3)
        {
            _animator.SetBool("isMovingForward", true);
        }
        else
        {
            _animator.SetBool("isMovingForward", false);
        }

        if (_jumpKeyPress)
        {
            _animator.SetBool("jump", true);
        }
    }*/
}