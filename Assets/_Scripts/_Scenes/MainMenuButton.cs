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

            //va salvata la scena corrente prima di passare al main menu perchè poi ci deve ritornare
            //Scene_Loader.Load(Scene_Loader.Scene.MainMenu);
            //MainMenu_prefab.setActive(true);
        }


    } 
}

   

