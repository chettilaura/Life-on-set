using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raccolta_suoni : MonoBehaviour
{

    //[SerializeField] public bool all_object_found = false;

    private GameObject dialogueBoxClone;

    Coroutine co;

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

            //if (exit == false)
            //{
                co = StartCoroutine(registration(collider));
            //}
            //Destroy(dialogueBoxClone,7f);
            QuestManager.questManager.currentQuest.questObjectiveCount++;


            if (QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement)
                QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;

        }

        //if (all_object_found == true)
            //dialogueBoxClone = (GameObject)GameObject.Instantiate(objects_found, transform.position, Quaternion.identity);
            //Destroy(dialogueBoxClone, 3f);



    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == 12)
        {
            Debug.Log("Player exit");

            
            StopCoroutine(co);
            Destroy(dialogueBoxClone, 0.5f);
        }
    }

    IEnumerator registration(Collider collider)
    {

        dialogueBoxClone = (GameObject)GameObject.Instantiate(registration_bar, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);

        Destroy(collider.gameObject);
        Destroy(dialogueBoxClone, 6f);
    }

}
