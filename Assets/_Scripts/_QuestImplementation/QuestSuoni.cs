using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSuoni : QuestNPC
{
    public GameObject startTask;
    public GameObject player; // per far partire il codice della raccolta suoni 

    //aggiungo i suoni 
    public GameObject motor_engine_sound;
    //public GameObject talking_people_sound;
    //public GameObject rain_sound;
    //public GameObject leaves_sound;
    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest != null){
                startTask.GetComponent<Collider>().enabled = true;
                player.GetComponent<sound>().enabled = true; //attivo lo script per la raccolta dei suoni
                motor_engine_sound.SetActive(true);
                //talking_people_sound.SetActive(true);
                //rain_sound.SetActive(true);
                //leaves_sound.SetActive(true);
            }else{
                startTask.GetComponent<Collider>().enabled = false;
                player.GetComponent<sound>().enabled = false;
                //non li metto il setactive(false) perche quando li raccolgo i suoni vengono distrutti 
            }
               
        }
        SetQuestMarker();
    }
}
