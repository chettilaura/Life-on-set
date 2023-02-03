using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

//classe relativa al singolo slot dell'inventario: lo prende e lo conta nell'inventario
public class Inventario_item  
{
    public Item_data item_data;
    //public int stacksize;


    //costruttore
    public Inventario_item(Item_data item_data)
    {
        this.item_data = item_data;
        //AddToStack();
        
    }


    public void AddToStack(){
        //stacksize++;
    }

    public void RemoveFromStack(){
        //stacksize--;
    }


}

