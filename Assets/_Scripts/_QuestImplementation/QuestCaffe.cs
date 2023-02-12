using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCaffe :  QuestNPC
{
    public GameObject coffeeMachine;

    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest.id == 0)
                coffeeMachine.GetComponent<Collider>().enabled = true;
            else
                coffeeMachine.GetComponent<Collider>().enabled = false;
        }
        SetQuestMarker();
    }
}
