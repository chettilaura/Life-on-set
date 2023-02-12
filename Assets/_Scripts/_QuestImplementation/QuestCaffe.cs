using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCaffe :  QuestNPC
{
    public GameObject coffeeMachine;
    public GameObject Player;
    private bool _coffeeReceived = false;
    public GameObject dialoguebox_caffe;
    private GameObject dialogueBoxClone;
    public GameObject info_regista;
    private bool info = false;
    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && info == false)
        {
            dialogueBoxClone = (GameObject)GameObject.Instantiate(info_regista, transform.position, Quaternion.identity);
            info = true;
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest.id == 0)
                coffeeMachine.GetComponent<Collider>().enabled = true;
            else
                coffeeMachine.GetComponent<Collider>().enabled = false;
        } else if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.Return) && Player.GetComponent<task_caffe>().CaffePreso && !_coffeeReceived)
        {
            QuestManager.questManager.currentQuest.questObjectiveCount++;
            _coffeeReceived = true;
            if (QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement)
                QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
        }
        SetQuestMarker();

         if(info == true){
            if (Input.GetKeyDown(KeyCode.Space)){
                Destroy(dialogueBoxClone);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe, transform.position, Quaternion.identity);
            }
         }
    }
}
