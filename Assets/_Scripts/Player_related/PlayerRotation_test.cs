using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation_test : MonoBehaviour

{
    private MouseMovement_test mouseMovement;
    private float speed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        mouseMovement = GetComponent<MouseMovement_test>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical")!=0) {
            transform.Rotate(Vector3.up, 2 * mouseMovement.normalizedMousePos.x); }
        transform.position += transform.forward * speed * Input.GetAxis("Vertical");
    }
}
