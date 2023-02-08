using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComparse : QuestNPC
{
    public Transform comparse;
    public Transform aliens;
    public GameObject spawner;


    private void Update()
    {

        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest != null)
            {
                for (int i = 0; i < comparse.childCount; i++)
                {
                    comparse.GetChild(i).gameObject.SetActive(true);
                }
                comparse.gameObject.SetActive(true);
                for (int i = 0; i < aliens.childCount; i++)
                {
                    aliens.GetChild(i).gameObject.SetActive(true);
                }
                aliens.gameObject.SetActive(true);
                spawner.SetActive(true);
            } else
            {
                comparse.gameObject.SetActive(false);
                aliens.gameObject.SetActive(false);
            }

        }
        SetQuestMarker();
    }

}
