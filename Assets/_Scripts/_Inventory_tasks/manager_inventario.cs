using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager_inventario : MonoBehaviour
{
 
    public GameObject slotPrefab; // qui droppo lo slot prefab creato
    public List<slot_inventario> inventoryslots = new List<slot_inventario>(6); //vuoto, riempito con ciclo for & instantiate slotPrefab 


     private void OnEnable()
    {
        //Debug.Log("ricevuta invocazione post iscrizione ad evento OnInventarioChange");
        Inventario.OnInventarioChange += DrawInventario; //quando l'inventario viene aggiornato, disegna l'inventario
    }

    private void OnDisable()
    {
        Inventario.OnInventarioChange -= DrawInventario;
    }

    


    void ResetInventario(){ 
        foreach (Transform childTransform in transform) //gli slot sono child del pannello invenatrio, prende tutti i figli (SLOT) e li distrugge
        {
            Destroy(childTransform.gameObject);
        }
        inventoryslots=new List<slot_inventario>(6); //resetto inventario
    }



    void DrawInventario (List<Inventario_item> inventario){
        //Debug.Log("metodo draw inventario");

        ResetInventario();

        //create 8 empty slots nell'inventario: qui dentro NON usa la lista inventario ricevuta 
        for (int i = 0; i < inventoryslots.Capacity ; i++)
        {
            GameObject newslot = Instantiate(slotPrefab); //istanza prefab
            newslot.transform.SetParent(transform,false); //modifiche alla sua posizione

            slot_inventario newSlotComponent = newslot.GetComponent<slot_inventario>(); //crea uno slot inventario, lo riempie con le info del prefab
            newSlotComponent.ClearSlot(); //imposta a false le variabili pubbliche dello slot inventario creato

            inventoryslots.Add(newSlotComponent); 
        }

        //aggiorno ogni slot accordingly to the list passata: riempie lista vuota creata con lista piena ricevuta 
        // qui riempie solo le var pubbliche di slot inventario che sono le reference ai figli 
        for (int i = 0; i < inventario.Count; i++)
        {
            inventoryslots[i].DrawSlot(inventario[i]);
        }

    }




}
