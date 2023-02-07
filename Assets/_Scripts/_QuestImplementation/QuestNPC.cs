using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public bool _inTrigger = false;
    public List<int> availableQuestIDs = new List<int> ();
    public int receivableQuestID;
    public GameObject _questAvailable;
    public GameObject _questCompleted;
    public QuestNPC questNPC;

    void Start()
    {
        SetQuestMarker();
        questNPC = this;
        Debug.Log("Here");
    }

    public void SetQuestMarker()
    {
        Debug.Log("Set quest marker");
        if (QuestManager.questManager.CheckCompleteQuest(this))
        {
            _questCompleted.SetActive(true);
        }
        else if (QuestManager.questManager.CheckAvailableQuest(this))
        {
            _questAvailable.SetActive(true);
        }
        else
        {
            _questAvailable.SetActive(false);
            _questCompleted.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            _inTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _inTrigger = false;
    }

}
