using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangesScene : MonoBehaviour
{
   


    void OnTriggerEnter(Collider collider)
    {

        //layer 7 = esterno 
        if (collider.gameObject.layer == 7 && QuestManager.questManager.currentQuest.id == -1) //currentQuest.id diventa -1 quando non c'� nessuna task attiva
        {

            Debug.Log("Player has entered the trigger");
            Debug.Log("non puoi ancora andare all'esterno fino a che non hai finito di aiutare chi ti ha chiesto aiuto");
            //chiama scena successiva
        
             Scene_Loader.Load(Scene_Loader.Scene.Esterno);

        }

        //layer 8 = studio1
        if (collider.gameObject.layer == 8 && QuestManager.questManager.currentQuest.id == -1)
        {

            Debug.Log("Player has entered the trigger");
            //chiama scena successiva
             Debug.Log("non puoi ancora entrare nello studio1 fino a che non hai finito di aiutare chi ti ha chiesto aiuto");
        
             Scene_Loader.Load(Scene_Loader.Scene.Studio1);

        }

      //layer 9 = studio2
        if (collider.gameObject.layer == 9 && QuestManager.questManager.CheckEverythingDone())
        {

            Debug.Log("Player has entered the trigger");
            //chiama scena successiva
             Debug.Log("non puoi ancora entrare nello studio2 fino a che non hai finito di aiutare tutti");
        
             Scene_Loader.Load(Scene_Loader.Scene.Studio2);

        }   

         //layer 10 = camper
        if (collider.gameObject.layer == 10 && QuestManager.questManager.currentQuest.id == -1)
        {

            Debug.Log("Player has entered the trigger");
            //chiama scena successiva
            Debug.Log("non puoi ancora entrare nel camper fino a che non hai finito di aiutare chi ti ha chiesto aiuto");
        
             Scene_Loader.Load(Scene_Loader.Scene.Camper);

        }

        //camion non è available  
         //layer 11 = camion 
       /* if (collider.gameObject.layer == 11 && QuestManager.questManager.currentQuest.id == -1)
        {

            Debug.Log("Player has entered the trigger");
            //chiama scena successiva
        
             Scene_Loader.Load(Scene_Loader.Scene.Camion);

        }
        */

    }

}
