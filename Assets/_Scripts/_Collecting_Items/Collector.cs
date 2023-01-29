using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//script da assegnare al player 

//determina se si è colliso con un oggetto che sia collectible, se si lo prende
public class Collector : MonoBehaviour
{
    void OnTriggerEnter (Collider collision)
    {
        InterfaceCollectible collectible = collision.GetComponent<InterfaceCollectible>();
        if (collectible != null)
        {
            collectible.Collect();
        }
    }
}
