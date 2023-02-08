using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_starts_task : MonoBehaviour
{
    private bool one_box = true;
    private GameObject dialogueBoxClone;
    [SerializeField] private GameObject comunicazione_start;
    [SerializeField] private GameObject start_task;
    //quando mi avvicino al tavolo, compare box premi E per iniziare gioco
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 14)
        {
            if (one_box)
            {
                dialogueBoxClone = GameObject.Instantiate(comunicazione_start, transform.position, Quaternion.identity);
                one_box = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(dialogueBoxClone, 1f);

                dialogueBoxClone = GameObject.Instantiate(start_task, transform.position, Quaternion.identity);
            }
        }
    }
}
