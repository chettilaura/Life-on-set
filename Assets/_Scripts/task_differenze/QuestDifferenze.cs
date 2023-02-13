using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDifferenze : QuestNPC
{
    public GameObject startTask;
    public GameObject dialoguebox_DOP;
    private GameObject dialogueBoxClone;
    public GameObject infoDOP;
    public GameObject dialoguebox_diff_inProgress;
    public GameObject dialoguebox_diff_completed;
    private bool info = false; //info diventa true quando la spiegazione è stata fatta vedere  
    private bool nonCompletedYet = false; //questa variabile diventa true quando torna dal NPC ma non ha ancora raccolto tutti i suoni 

    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if ( info == false ){
            dialogueBoxClone = (GameObject)GameObject.Instantiate(infoDOP, transform.position, Quaternion.identity);
            info = true;
            }
            
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest.id == 5)
                startTask.GetComponent<Collider>().enabled = true;
            else
                startTask.GetComponent<Collider>().enabled = false;
        }
        SetQuestMarker();

        if(questNPC._inTrigger && info == true){
            if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.Space)){
                Destroy(dialogueBoxClone);

                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_DOP, transform.position, Quaternion.identity);
                nonCompletedYet = true;
            }
        }

        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && nonCompletedYet == true && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.ACCEPTED)
        {
            //esce dialogo "non hai ancora completato il task"
            dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_diff_inProgress, transform.position, Quaternion.identity);
        }

        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE){
            //esce dialogo " hai completato il task" & duiventa verde 
            dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_diff_completed, transform.position, Quaternion.identity);
        }
    }
}
