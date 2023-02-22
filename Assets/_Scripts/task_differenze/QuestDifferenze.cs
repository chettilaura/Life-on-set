using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class QuestDifferenze : QuestNPC
{
    public GameObject startTask;
    public GameObject dialoguebox_DOP;
    private GameObject dialogueBoxClone;
    private GameObject spiegazione_canvas;
    public GameObject infoContinuity;
    public GameObject dialoguebox_prima_il_caffe;
    public GameObject Player;
    //public GameObject dialoguebox_diff_inProgress; //qui non lo facciamo perché deve finirlo per forza una volta iniziato 
    public GameObject dialoguebox_diff_completed;
    public GameObject FinishedAllTasks;

    //private bool nonCompletedYet = true; //questa variabile diventa true quando torna dal NPC ma non ha ancora raccolto tutti i suoni 
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto, 3 ->finito

   

    //movimento camera
   public CinemachineVirtualCamera camera_dialoghi; //camera per i dialoghi 
    public DialogueScript dialogue_iniziale;
    public DialogueScript dialogue_inattesa; 
    public DialogueScript dialogue_ricevuto;
    public DialogueScript dialogue_completato;
    public DialogueScript dialogue_prima_il_caffe;
    public DialogueScript dialogue_finishedAllTasks;
    
    private bool fine_dialogo_iniziale = false;
    private bool fine_dialogo_caffe_ricevuto = false;
    private bool fine_dialogo_inattesa = false;
    private bool fine_dialogo_completato = false;
    private bool fine_dialogo_prima_il_caffe = false;
    private bool fine_dialogo_finishedAllTasks = false;

    void Update()

    {
        //6 movimenti di camera dei 6 dialoghi 

        if (fine_dialogo_iniziale == true){
            if(dialogue_iniziale.fine_dialogo == true && dialogue_iniziale != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_iniziale = false;  
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
            }
        }

        if (fine_dialogo_caffe_ricevuto == true){
            if(dialogue_ricevuto.fine_dialogo == true && dialogue_ricevuto != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_caffe_ricevuto = false; 
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
        if(fine_dialogo_prima_il_caffe == true){
            if(dialogue_prima_il_caffe.fine_dialogo == true && dialogue_prima_il_caffe != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_prima_il_caffe = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true; 
            }
        }

        if(fine_dialogo_finishedAllTasks == true){
            if(dialogue_finishedAllTasks.fine_dialogo == true && dialogue_finishedAllTasks != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_finishedAllTasks = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true; 
            }
        }
         //istanzia il primo dialogo di partenza se è stato premuto spazio dopo aver visto la spiegazione
        if( inizio_task == 1){
            if (Input.GetKeyDown(KeyCode.Mouse0)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_DOP, transform.position, Quaternion.identity);
                dialogue_iniziale = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_iniziale = true;  
                inizio_task = 2;
            }
         }

        //istanzia il dialogo super finale

       /*if (inizio_task == 3)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                dialogueBoxClone = (GameObject)GameObject.Instantiate(FinishedAllTasks, transform.position, Quaternion.identity);
                inizio_task = 4;
            }
        }*/

        

        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && Player.GetComponent<Cinemachine.Examples.CharacterMovement>().speed<0.001f)
        {
            //NPC si gira verso il player
            LookAtPlayer(Player.transform);
            //blocco il moviemnto del player durante il dialogo
            Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false; 
            camera_dialoghi.Priority = camera_dialoghi.Priority +10;

            //controllo prima task caffe completata
            if (QuestManager.questManager.FirstTaskDone)
            {
                //inizia spiegazione
                if (inizio_task == 0)
                {
                    spiegazione_canvas = (GameObject)GameObject.Instantiate(infoContinuity, transform.position, Quaternion.identity);
                    inizio_task = 1;

                }

                QuestManager.questManager.QuestRequest(this);

                //controllo se la task è quella del DOP
                if (QuestManager.questManager.currentQuest.id == 5){
                    startTask.GetComponent<Collider>().enabled = true; //attiva collider sulla sedia 
                }
                else{
                    startTask.GetComponent<Collider>().enabled = false;

                }

                if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2)
                {
                    //esce dialogo " hai completato il task" & diventa verde 
                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_diff_completed, transform.position, Quaternion.identity);
                    dialogue_completato = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                    fine_dialogo_completato = true;

                    if (QuestManager.questManager.CheckEverythingDone())
                    {
                        //inizio_task = 3;
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(FinishedAllTasks, transform.position, Quaternion.identity);
                        inizio_task = 4;
                        dialogue_finishedAllTasks = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                        fine_dialogo_finishedAllTasks = true;
                    }
                    //nonCompletedYet = false;
                }
            } else
            {
                //dialogo task caffè non fatta
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_prima_il_caffe, transform.position, Quaternion.identity);
                dialogue_prima_il_caffe = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_prima_il_caffe = true;
            }

        }

        SetQuestMarker();
        
    }
}
