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
                    if (selectionRender.material!=highlightMaterial){
                            selectionRender.material = highlightMaterial;
                            
                            if(contatore_oggetti_illuminati==2){
                                //la task è finita -> chiedere ad agnese come si segna che una quest è stata completata 
                                Debug.Log("task finita");
                            }
                    }else{
                        Debug.Log("oggetto già illuminato");
                    }
                    selectionRender.material = highlightMaterial;
                }
                }
            }
        }
    }




    private void UpdateCursor()
    {
        if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(1))
            Cursor.lockState = CursorLockMode.Locked;

        if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }
}
