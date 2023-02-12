using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSuoni : QuestNPC
{
    public GameObject startTask;
    public GameObject player;
    //suoni 
    public GameObject motor_engine_sound;
    public GameObject talking_people_sound;
    public GameObject rain_sound;
    public GameObject leaves_sound;
    public GameObject dialoguebox_sound;
    private GameObject dialogueBoxClone;
    public GameObject infoFonico;
    private bool info = false;
    void Update()
    {
        

        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && info == false)
        {
            dialogueBoxClone = (GameObject)GameObject.Instantiate(infoFonico, transform.position, Quaternion.identity);
            info = true;
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest.id == 4){
                startTask.GetComponent<Collider>().enabled = true;
                player.GetComponent<sound>().enabled = true; //attivo lo script per la raccolta dei suoni
                motor_engine_sound.SetActive(true);
                talking_people_sound.SetActive(true);
                rain_sound.SetActive(true);
                leaves_sound.SetActive(true);
            }else{
                startTask.GetComponent<Collider>().enabled = false;
                player.GetComponent<sound>().enabled = false;
            }
               
        }
        SetQuestMarker();

        if(info == true){
            if ( Input.GetKeyDown(KeyCode.Space)){
                Destroy(dialogueBoxClone);

                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound, transform.position, Quaternion.identity);
        }
        }
    }
}
