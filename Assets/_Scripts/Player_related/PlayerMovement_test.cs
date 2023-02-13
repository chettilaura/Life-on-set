using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_test : MonoBehaviour

{
    private MouseMovement_test mouseMovement;
    private float speed;
    private float rotationAngle;
    public Animator _animator;
    public AudioSource footSteps;
    // Start is called before the first frame update
    void Start()
    {
        mouseMovement = GetComponent<MouseMovement_test>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("isMovingForward", false);
        if (Input.GetAxis("Vertical")!=0) {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 0.045f;
                rotationAngle = 1.0f;
            }
            else
            {
                speed = 0.02f;
                rotationAngle = 0.5f;
            }
            footSteps.enabled = true;
            _animator.SetBool("isMovingForward", true);
            transform.Rotate(Vector3.up, rotationAngle * mouseMovement.normalizedMousePos.x); }
        else { footSteps.enabled = false; }
        transform.position += transform.forward * speed * Input.GetAxis("Vertical");
    }
}
