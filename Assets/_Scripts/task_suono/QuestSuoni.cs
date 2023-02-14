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
    public GameObject dialoguebox_sound; //questo è il dialogo dell'NPC che da la task per la prima volta
    public GameObject dialoguebox_sound_inProgress; //questo è il dialogo dell'NPC che ripete la task quando il player ci ritorna 
    public GameObject dialoguebox_sound_completed; //questo è il dialogo dell'NPC che dice di aver concluso la task 
   

    public GameObject dialoguebox_caffe_ricevuto; 

    private GameObject spiegazione_canvas;
    private GameObject dialogueBoxClone;
    public GameObject infoFonico;
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto
    private bool nonCompletedYet = true; 
    private bool _coffeeReceived = false; //questa variabile diventa true quando il player ha consegnato il caffè

    public GameObject suonoAmbienteGioco;


    void Update()
    {
        
        //istanzia il primo dialogo di partenza se è stato premuto spazio dopo aver visto la spiegazione
        if( inizio_task == 1){
            if (Input.GetKeyDown(KeyCode.Return)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound, transform.position, Quaternion.identity);
                inizio_task = 2;
            }
        }

        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) )
        {
            if (QuestManager.questManager.FirstTaskDone)
            {
                QuestManager.questManager.QuestRequest(this); //qui assegna come corrente la quest che ha questo script

                //se la quest attiva è quella dei suoni 
                if (QuestManager.questManager.currentQuest.id == 4)
                {
                    startTask.GetComponent<Collider>().enabled = true;
                    player.GetComponent<sound>().enabled = true; //attivo lo script per la raccolta dei suoni
                                                                 //abbassa il volume del gioco 
                    suonoAmbienteGioco.GetComponent<AudioSource>().volume = 0.05f;


                    //instanzia la spiegazione
                    if (inizio_task == 0)
                    {
                        spiegazione_canvas = (GameObject)GameObject.Instantiate(infoFonico, transform.position, Quaternion.identity);
                        inizio_task = 1;

                        //attiva i suoni nell'ambiente 
                        motor_engine_sound.SetActive(true);
                        talking_people_sound.SetActive(true);
                        rain_sound.SetActive(true);
                        leaves_sound.SetActive(true);
                    }

                    //si avvicina all'NPC premendo E ma non ha ancora finito questa task
                    if (nonCompletedYet == true && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.ACCEPTED && inizio_task == 2)
                    {
                        //esce dialogo "non hai ancora completato il task"
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound_inProgress, transform.position, Quaternion.identity);

                    }



                }
                else
                {
                    //rialza il volume del gioco perchè non è più attivo il task dei suoni  
                    suonoAmbienteGioco.GetComponent<AudioSource>().volume = 0.2f;
                    //disattivo 
                    startTask.GetComponent<Collider>().enabled = false;
                    //disattivo lo script per la raccolta dei suoni perchè non è attivo il task dei suoni  
                    player.GetComponent<sound>().enabled = false;



                    //si avvicina all'NPC premendo E e ha appena finito questa 
                    if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2) //se quest suoni è sengnata come fatta
                    {

                        //esce dialogo " hai completato il task" 
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound_completed, transform.position, Quaternion.identity);
                        nonCompletedYet = false;
                    }
                }

            } else
            {
                //qui dialogo per dire che non ha ancora fatto task caffè
                Debug.Log("Fai prima la task del caffè");
            }
            //si avvicina all'NPC premendo E ma è attiva la task caffe
            if (player.GetComponent<task_caffe>().CaffePreso && !_coffeeReceived)
            {
                QuestManager.questManager.currentQuest.questObjectiveCount++;
                //caffe consegnato 
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_ricevuto, transform.position, Quaternion.identity);

                _coffeeReceived = true;
                if (QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement)
                    QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
            }
        }
        SetQuestMarker();   //controlla se la task è finita e se è finita mette il punto esclamativo

        




    }
}
