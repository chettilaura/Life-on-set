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
    public GameObject spiegazione_canvas;
    public GameObject infoLuci;
    public GameObject dialoguebox_luci_completed;
    public GameObject dialoguebox_luci_inProgress;
    public GameObject dialoguebox_prima_il_caffe;
    public GameObject FinishedAllTasks;
    public GameObject Player;

    private bool nonCompletedYet = true; //questa variabile diventa true quando torna dal NPC ma non ha ancora raccolto tutti i suoni 
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto
 
    void Update()
    {


         //istanzia il primo dialogo di partenza se è stato premuto spazio dopo aver visto la spiegazione
         if( inizio_task == 1){
            if (Input.GetKeyDown(KeyCode.Return)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci, transform.position, Quaternion.identity);
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
                    spiegazione_canvas = (GameObject)GameObject.Instantiate(infoLuci, transform.position, Quaternion.identity);
                    inizio_task = 1;

                }
                QuestManager.questManager.QuestRequest(this); //mette come current quest la luci task & controlla DONE

                if (QuestManager.questManager.currentQuest.id == 3)
                {

                    //abilita il collider del relativo oggetto per iniziare la task (es. sedia regista per task differenze)
                    startTask.GetComponent<Collider>().enabled = true;
                    Light.GetComponent<ambient_light>().enabled = true;
                    Camera.SetActive(true);
                    LightGun.GetComponent<LightGun>().enabled = true;
                    //start_task_luci.SetActive(true);


                    //si avvicina all'NPC premendo E ma non ha ancora finito questa task
                    if (nonCompletedYet == true && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.ACCEPTED && inizio_task == 2)
                    {
                        //esce dialogo "non hai ancora completato il task"
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci_inProgress, transform.position, Quaternion.identity);

                    }

                }
                else
                {
                    startTask.GetComponent<Collider>().enabled = false;
                    Light.GetComponent<ambient_light>().enabled = false;
                    LightGun.GetComponent<LightGun>().enabled = false;
                    Camera.SetActive(false);

                    //si avvicina all'NPC premendo E e ha appena finito questa 
                    if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2) //se quest comparse è sengnata come fatta
                    {

                        //esce dialogo " hai completato il task" 
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci_completed, transform.position, Quaternion.identity);
                        nonCompletedYet = false;
                        if (QuestManager.questManager.CheckEverythingDone())
                        {
                            inizio_task = 3;
                        }
                    }



                }
            } else
            {
                //dialogo da far uscire quando non ha ancora fatto task caffè
                GameObject.Instantiate(dialoguebox_prima_il_caffe, transform.position, Quaternion.identity);
            }

        }
        SetQuestMarker();

        

    }
}
