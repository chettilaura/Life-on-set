using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_tutorial_appears : MonoBehaviour
{

    public GameObject tutorial_canvas_prefab;
    public tutorial_una_sola_volta tutorial_una_sola_volta;

    void Update(){
    if(tutorial_una_sola_volta.tutorial_attivo == true){
        tutorial_canvas_prefab.SetActive(true);
    }else{
        tutorial_canvas_prefab.SetActive(false);
    }

}
    /*public GameObject tutorial_prefab;
    void Update(){
     if (Input.GetKeyDown(KeyCode.Question) )
        {

            if (tutorial_prefab.activeSelf == false)
            {
                tutorial_prefab.SetActive(true);
            }
            else
            {
                tutorial_prefab.SetActive(false);
            }
        }
    }*/




}
