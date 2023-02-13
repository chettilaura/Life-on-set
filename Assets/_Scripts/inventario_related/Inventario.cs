using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventario : MonoBehaviour
{

    public static event Action<List<Inventario_item>> OnInventarioChange;  //manager inventario sarà iscritto a questo evento
   
    public List <Inventario_item> inventario = new List<Inventario_item>(); //lista degli item nell'inventario
    private Dictionary <Item_data, Inventario_item> dizionario_item= new Dictionary<Item_data, Inventario_item>();

private void OnEnable()
    {
        //iscrivo inventario all'evento coin con += -> quando coin viene raccolto, viene chiamato il metodo add -> aggiungo listener
       
        //coin_item.onItemDataCollected+= Add; 
        tutorial_task.onItemDataCollected+=Add;
        illuminazione_task.onItemDataCollected+=Add;
        suono_task.onItemDataCollected+=Add;
        comparse_task.onItemDataCollected+=Add;
        costumi_task.onItemDataCollected+=Add;
        continuita_task.onItemDataCollected+=Add;
    }

    private void OnDisable(){
        //disiscrivo inventario dall'evento coin con -= 
        
        //coin_item.onItemDataCollected -= Add; 
        tutorial_task.onItemDataCollected -= Add;
        illuminazione_task.onItemDataCollected -= Add;
        suono_task.onItemDataCollected -= Add;
        comparse_task.onItemDataCollected -= Add;
        costumi_task.onItemDataCollected -= Add;
        continuita_task.onItemDataCollected -= Add;

    }

    public void Add(Item_data itemData){
        if(dizionario_item.TryGetValue(itemData, out Inventario_item item)){
            //mettere qui: se oggetto già raccolto c'è errore  
            Debug.Log("item already collected");
            //item.AddToStack(); //se oggetto già raccolto incremento la sua stack quantità
            Debug.Log($"{item.item_data.item_name}");
            OnInventarioChange?.Invoke(inventario);

        }else{

        Inventario_item newItem = new Inventario_item(itemData); //se oggetto non ancora raccolto lo aggiungo alla lista e al dizionario
        inventario.Add(newItem); //aggiungo a lista
        dizionario_item.Add(itemData, newItem); //aggiungo a dizionario
        Debug.Log($"{itemData.item_name} added to inventory for the first time");
        Debug.Log($"{inventario.Count}");
        OnInventarioChange?.Invoke(inventario);
        }
    }



    public void Remove(Item_data itemData){
        if(dizionario_item.TryGetValue(itemData, out Inventario_item item)){
            //item.RemoveFromStack(); //se oggetto già raccolto decremento la sua stack quantità
            //if(item.stacksize == 0){
            inventario.Remove(item); // lo rimuovo dalla lista
            dizionario_item.Remove(itemData); //lo rimuovo anche dal dizionario
            //}
            OnInventarioChange?.Invoke(inventario);
        }
    }

}
