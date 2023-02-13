using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class spotlight_on : MonoBehaviour
{
    Light spotlight;
    [SerializeField] PlayerInput playerInput;

    private InputAction aimAction;
    // Start is called before the first frame update
    void Start()
    {
        spotlight = GetComponent<Light>();
        spotlight.enabled = false;
    }

    private void Awake()
    {
        aimAction = playerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        spotlight.enabled = true;
        spotlight.intensity = 3f;

    }

    private void CancelAim()
    {
        spotlight.enabled = false;
    }

}
