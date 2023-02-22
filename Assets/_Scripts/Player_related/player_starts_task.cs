using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class player_starts_task : MonoBehaviour
{
    private bool one_box = true;
    private bool active_differenze=false;
    private bool active_costumi=false;
    private GameObject dialogueBoxClone;
    [SerializeField] private GameObject comunicazione_start;
    public Animator anim;


    [SerializeField] private GameObject start_task_differenze ;
    [SerializeField] private GameObject start_task_costumi ;
    [SerializeField] private GameObject start_task_suoni ;
    [SerializeField] private GameObject start_task_comparse ; 

    public GameObject faretto;
    Light spotlight;
    public List<Light> luci_ambiente;
    private bool luci_accese = true;
    private bool faretto_acceso = false;
    //questo script contiene oggetti (canvas) da instanziare quando il player entra in trigger con l'oggetto trigger della task 
        //esempio: player va contro sedia director e compare "Premi INVIO per iniziare il task" + canvas con gioco differenze

    private void OnTriggerStay(Collider other)
    {


        //task differenze
        if(other.gameObject.layer == 14)
        {
            /*
            if (one_box)
            {
                //istanzia la canvas del box di comunicazione
                dialogueBoxClone = GameObject.Instantiate(comunicazione_start, transform.position, Quaternion.identity);
                one_box = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                
                //instanzia la canvas del task da far partire 
                */
                if(active_differenze==false){
                GameObject.Instantiate(start_task_differenze, transform.position, Quaternion.identity);
                active_differenze=true;
                }

                //Destroy(dialogueBoxClone, 0.5f);
            //}

        }

        //task luci 
        if(other.gameObject.layer == 16)
        {
           /* if (one_box)
            {
                dialogueBoxClone = GameObject.Instantiate(comunicazione_start, transform.position, Quaternion.identity);
                one_box = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("task luci abilitato");
                //GameObject.Instantiate(start_task_luci, transform.position, Quaternion.identity);
                Destroy(dialogueBoxClone, 0.5f);
            } */
        }

        //task costumi 
        if(other.gameObject.layer == 17)
        {
            /*
            if (one_box)
            {
                dialogueBoxClone = GameObject.Instantiate(comunicazione_start, transform.position, Quaternion.identity);
                one_box = false;
            }
            */

            //if (Input.GetKeyDown(KeyCode.Return))
            //{
                if(active_costumi==false){
                GameObject.Instantiate(start_task_costumi, transform.position, Quaternion.identity);
                active_costumi=true;
                }
                //Destroy(dialogueBoxClone, 0.5f);

            //}
        }


        //task suoni  
        if(other.gameObject.layer == 19)
        {
            if (one_box)
            {
                dialogueBoxClone = GameObject.Instantiate(comunicazione_start, transform.position, Quaternion.identity);
                one_box = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameObject.Instantiate(start_task_suoni, transform.position, Quaternion.identity);
                Destroy(dialogueBoxClone, 0.5f);
            }
        }

        //accensione faretto 
        if(other.gameObject.layer == 20){
            if(Input.GetKeyDown(KeyCode.E)){
                if(faretto_acceso == false){
                    spotlight = (faretto.transform.Find("Directional Light")?.gameObject).GetComponent<Light>();
                    spotlight.enabled = true;
                    faretto_acceso = true;
                } else {
                    spotlight = (faretto.transform.Find("Directional Light")?.gameObject).GetComponent<Light>();
                    spotlight.enabled = false;
                    faretto_acceso = false;
                }
            }

        }

         if(other.gameObject.layer == 21){
            if(Input.GetKeyDown(KeyCode.E)){
                if(luci_accese == true){
                    for(int i=0; i<luci_ambiente.Count; i++)
                    {
                        luci_ambiente[i].enabled = false;   
                    }
                    luci_accese = false;
                    anim.SetBool("On", false);
                    Debug.Log("luci spente");
                }  else {
                    for (int i = 0; i < luci_ambiente.Count; i++)
                    {
                        luci_ambiente[i].enabled = true;
                    }
                    luci_accese = true;
                    Debug.Log("luci accese");
                    anim.SetBool("On", true);
                }  
            }
        }    
    }
}
