using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    private float _rotationX = 0f;
    private float _rotationY = 0f;

    private float _mouseSensitivity = 7f;

    [SerializeField] private Transform _playerBody;
    [SerializeField] private Vector3 _offset;


    void Update()
    {
        
        //follow player
        Vector3 desiredPosition = _playerBody.position + _offset;
        transform.position = desiredPosition;


        //rotation mouse
       _rotationX += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _rotationY += Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationY = Mathf.Clamp(_rotationY, -30f, 30f);

        transform.rotation = Quaternion.Euler(-_rotationY, _rotationX, 0f);
        
    }
}
