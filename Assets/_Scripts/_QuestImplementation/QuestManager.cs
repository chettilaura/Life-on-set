using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public static QuestManager questManager;
    public List<Quest> questList = new List<Quest> ();
    public Quest currentQuest;

    private void Awake()
    {
        if (questManager == null)
            questManager = this;
        else if (questManager != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void QuestRequest(QuestNPC questNPC)
    {
        //available quests
        if(questNPC.availableQuestIDs.Count > 0)
        {
            for(int i= 0; i < questList.Count; i++)
            {
                for(int j=0; j < questNPC.availableQuestIDs.Count; j++)
                {
                    if (questList[i].id == questNPC.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                    {
                        Debug.Log("Quest ID: " + questNPC.availableQuestIDs[j] + " " + questList[i].progress);
                        AcceptQuest(questNPC.availableQuestIDs[j]);
                        //quest ui manager
                    }
                }
            } 
        }

        //active quest
        if(currentQuest.id ==questNPC.receivableQuestID && currentQuest.progress == Quest.QuestProgress.ACCEPTED || currentQuest.progress == Quest.QuestProgress.COMPLETE)
        {
            Debug.Log("Quest ID: " + questNPC.receivableQuestID + " is " + currentQuest.progress);
            CompleteQuest(questNPC.receivableQuestID);
        }

    }


    //ACCEPT QUEST
    public void AcceptQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                questList[i].progress = Quest.QuestProgress.ACCEPTED;
                currentQuest = questList[i];
            }
        }
    }

    //COMPLETE QUEST

    public void CompleteQuest(int questID)
    {
        if(currentQuest.id == questID && currentQuest.progress == Quest.QuestProgress.COMPLETE)
        {
            currentQuest.progress = Quest.QuestProgress.DONE;
            currentQuest = null;
        }
    }

    //BOOLS
    public bool RequestAvailableQuest(int questID)
    {
        for(int i=0; i<questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                return true;
        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        if (currentQuest.id == questID)
            return true;
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
                return true;
        }
        return false;
    }

    //BOOLS 2
    public bool CheckAvailableQuest(QuestNPC questNPC)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for(int j=0; j<questNPC.availableQuestIDs.Count; j++)
            {
                if (questList[i].id == questNPC.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckAcceptedQuest(QuestNPC questNPC)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < questNPC.availableQuestIDs.Count; j++)
            {
                if (questList[i].id == questNPC.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckCompleteQuest(QuestNPC questNPC)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questNPC.receivableQuestID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }
}
