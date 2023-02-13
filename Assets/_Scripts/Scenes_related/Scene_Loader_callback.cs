using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Loader_callback : MonoBehaviour
{
private bool isFirstUpdate = true;

//chiama la funzione LoaderCallback() dopo il primo Update
//così si assicura che faccia almeno un Update prima di caricare la scena: sfruttando Loading 
private void Update ()
{
    if (isFirstUpdate)
    {
       
        Scene_Loader.LoaderCallback();
         isFirstUpdate = false;
    }
}


}
