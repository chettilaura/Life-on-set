using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class changeCamera : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] int priorityOffset = 10;

    [SerializeField] Canvas AimCanvas;

    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;
    
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
        AimCanvas.enabled = false;
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
        virtualCamera.Priority += priorityOffset;
        AimCanvas.enabled = true;
    }

    private void CancelAim()
    {
        virtualCamera.Priority -= priorityOffset;
        AimCanvas.enabled = false;
    }
}
