using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMov : MonoBehaviour
{
    //mine
    public Transform GroundCheckTransform = null;
    public LayerMask playerMask;
    private bool _jumpKeyPress;
    private float _horizontalInput;
    private float _verticalInput;
    private Rigidbody _rigidBodyComponent;
    public Animator _animator;


    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private Transform _cameraT;
    

  
   
  
    void Start()
    {
        _rigidBodyComponent = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }


 

    void Update()
    {
        //input jump
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            _jumpKeyPress = true;
        }

        //input frecce
        _horizontalInput = Input.GetAxis("Horizontal"); //sx-dx
        _verticalInput = Input.GetAxis("Vertical"); //avanti-dietro
        

        //calcolo vettori in base a input 
        _inputVector = new Vector3(_horizontalInput, 0, _verticalInput);
        _inputSpeed = Mathf.Clamp(_inputVector.magnitude, 0f, 1f);

        //se non c'è input
        if (_inputSpeed <= 0f)
        {
            changeAnimation(_inputSpeed, _jumpKeyPress);
            return;
        }


        //Compute direction According to Camera Orientation
        //spostamenti riferiti alla visuale della camera e non al forward del personaggio -> non possono mai camminare all'indietro
        _targetDirection = _cameraT.TransformDirection(_inputVector).normalized;
        _targetDirection.y = 0f;

       
        //animazioni
        changeAnimation(_inputSpeed, _jumpKeyPress);

    }




    private void FixedUpdate()
    {
        //spostamento
        Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDirection, _rotationSpeed * Time.fixedDeltaTime, 0f);
        _rigidBodyComponent.MoveRotation(Quaternion.LookRotation(newDir));
        _rigidBodyComponent.MovePosition(_rigidBodyComponent.position + transform.forward * _inputSpeed * _speed * Time.fixedDeltaTime);



        //controllo se atterrato prima di saltare di nuovo & salto eventuale
        if (Physics.OverlapSphere(GroundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        if (_jumpKeyPress)
        {
            _rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            changeAnimation(_inputSpeed, _jumpKeyPress);
            _jumpKeyPress = false;
            _animator.SetBool("jump", false);
        }
    }





    private void changeAnimation(float _speed, bool _jumpKeyPress) {
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
        }


}
