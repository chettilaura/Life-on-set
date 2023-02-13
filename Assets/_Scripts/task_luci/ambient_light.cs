using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ambient_light : MonoBehaviour
{
    Light main_light;
    [SerializeField] PlayerInput playerInput;

    private InputAction aimAction;
    // Start is called before the first frame update
    void Start()
    {
        main_light = GetComponent<Light>();
        main_light.intensity = 1f;
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
        if(QuestManager.questManager.currentQuest != null)
        {
            if(QuestManager.questManager.currentQuest.id == 3)
                main_light.intensity = 0.25f;
        }
            

    }

    private void CancelAim()
    {
        main_light.intensity = 1f;
    }

}
