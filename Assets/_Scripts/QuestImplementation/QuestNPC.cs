using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuestNPC : MonoBehaviour
{
    public bool _inTrigger = false;
    public List<int> availableQuestIDs = new List<int> ();
    public int receivableQuestID;
    public GameObject _questAvailable;
    public GameObject _questCompleted;
    public QuestNPC questNPC;
    public GameObject coffeeQuestSign;

    public void Start()
    {
        SetQuestMarker();

        questNPC = GetComponent<QuestNPC>();
    } 

    public void SetQuestMarker()
    {
        if (QuestManager.questManager.CheckCompleteQuest(this))
        {
            _questCompleted.SetActive(true);
            coffeeQuestSign.SetActive(false);
        }
        else if (QuestManager.questManager.CheckAvailableQuest(this))
        {
            if (QuestManager.questManager.questList.Exists(quest => quest.progress == Quest.QuestProgress.ACCEPTED))
                _questAvailable.SetActive(false);
            else
            {
                if (QuestManager.questManager.FirstTaskDone)
                {
                    _questAvailable.SetActive(true);
                } else
                {
                    _questAvailable.SetActive(false);
                    if(!QuestManager.questManager.CheckCompleteQuest(this))
                        coffeeQuestSign.SetActive(true);
                }
            }
                //_questAvailable.SetActive(true);
        } else
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

    public void LookAtPlayer(Transform Player)
    {
        transform.LookAt(Player.transform);
    }

}
