using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public float DistanceFromPlayer = 5;
    public float height = 2;
    private float _rotationY = 0f;
    private float _rotationX = 0f;  

    void LateUpdate()
    {
        transform.position = _player.transform.position - _player.transform.forward * DistanceFromPlayer;
        transform.LookAt(_player.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);

        //mouse rotation 
        _rotationX += Input.GetAxis("Mouse X");
        _rotationY += Input.GetAxis("Mouse Y");

        _rotationY = Mathf.Clamp(_rotationY, -30f, 30f);
        _rotationX = Mathf.Clamp(_rotationX, -30f, 30f);

        transform.Rotate(-_rotationY, _rotationX, 0f);
    }
}


