using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_starts_task : MonoBehaviour
{
    private bool one_box = true;
    private GameObject dialogueBoxClone;
    [SerializeField] private GameObject comunicazione_start;


    [SerializeField] private GameObject start_task_differenze ;
    [SerializeField] private GameObject start_task_luci ;
    [SerializeField] private GameObject start_task_costumi ;
    [SerializeField] private GameObject start_task_caffe ;
    [SerializeField] private GameObject start_task_suoni ;
    [SerializeField] private GameObject start_task_comparse ; 

 

    //questo script contiene oggetti (canvas) da instanziare quando il player entra in trigger con l'oggetto trigger della task 
        //esempio: player va contro sedia director e compare "Premi INVIO per iniziare il task" + canvas con gioco differenze

    private void OnTriggerStay(Collider other)
    {


        //task differenze
        if(other.gameObject.layer == 14)
        {
            if (one_box)
            {
                //istanzia la canvas del box di comunicazione
                dialogueBoxClone = GameObject.Instantiate(comunicazione_start, transform.position, Quaternion.identity);
                one_box = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                
                //instanzia la canvas del task da far partire 
                GameObject.Instantiate(start_task_differenze, transform.position, Quaternion.identity);
                Destroy(dialogueBoxClone, 0.5f);
            }

        }

        //task luci 
        if(other.gameObject.layer == 16)
        {
            if (one_box)
            {
                dialogueBoxClone = GameObject.Instantiate(comunicazione_start, transform.position, Quaternion.identity);
                one_box = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameObject.Instantiate(start_task_luci, transform.position, Quaternion.identity);
                Destroy(dialogueBoxClone, 0.5f);
            }
        }

        //task costumi 
        if(other.gameObject.layer == 17)
        {
            if (one_box)
            {
                dialogueBoxClone = GameObject.Instantiate(comunicazione_start, transform.position, Quaternion.identity);
                one_box = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameObject.Instantiate(start_task_costumi, transform.position, Quaternion.identity);
                Destroy(dialogueBoxClone, 0.5f);
            }
        }

        //task caffe 
        if(other.gameObject.layer == 18)
        {
            if (one_box)
            {
                dialogueBoxClone = GameObject.Instantiate(comunicazione_start, transform.position, Quaternion.identity);
                one_box = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameObject.Instantiate(start_task_caffe, transform.position, Quaternion.identity);
                Destroy(dialogueBoxClone, 0.5f);
            }
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
    }
}
