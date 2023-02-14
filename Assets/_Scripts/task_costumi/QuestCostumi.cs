using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCostumi : QuestNPC
{
    public GameObject startTask;

    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (QuestManager.questManager.FirstTaskDone)
            {
                QuestManager.questManager.QuestRequest(this);
                if (QuestManager.questManager.currentQuest.id == 2)
                    startTask.GetComponent<Collider>().enabled = true;
                else
                    startTask.GetComponent<Collider>().enabled = false;
            } else
            {
                //scrivere dialogo task caffè non fatta
            }

        }
        SetQuestMarker();
    }
}
