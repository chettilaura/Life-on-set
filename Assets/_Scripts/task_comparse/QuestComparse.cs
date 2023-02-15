using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComparse : QuestNPC
{
    public Transform comparse;
    public Transform aliens;
    private bool _coffeeReceived = false;
    public GameObject Player;
    private GameObject dialogueBoxClone;
    private GameObject spiegazione_canvas;
    public GameObject info_aiutoregista;
    private bool nonCompletedYet = true; 

    //dialoghi 
    public GameObject dialoguebox_comparse_iniziale;
    public GameObject dialoguebox_caffe_ricevuto;
    public GameObject dialoguebox_comparse_inProgress;
    public GameObject dialogo_comparse_completed;
    public GameObject dialoguebox_prima_il_caffe;
    public GameObject FinishedAllTasks;
    
    
     private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto

    private void Update()
    {

         if(inizio_task ==1){
            if (Input.GetKeyDown(KeyCode.Return)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_comparse_iniziale, transform.position, Quaternion.identity);
                inizio_task = 2;
            }
         }

        //istanzia il dialogo super finale

        if (inizio_task == 3)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(FinishedAllTasks, transform.position, Quaternion.identity);
                inizio_task = 4;
            }
        }



        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) )
        {
            if (QuestManager.questManager.FirstTaskDone)
            {
                QuestManager.questManager.QuestRequest(this); //assegna questa come quest corrente che come DONE 


                if (QuestManager.questManager.currentQuest.id == 1)
                {
                    //primo dialogo istanziato
                    if (inizio_task == 0)
                    {
                        spiegazione_canvas = (GameObject)GameObject.Instantiate(info_aiutoregista, transform.position, Quaternion.identity);
                        inizio_task = 1;
                    }


                    //attiva tutte le comparse
                    for (int i = 0; i < comparse.childCount; i++)
                    {
                        comparse.GetChild(i).gameObject.SetActive(true);
                    }
                    comparse.gameObject.SetActive(true);
                    for (int i = 0; i < aliens.childCount; i++)
                    {
                        aliens.GetChild(i).gameObject.SetActive(true);
                    }
                    aliens.gameObject.SetActive(true);



                    //si avvicina all'NPC premendo E ma non ha ancora finito questa task
                    if (nonCompletedYet == true && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.ACCEPTED && inizio_task == 2)
                    {
                        //esce dialogo "non hai ancora completato il task"
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_comparse_inProgress, transform.position, Quaternion.identity);

                    }


                }
                else //la quest attiva non è più questa
                {
                    //disattiva tutte le comparse
                    for (int i = 0; i < comparse.childCount; i++)
                    {
                        comparse.GetChild(i).gameObject.SetActive(false);
                    }
                    comparse.gameObject.SetActive(false);
                    for (int i = 0; i < aliens.childCount; i++)
                    {
                        aliens.GetChild(i).gameObject.SetActive(false);
                    }
                    aliens.gameObject.SetActive(false);
                    


                    //si avvicina all'NPC premendo E e ha appena finito questa 
                    if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2) //se quest comparse è sengnata come fatta
                    {

                        Debug.Log("è entrato nell'if di conclusione task");
                        //esce dialogo " hai completato il task" 
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialogo_comparse_completed, transform.position, Quaternion.identity);
                        nonCompletedYet = false;
                        if (QuestManager.questManager.CheckEverythingDone())
                        {
                            inizio_task = 3;
                        }
                    }
                }

            } else
            {
                //mettere dialogo per task caffè non ancora fatta
                 dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_prima_il_caffe, transform.position, Quaternion.identity);

            }

            //si avvicina all'NPC premendo E ma è attiva la task caffe
            if (Player.GetComponent<task_caffe>().CaffePreso && !_coffeeReceived)
            {
                QuestManager.questManager.currentQuest.questObjectiveCount++;
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_ricevuto, transform.position, Quaternion.identity);
                _coffeeReceived = true;
                if (QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement)
                    QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
            }


        }


        SetQuestMarker();

       
    }

}
