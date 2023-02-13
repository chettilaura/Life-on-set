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

    //prova
    private Vector2 input;
    private float direction = 0f;
    public bool useCharacterForward = false;
    private Vector3 targetDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        //light_up = GetComponent<LightGun>();

        cameraTransform = Camera.main.transform;

        _animator = GetComponent<Animator>();

        //prova
        
    }

    void Update()
    {

        Vector2 input = moveAction.ReadValue<Vector2>();

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (input.y < 0f && useCharacterForward)
            direction = input.y;
        else
            direction = 0f;



            /*Vector3 move = new Vector3(input.x, 0, input.y);
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);*/

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
    public virtual void UpdateTargetDirection()
    {
        if (!useCharacterForward)
        {
            var forward = cameraTransform.TransformDirection(Vector3.forward);
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = cameraTransform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            targetDirection = input.x * right + input.y * forward;
        }
        else
        {
            
            var forward = transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = transform.TransformDirection(Vector3.right);
            targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
        }
    }
}

        