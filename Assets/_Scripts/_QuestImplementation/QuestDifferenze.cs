using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDifferenze : QuestNPC
{
    public GameObject startTask;
    public GameObject dialoguebox_DOP;
    private GameObject dialogueBoxClone;
    public GameObject infoDOP;
    private bool info = false;

    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && info == false)
        {
            dialogueBoxClone = (GameObject)GameObject.Instantiate(infoDOP, transform.position, Quaternion.identity);
            info = true;
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest.id == 5)
                startTask.GetComponent<Collider>().enabled = true;
            else
                startTask.GetComponent<Collider>().enabled = false;
        }
        SetQuestMarker();

        if(info == true){
            if ( Input.GetKeyDown(KeyCode.Space)){
                Destroy(dialogueBoxClone);

                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_DOP, transform.position, Quaternion.identity);
            }
        }
    }
}
