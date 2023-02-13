using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class costumi_task : MonoBehaviour, InterfaceCollectible
{
     public static event HandleItemCollected onItemDataCollected;
    //creo un tipo "HandleCoinCollected" da usare al posto di "Action" nella creazione di event che non permetteva di passare argomenti 
    public delegate void HandleItemCollected (Item_data itemData); //delegate is a function pointer, creo delegate diverso per ogni oggetto
    public Item_data CostumiData; //creo reference allo scriptable object coin -> in inspector assegno lo scriptable object (stile prefab)

    public void Collect()
    {
        Debug.Log("Collecting coin");
        Destroy(gameObject);
        onItemDataCollected?.Invoke(CostumiData);
    }
}