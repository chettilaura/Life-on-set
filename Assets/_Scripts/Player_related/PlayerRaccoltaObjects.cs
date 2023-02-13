using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaccoltaObjects : MonoBehaviour
{
    public int number_object = 0;
    public int max_object = 2;

    [SerializeField] public bool all_object_found = false;

    private GameObject dialogueBoxClone;

    [SerializeField] private GameObject object_found;




    void Start()
    {


    }


    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.layer == 7)
        {

            Debug.Log("Player has entered the trigger");
            Destroy(collider.gameObject);
            number_object++;

            if (number_object == max_object)
                all_object_found = true;

            comunicazione_raccolta_oggetti();

        }

    }

    void comunicazione_raccolta_oggetti()
    {
        if (all_object_found == true)
            dialogueBoxClone = (GameObject)GameObject.Instantiate(object_found, transform.position, Quaternion.identity);
        Destroy(dialogueBoxClone, 3f);

    }

}
