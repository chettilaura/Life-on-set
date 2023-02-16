using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour
{
   //chiusura applicazione: non funziona in play mode da editor unity

    public void ExitGame(){
        Application.Quit();
    }


  
 
}

   
