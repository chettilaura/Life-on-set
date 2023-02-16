using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksInMenu : MonoBehaviour
{

    public List<GameObject> GeneralTasks;
    public List<GameObject> FinishedTasks;
    public int contatore = 0;

    public void FillFinishedTasks()  //controlla quali quests sono già finite e possono essere mostrate
    {
        if (QuestManager.questManager.questList[1].progress == Quest.QuestProgress.DONE && !FinishedTasks.Contains(GeneralTasks[7])) //comparse
            FinishedTasks[7]=GeneralTasks[7]; //aiuto regista
            
        if (QuestManager.questManager.questList[2].progress == Quest.QuestProgress.DONE && !FinishedTasks.Contains(GeneralTasks[2])) //costumi
            FinishedTasks[1] = GeneralTasks[2]; //costumista
        if (QuestManager.questManager.questList[3].progress == Quest.QuestProgress.DONE && !FinishedTasks.Contains(GeneralTasks[5])) //illuminazione
        {
            FinishedTasks[2] = GeneralTasks[5]; //luci
            FinishedTasks[3] = GeneralTasks[6];  //DOP
        }
        if (QuestManager.questManager.questList[4].progress == Quest.QuestProgress.DONE && !FinishedTasks.Contains(GeneralTasks[0])) //suoni
        {
            FinishedTasks[4] = GeneralTasks[0]; //suoni
            FinishedTasks[5] = GeneralTasks[1]; //fonico
        }
        if (QuestManager.questManager.questList[5].progress == Quest.QuestProgress.DONE && !FinishedTasks.Contains(GeneralTasks[4])) //differenze/continuita'
            FinishedTasks[6] = GeneralTasks[4]; //continuity
        if (QuestManager.questManager.questList[0].progress == Quest.QuestProgress.DONE && !FinishedTasks.Contains(GeneralTasks[3])) // caffe e regista
            FinishedTasks[0] = GeneralTasks[3]; //regista
        FinishedTasks[0].SetActive(true);
    }
    public void RightArrow()
    {
        Debug.Log(contatore);
        FinishedTasks[contatore].SetActive(false);
        if(contatore == FinishedTasks.Count - 1)
        {
            contatore = 0;
        } else
        {
            contatore++;
        }
        FinishedTasks[contatore].SetActive(true);
    }

    public void LeftArrow()
    {
        FinishedTasks[contatore].SetActive(false);
        if (contatore == 0)
        {
            contatore = FinishedTasks.Count - 1;
        }
        else
        {
            contatore--;
        }
        FinishedTasks[contatore].SetActive(true);
    }
}
