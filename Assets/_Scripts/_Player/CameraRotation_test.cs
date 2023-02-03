using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation_test : MonoBehaviour
{
    private MouseMovement_test mouseMovement;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        mouseMovement = GetComponent<MouseMovement_test>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(7*mouseMovement.normalizedMousePos.y, player.transform.rotation.eulerAngles.y + 10*mouseMovement.normalizedMousePos.x, 0);
            //transform.Rotate(Vector3.up, mouseMovement.normalizedMousePos.x);
    }
}
