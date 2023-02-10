using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLuci : QuestNPC
{
   public GameObject startTask;

    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest != null)
                //abilita il collider del relativo oggetto per iniziare la task (es. sedia regista per task differenze)
                startTask.GetComponent<Collider>().enabled = true;
            else
                startTask.GetComponent<Collider>().enabled = false;
        }
        SetQuestMarker();
    }
}
