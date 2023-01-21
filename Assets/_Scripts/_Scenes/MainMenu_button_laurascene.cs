using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//ho dovuto commentare tutto perchè puntare bottone con il mouse non è fattibile 

public class MainMenu_button : MonoBehaviour  //, IPointerDownHandler, IPointerUpHandler
{

    //bool isPressed = false; 

    void Update(){

        //premo Esc per andare al menu principale
         if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Scene_Loader.Load(Scene_Loader.Scene.MainMenu);
        }

        /*
        if(isPressed){
            Loader.Load(Loader.Scene.MainMenu);
        }
        */
    }


  /*
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      isPressed = false;
    }
    */
}

   

