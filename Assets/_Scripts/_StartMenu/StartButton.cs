using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;
    [SerializeField] private Animator _ciackOpen;
    [SerializeField] private GameObject _container;

    void Start()
    {   
        _container.GetComponent<Animator>();
    }

    void Update(){
        if(isPressed){
            _ciackOpen.Play("startGame");
            StartCoroutine(TimeDelay());
            //Debug.Log(_ciackOpen.GetBool("startGame"));
        }
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(1);
        Scene_Loader.Load(Scene_Loader.Scene.Esterno);
    }

  
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      isPressed = false;
    }
}

   
