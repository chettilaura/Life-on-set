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

    private CharacterController _characterController;

    public float Speed = 5.0f;

    public float RotationSpeed = 240.0f;

    private float Gravity = 20.0f;

    private Vector3 _moveDir = Vector3.zero;


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
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        /*groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }*/


        // prova


        // Get Input for axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

        if (move.magnitude > 1f) move.Normalize();

        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

        if (_characterController.isGrounded)
        {
            _animator.SetBool("run", move.magnitude > 0);

            _moveDir = transform.forward * move.magnitude;

            _moveDir *= Speed;

        }

        _moveDir.y -= Gravity * Time.deltaTime;

        _characterController.Move(_moveDir * Time.deltaTime);

    }
}

        /*Vector2 input = moveAction.ReadValue<Vector2>(); 
        
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed); */

        /*if (playerSpeed <= 0f)
        {
            _animator.SetBool("isMovingForward", false);
            //changeAnimation(playerSpeed, _jumpKeyPress);
            return;
        }*/


        // Changes the height position of the player..
        /*if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);

        //changeAnimation(playerSpeed, _jumpKeyPress);
        }*/

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
 