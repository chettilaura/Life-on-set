using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    private bool _inTrigger = false;
    public List<int> availableQuestIDs = new List<int> ();
    public int receivableQuestID;
    [SerializeField] private GameObject _questAvailable;
    [SerializeField] private GameObject _questCompleted;

    void Start()
    {
        SetQuestMarker();
    }

    public void SetQuestMarker()
    {
        if (QuestManager.questManager.CheckCompleteQuest(this))
        {
            _questCompleted.SetActive(true);
        }
        else if (QuestManager.questManager.CheckAvailableQuest(this))
        {
            _questAvailable.SetActive(true);
        } else
        {
            _questAvailable.SetActive(false);   
        }

    }

    void Update()
    {
        if(_inTrigger && Input.GetKeyDown(KeyCode.E)) {
            QuestManager.questManager.QuestRequest(this);
        }
        SetQuestMarker();
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
