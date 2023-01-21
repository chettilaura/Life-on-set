using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour  //, IPointerDownHandler, IPointerUpHandler
{
    void Update(){

        //premo Esc per andare al menu principale
         if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Scene_Loader.Load(Scene_Loader.Scene.MainMenu);
        }

    } 
}

   

