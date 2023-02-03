using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//riempie variabili pubbliche create con le info dell'item data -> non modifica i figli dello slot

public class slot_inventario : MonoBehaviour
{

    //in queste due variabili pubbliche metto il riferimento ai figli (icon & text) dello slot
    public Image icon;
    public TextMeshProUGUI text;
    //public TextMeshProUGUI stacksizeText;

    //disattiva le due variabili pubbliche dello slot -> quindi visibilità due figli 
   public void ClearSlot()
    {
        icon.enabled=false;
        text.enabled=false;
        //stacksizeText.enabled=false; 
       
    }

    public void DrawSlot(Inventario_item item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }

    //attiva le due variabili pubbliche dello slot -> quindi visibilità due figli 
        icon.enabled=true;
        text.enabled=true;
        //stacksizeText.enabled=true;

    //riempie le due variabili pubbliche dello slot con le info dell'item data -> quindi riempie due figli 
        icon.sprite=item.item_data.icon;
        text.text=item.item_data.name; 
        //stacksizeText.text=item.stacksize.ToString();
        
    }

}
        


