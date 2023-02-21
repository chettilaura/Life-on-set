using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCostumi : QuestNPC
{
    public GameObject startTask;
     public GameObject dialoguebox_iniziale;
      private GameObject dialogueBoxClone;
    private GameObject spiegazione_canvas;
    public GameObject infoCosumista;
    public GameObject dialoguebox_costumi_completed;
    public GameObject dialoguebox_prima_il_caffe;
    public GameObject FinishedAllTasks;
    public GameObject Player;
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto, 3-> finito


    void Update()
    {


        //istanzia il primo dialogo di partenza se è stato premuto spazio dopo aver visto la spiegazione
         if( inizio_task == 1){
            if (Input.GetKeyDown(KeyCode.Return)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_iniziale, transform.position, Quaternion.identity);
                inizio_task = 2;
            }
         }

        //istanzia il dialogo super finale

        if (inizio_task == 3)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                dialogueBoxClone = (GameObject)GameObject.Instantiate(FinishedAllTasks, transform.position, Quaternion.identity);
                inizio_task = 4;
            }
        }


        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            //NPC si gira verso il player
            LookAtPlayer(Player.transform);
            if (QuestManager.questManager.FirstTaskDone)
             {


                if (inizio_task == 0)
                {
                    spiegazione_canvas = (GameObject)GameObject.Instantiate(infoCosumista, transform.position, Quaternion.identity);
                    inizio_task = 1;

                }

                
                
                QuestManager.questManager.QuestRequest(this); //mette questa come quest corrente

                
                if (QuestManager.questManager.currentQuest.id == 2)
                    startTask.GetComponent<Collider>().enabled = true; //abilita canvas costumi
                else
                    startTask.GetComponent<Collider>().enabled = false; //disabilita canvas costumi 


                 if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2)
                {
                    //esce dialogo " hai completato il task" & diventa verde 
                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_costumi_completed, transform.position, Quaternion.identity);
                    if (QuestManager.questManager.CheckEverythingDone())
                    {
                        inizio_task = 3;
                    }
                }
                    

            } else
            {
                //dialogo task caffè non fatta
               dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_prima_il_caffe, transform.position, Quaternion.identity);
                
            }   


        } //fine intrigger + E

    

        SetQuestMarker();
    }
}
