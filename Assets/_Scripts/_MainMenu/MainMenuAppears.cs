using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAppears : MonoBehaviour
{
    public GameObject MainMenu_prefab;
    void Update(){
     if (Input.GetKeyDown(KeyCode.Escape)) 
        {

            if (MainMenu_prefab.activeSelf == false)
            {
                MainMenu_prefab.SetActive(true);
            }
            else
            {
                MainMenu_prefab.SetActive(false);
            }
        }
    }
}
