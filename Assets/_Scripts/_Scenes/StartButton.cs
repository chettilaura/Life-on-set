using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false; 

    void Update(){
        if(isPressed){
            Scene_Loader.Load(Scene_Loader.Scene.Esterno);
        }
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

   
