using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//script da assegnare al player 

//determina se si è colliso con un oggetto che sia collectible, se si lo prende
public class Collector : MonoBehaviour
{
    private void OnTriggerEnter (Collider collision) //check se c'è collectible object
    {
        InterfaceCollectible collectible = collision.GetComponent<InterfaceCollectible>();
        if (collectible != null)
        {
            collectible.Collect(); //we tell it to collect itself
        }
    }
}
