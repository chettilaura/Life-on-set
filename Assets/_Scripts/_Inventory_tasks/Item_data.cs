using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//classe che difinisce uno scriptable object: definisce il tipo "item" con le sue caratteristiche



[CreateAssetMenu]
//createassetmenu means that in project > create > item data 
public class Item_data : ScriptableObject //ScriptableObject  means che sta definendo le proprietà comuni ad ogni item data (una specie di prefab)
{
    public string item_name;
    public Sprite icon;
}
