using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLuci : QuestNPC
{
    public GameObject startTask;
    public GameObject Camera;
    public GameObject Light;
    public GameObject LightGun;
    public GameObject dialoguebox_luci;
    private GameObject dialogueBoxClone;
    public GameObject infoLuci;
    public GameObject dialoguebox_luci_completed;
    public GameObject dialoguebox_luci_inProgress;
    private bool info = false; //info diventa true quando la spiegazione è stata fatta vedere  
    private bool nonCompletedYet = false; //questa variabile diventa true quando torna dal NPC ma non ha ancora raccolto tutti i suoni 
    private bool _coffeeReceived = false;
 
    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if(info == false){
                dialogueBoxClone = (GameObject)GameObject.Instantiate(infoLuci, transform.position, Quaternion.identity);
                info = true;
            }
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest.id == 3)
            {
               
                //abilita il collider del relativo oggetto per iniziare la task (es. sedia regista per task differenze)
                startTask.GetComponent<Collider>().enabled = true;
                Light.GetComponent<ambient_light>().enabled = true;
                Camera.SetActive(true);
                LightGun.GetComponent<LightGun>().enabled = true ;
                //start_task_luci.SetActive(true);

            } else
            {
                startTask.GetComponent<Collider>().enabled = false;
                Light.GetComponent<ambient_light>().enabled = false;
                LightGun.GetComponent<LightGun>().enabled = false;
                Camera.SetActive(false);
            }
        }
        SetQuestMarker();

        if(questNPC._inTrigger && info == true){
            if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.Space)){
                Destroy(dialogueBoxClone);

                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci, transform.position, Quaternion.identity);
                nonCompletedYet = true;
            }
        }

        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && nonCompletedYet == true && QuestManager.questManager.currentQuest.questObjectiveCount != 2){
            //esce dialogo "non hai ancora completato il task"
            dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci_inProgress, transform.position, Quaternion.identity);
        }

        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && QuestManager.questManager.currentQuest.questObjectiveCount == 2 ){
            //esce dialogo " hai completato il task" & duiventa verde 
            dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci_completed, transform.position, Quaternion.identity); 

        }
    }
}
