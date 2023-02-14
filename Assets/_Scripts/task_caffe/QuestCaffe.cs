using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCaffe :  QuestNPC
{
    public GameObject coffeeMachine;
    public GameObject Player;
    private bool _coffeeReceived = false;
   
    public GameObject dialoguebox_caffe;
    public GameObject dialoguebox_caffe_completed;
    public GameObject dialoguebox_caffe_ricevuto;
    public GameObject dialoguebox_caffe_inAttesa;
    private GameObject dialogueBoxClone;
    public GameObject spiegazione_canvas;
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto
    void Update()
    {

        //se si preme spazio dopo la spiegazione parte il primo dialogo 
        if(inizio_task == 1)
        {
            if (Input.GetKeyDown(KeyCode.Return)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe, transform.position, Quaternion.identity);
                inizio_task = 2;
            }
        }

        //se si preme E vicino al regista 
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) )
        {

            //se è la prima volta che si preme E vicino al regista mostra spiegazione regista
            if (inizio_task == 0)
            {
                spiegazione_canvas = (GameObject)GameObject.Instantiate(spiegazione_canvas, transform.position, Quaternion.identity);
                inizio_task = 1;
            }

            QuestManager.questManager.QuestRequest(this); // assegna come corrente la task caffe

            //se la current quest è la task caffe abilita macchinetta caffe
            if (QuestManager.questManager.currentQuest.id == 0)
                coffeeMachine.GetComponent<Collider>().enabled = true;
            else
                coffeeMachine.GetComponent<Collider>().enabled = false;


             //si avvicina all'NPC premendo E ma non ha ancora finito questa task
                if (!Player.GetComponent<task_caffe>().CaffePreso && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.ACCEPTED && inizio_task == 2 && QuestManager.questManager.currentQuest.id == 0 )
                {
                    //esce dialogo "non hai ancora completato il task"
                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_inAttesa, transform.position, Quaternion.identity);

                }

            //si avvicina all'NPC premendo E mentre è attiva la task caffe e non ha ancora ricevuto il caffe
            if(Player.GetComponent<task_caffe>().CaffePreso && !_coffeeReceived){
                  QuestManager.questManager.currentQuest.questObjectiveCount++;
                  dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_ricevuto , transform.position, Quaternion.identity);
                    
                    if(QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement){
                        QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
                    }
                    _coffeeReceived = true;
            }



             //se ha portato tutti e tre i caffè ha finito 
            if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2){
                    Debug.Log("questObjectiveCount==questObjectiveRequirement");
                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_completed , transform.position, Quaternion.identity);
                }

        }

        SetQuestMarker(); //check sui quest markers

    
       

    } //update
} //script

