using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComparse : QuestNPC
{
    public Transform script;


    private void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {

            QuestManager.questManager.QuestRequest(this);
            for (int i = 0; i < script.childCount; i++)
            {
                script.GetChild(i).gameObject.SetActive(true);
            }
            script.gameObject.SetActive(true);
        }
        SetQuestMarker();
    }

}
