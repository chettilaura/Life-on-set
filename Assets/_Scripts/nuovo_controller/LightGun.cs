using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGun : MonoBehaviour
{
    private PlayerInput playerInput;
    
    private InputAction lightAction;

    private Transform cameraTransform;
    public LayerMask mask;
    Camera cam;

    [SerializeField] private Material highlightMaterial;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        UpdateCursor();

        if (Cursor.lockState == CursorLockMode.None)
            return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), mousePos - (transform.position + new Vector3(0f, 1f, 0f)), Color.blue);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                Debug.Log(hit.transform.name);
                //hit.transform.GetComponent<Renderer>().material.color = Color.red;
                var selection = hit.transform;
                var selectionRender = selection.GetComponent<Renderer>();
                if (selectionRender != null)
                {
                    selectionRender.material = highlightMaterial;
                }
            }
        }
    }


    // Start is called before the first frame update
    /*private void Awake()
    {
        Debug.Log("ok1");
        playerInput = GetComponent<PlayerInput>();
        lightAction = playerInput.actions["Light"];

        cameraTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        Debug.Log("ok2");
        lightAction.performed += _ => startLighting();
        //startLighting();
    }

    private void OnDisable()
    {
        lightAction.performed -= _ => startLighting();
    }

    private void startLighting()
    {
        
        RaycastHit hit;
        Debug.Log("ok3");
        if (Physics.Raycast(cameraTransform.position + new Vector3(0f, 0f, 2f), cameraTransform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(cameraTransform.position + new Vector3(0f, 0f, 2f), cameraTransform.forward, Color.green);
            //GameObject torch = GameObject.Instantiate(torch_light, cameraTransform.position, Quaternion.identity);
            Debug.Log("ok4");
            Debug.Log(hit.collider.name);
            var selection = hit.transform;
            var selectionRender = selection.GetComponent<Renderer>();
            if(selectionRender != null)
            {
                selectionRender.material = highlightMaterial;
            }

        }
    }*/

    private void UpdateCursor()
    {
        if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(1))
            Cursor.lockState = CursorLockMode.Locked;

        if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }
}
