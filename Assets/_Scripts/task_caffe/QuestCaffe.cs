using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    public DialogueScript dialogue_iniziale;
   public DialogueScript dialogue_inattesa; 
    public DialogueScript dialogue_ricevuto;
    public DialogueScript dialogue_completato;
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto, 3->Finito
    public CinemachineVirtualCamera camera_dialoghi; //camera per i dialoghi 
    private bool fine_dialogo_iniziale = false;
    private bool fine_dialogo_ricevuto = false;
    private bool fine_dialogo_inattesa = false;
    private bool fine_dialogo_completato = false;

    void Update()
    {

        //se si preme spazio dopo la spiegazione parte il primo dialogo 
        if(inizio_task == 1)
        {
            if (Input.GetKeyDown(KeyCode.Return)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe, transform.position, Quaternion.identity);
                inizio_task = 2; 
                fine_dialogo_iniziale = true; 
                dialogue_iniziale = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
            }
        
        }

        if (fine_dialogo_iniziale == true){
            if(dialogue_iniziale.fine_dialogo == true && dialogue_iniziale != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_iniziale = false;  
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
            }
        }

        if (fine_dialogo_ricevuto == true){
            if(dialogue_ricevuto.fine_dialogo == true && dialogue_ricevuto != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_ricevuto = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
            }
        }  

        if (fine_dialogo_inattesa == true){
            if(dialogue_inattesa.fine_dialogo == true && dialogue_inattesa != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_inattesa = false;
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;  
            }
        }

        if (fine_dialogo_completato == true){
            if(dialogue_completato.fine_dialogo == true && dialogue_completato != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_completato = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true; 
            }
        }

        //se si preme E vicino al regista 
        //l'ultima condizione è per obbligarlo a fermarsi prima di parlare (se no si blocca in posizioni strane)
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && Player.GetComponent<Cinemachine.Examples.CharacterMovement>().speed<0.001f)
        {
            Vector3 targetDirection = Player.transform.position - questNPC.transform.position;
            targetDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            questNPC.transform.rotation = Quaternion.RotateTowards(questNPC.transform.rotation, targetRotation, 150f * Time.deltaTime);
            Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false; //blocco il movimento del player durante dialogo 
            camera_dialoghi.Priority = camera_dialoghi.Priority +10;
            //se è la prima volta che si preme E vicino al regista mostra spiegazione regista
            if (inizio_task == 0 && QuestManager.questManager.questList[0].progress != Quest.QuestProgress.DONE)
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
                    dialogue_inattesa = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                    fine_dialogo_inattesa = true;
                }

            //si avvicina all'NPC premendo E mentre è attiva la task caffe e non ha ancora ricevuto il caffe
            if(Player.GetComponent<task_caffe>().CaffePreso && !_coffeeReceived){
                QuestManager.questManager.currentQuest.questObjectiveCount++;
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_ricevuto , transform.position, Quaternion.identity);
                dialogue_ricevuto = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_ricevuto = true;
                if(QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement){
                    QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
                }
                _coffeeReceived = true;
            }

             //se ha portato tutti e tre i caffè ha finito 
            if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2){
                Debug.Log("questObjectiveCount==questObjectiveRequirement");
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_completed , transform.position, Quaternion.identity);
                dialogue_completato = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_completato = true;
                QuestManager.questManager.FirstTaskDone = true;
            }

        }

        SetQuestMarker(); //check sui quest markers

    } //update
}


