using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raccolta_suoni : MonoBehaviour
{
    public int number_object = 0;
    public int max_object = 2;

    [SerializeField] public bool all_object_found = false;

    private GameObject dialogueBoxClone;

    //per comunicare che tutti gli oggetti sono stati trovati
    [SerializeField] private GameObject objects_found;
    [SerializeField] private GameObject suono_registrato;
    [SerializeField] private GameObject registration_bar;



    void Start()
    {
        
    }

    void Update()
    {
 
    }

    void OnTriggerEnter(Collider collider)
    {
        

        if (collider.gameObject.layer == 12)
        {
            
            Debug.Log("Player has entered the trigger");

            StartCoroutine(registration(collider));
            number_object++;


            if (number_object == max_object)
                all_object_found = true;

        }

        //if (all_object_found == true)
            //dialogueBoxClone = (GameObject)GameObject.Instantiate(objects_found, transform.position, Quaternion.identity);
            //Destroy(dialogueBoxClone, 3f);



    }

    IEnumerator registration(Collider collider)
    {
        

        dialogueBoxClone = (GameObject)GameObject.Instantiate(registration_bar, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(5);
        
        Destroy(collider.gameObject);
        Destroy(dialogueBoxClone, 6f);
    }

}
