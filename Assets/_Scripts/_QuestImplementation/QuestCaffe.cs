using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCaffe :  QuestNPC
{
    public GameObject coffeeMachine;
    public GameObject Player;
    private bool _coffeeReceived = false;

    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest.id == 0)
                coffeeMachine.GetComponent<Collider>().enabled = true;
            else
                coffeeMachine.GetComponent<Collider>().enabled = false;
        } else if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.Return) && Player.GetComponent<task_caffè>().CaffèPreso && !_coffeeReceived)
        {
            QuestManager.questManager.currentQuest.questObjectiveCount++;
            _coffeeReceived = true;
            if (QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement)
                QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
        }
        SetQuestMarker();
    }
}
