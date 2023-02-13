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
    private GameObject dialogueBoxClone;
    public GameObject infoFonico;
    private bool info = false; //info diventa true quando la spiegazione è stata fatta vedere  
    private bool nonCompletedYet = false; //questa variabile diventa true quando torna dal NPC ma non ha ancora raccolto tutti i suoni 
    private bool _coffeeReceived = false;

    public GameObject suonoAmbienteGioco;


    void Update()
    {
        

        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) )
        {
            if (info == false)   //instanzia la spiegazione
            {
                dialogueBoxClone = (GameObject)GameObject.Instantiate(infoFonico, transform.position, Quaternion.identity);
                info = true;
            }

            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest.id == 4){    //controllo che la quest attiva sia quella dei suoni 
                startTask.GetComponent<Collider>().enabled = true;
                player.GetComponent<sound>().enabled = true; //attivo lo script per la raccolta dei suoni
                //abbassa il volume del gioco 
                suonoAmbienteGioco.GetComponent<AudioSource>().volume = 0.05f;
                motor_engine_sound.SetActive(true);
                talking_people_sound.SetActive(true);
                rain_sound.SetActive(true);
                leaves_sound.SetActive(true);

                if (nonCompletedYet == true && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.ACCEPTED)
                {
                    //esce dialogo "non hai ancora completato il task"
                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound_inProgress, transform.position, Quaternion.identity);

                }

                if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE)
                {
                    //esce dialogo " hai completato il task" & duiventa verde 
                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound_completed, transform.position, Quaternion.identity);

                }
            }
            else{
                //rialza il volume del gioco 
                suonoAmbienteGioco.GetComponent<AudioSource>().volume = 0.2f;
                startTask.GetComponent<Collider>().enabled = false;
                player.GetComponent<sound>().enabled = false;

                //qui mettere dialogo di quando si avvicina ma è attiva un'altra task

                //to do

                if(player.GetComponent<task_caffe>().CaffePreso && !_coffeeReceived)
                {
                    QuestManager.questManager.currentQuest.questObjectiveCount++;
                    _coffeeReceived = true;
                    if (QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement)
                        QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;

                    //qui mettere dialogo da inserire quando è attiva la task del caffè e viene consegnato il caffè
                }
            }
               
        }
        SetQuestMarker();   //controlla se la task è finita e se è finita mette il punto esclamativo

        if(questNPC._inTrigger &&  info == true){
            if (Input.GetKeyDown(KeyCode.Space)){
                Destroy(dialogueBoxClone);

                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound, transform.position, Quaternion.identity);
                nonCompletedYet = true;
            }
        }





    }
}
