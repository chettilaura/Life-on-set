using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangesScene : MonoBehaviour
{
   


    void OnTriggerEnter(Collider collider)
    {

        //layer 7 = esterno 
        if (collider.gameObject.layer == 7)
        {

            Debug.Log("Player has entered the trigger");
            //chiama scena successiva
        
             Scene_Loader.Load(Scene_Loader.Scene.Esterno);

        }

        //layer 8 = studio1
        if (collider.gameObject.layer == 8)
        {

            Debug.Log("Player has entered the trigger");
            //chiama scena successiva
        
             Scene_Loader.Load(Scene_Loader.Scene.Studio1);

        }

      //layer 9 = studio2
        if (collider.gameObject.layer == 9)
        {

            Debug.Log("Player has entered the trigger");
            //chiama scena successiva
        
             Scene_Loader.Load(Scene_Loader.Scene.Studio2);

        }   

         //layer 10 = camper
        if (collider.gameObject.layer == 10)
        {

            Debug.Log("Player has entered the trigger");
            //chiama scena successiva
        
             Scene_Loader.Load(Scene_Loader.Scene.Camper);

        }

         //layer 11 = camion
        if (collider.gameObject.layer == 11)
        {

            Debug.Log("Player has entered the trigger");
            //chiama scena successiva
        
             Scene_Loader.Load(Scene_Loader.Scene.Camion);

        }

    }

}
