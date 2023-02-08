using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class find_diff : MonoBehaviour
{

    public int contatore_diff=0;
    public GameObject immagini;
    public GameObject canvas_differenze;
    public GameObject endgame_text;
    GameObject thisFireball;

    
    
    // Start is called before the first frame update
    public void updatediff(){
        contatore_diff++;
    }

    void Update(){
        if (contatore_diff==6){
            Debug.Log("Hai trovato tutte le differenze!");
            //cancellla le due immagini 
            immagini.SetActive(false);
            //thisFireball = (GameObject)Instantiate(endgame_text);

            
            //compare scritta di fine gioco
            endgame_text.SetActive(true);
            Invoke("end_text", 5);
            
        }
    } 

    public void end_text(){
        //Destroy(thisFireball);
        endgame_text.SetActive(false);
        Debug.Log("end_text");
        canvas_differenze.SetActive(false);
    }
}