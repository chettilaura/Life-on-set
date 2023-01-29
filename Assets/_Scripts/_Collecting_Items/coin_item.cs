using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class coin_item : MonoBehaviour, InterfaceCollectible
{

    public static event Action onCoinCollected; //event is a delegate that can be subscribed to by other classes, creo event diverso per ogni oggetto 
    //il fatto che sia di tipo Action means that sarà chiamato senza parametri 

    public void Collect()
    {
        Debug.Log("Collecting coin");
        Destroy(gameObject);
        onCoinCollected?.Invoke();
    }


    /*
    private void onEnable()
    {
       coin_item.onCoinCollected += addToInventory; //ci si iscrive all'evento con +=
    }

    private void onDisable(){
        coin_item.onCoinCollected -= addToInventory; //ci si disiscrive all'evento con -=
    }
    public void addToInventory(){
        Debug.Log("Adding coin to inventory");
    }
    */
    
}
