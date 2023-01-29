using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    private void OnEnable()
    {
        //iscrivo inventario all'evento coin con += -> quando coin viene raccolto, viene chiamato il metodo addToInventory
       coin_item.onCoinCollected += addToInventory; 
    }

    private void OnDisable(){
        //disiscrivo inventario dall'evento coin con -= 
        coin_item.onCoinCollected -= addToInventory; //ci si disiscrive all'evento con -=
    }

    //metodo che viene chiamato quando coin viene raccolto coin_item 
    public void addToInventory(){
        Debug.Log("Adding coin to inventory");
    }

}
