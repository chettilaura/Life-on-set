using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSuoni : QuestNPC
{
    public GameObject startTask;
    public GameObject player; // per far partire il codice della raccolta suoni 
    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest != null)
                startTask.GetComponent<Collider>().enabled = true;
                player.GetComponent<sound>().enabled = true; //attivo lo script per la raccolta dei suoni
            else
                startTask.GetComponent<Collider>().enabled = false;
                player.GetComponent<sound>().enabled = false;
        }
        SetQuestMarker();
    }
}
