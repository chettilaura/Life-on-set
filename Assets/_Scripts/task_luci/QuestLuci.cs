using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class QuestLuci : QuestNPC
{
    public GameObject Player;
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
    public GameObject dialoguebox_finishedAllTasks;
    public Animator Animations;

    private bool nonCompletedYet = true; //questa variabile diventa true quando torna dal NPC ma non ha ancora raccolto tutti i suoni 
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto

    //movimenti camera dialoghi 
    public CinemachineVirtualCamera camera_dialoghi; //camera per i dialoghi 
    public DialogueScript dialogue_iniziale;
    public DialogueScript dialogue_inattesa; 
    public DialogueScript dialogue_ricevuto;
    public DialogueScript dialogue_completato;
    public DialogueScript dialogue_prima_il_caffe;
    public DialogueScript dialogue_finishedAllTasks;
    
    private bool fine_dialogo_iniziale = false;
    private bool fine_dialogo_inattesa = false;
    private bool fine_dialogo_completato = false;
    private bool fine_dialogo_prima_il_caffe = false;
    private bool fine_dialogo_finishedAllTasks = false;

      //check che per evitare che premendo E ricominci il dialogo mentre sta parlando NPC 
    private bool gia_fatto_iniziale = false;
    private bool gia_fatto_completato = false;
    private bool gia_fatto_prima_il_caffe = false;
    private bool gia_fatto_finishedAllTasks = false;
    private bool gia_fatto_inattesa = false;    

    private bool gia_fatto_canvas = false;



    void Update()
    {
        //6 movimenti di camera dei 6 dialoghi 

        if (fine_dialogo_iniziale == true){
            if(dialogue_iniziale.fine_dialogo == true && dialogue_iniziale != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_iniziale = false;  
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                Animations.SetBool("talking", false);
                gia_fatto_iniziale = false;
            }
        }


        if (fine_dialogo_inattesa == true){
            if(dialogue_inattesa.fine_dialogo == true && dialogue_inattesa != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_inattesa = false;
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                Animations.SetBool("talking", false);
                gia_fatto_inattesa = false;
            }
        }

        if (fine_dialogo_completato == true){
            if(dialogue_completato.fine_dialogo == true && dialogue_completato != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_completato = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                Animations.SetBool("talking", false);
                gia_fatto_completato = false;
            }
        }

        if(fine_dialogo_prima_il_caffe == true){
            if(dialogue_prima_il_caffe.fine_dialogo == true && dialogue_prima_il_caffe != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_prima_il_caffe = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                Animations.SetBool("talking", false);
                gia_fatto_prima_il_caffe = false;
            }
        }

        if(fine_dialogo_finishedAllTasks == true){
            if(dialogue_finishedAllTasks.fine_dialogo == true && dialogue_finishedAllTasks != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_finishedAllTasks = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                Animations.SetBool("talking", false);
                gia_fatto_finishedAllTasks  = false;
            }
        }

         //istanzia il primo dialogo di partenza se è stato premuto spazio dopo aver visto la spiegazione
         if( inizio_task == 1 && gia_fatto_iniziale == false){
            if (Input.GetKeyDown(KeyCode.Mouse0)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci, transform.position, Quaternion.identity);
                dialogue_iniziale = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_iniziale = true; 
                inizio_task = 2;
                gia_fatto_iniziale = true;
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

            if (QuestManager.questManager.FirstTaskDone)
            {
                if (inizio_task == 0 && gia_fatto_canvas==false)
                {
                    Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false; //blocco i movimenti del player per il dialogo 
                    camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                    spiegazione_canvas = (GameObject)GameObject.Instantiate(infoLuci, transform.position, Quaternion.identity);
                    inizio_task = 1;
                    Animations.SetBool("talking", true);
                    gia_fatto_canvas = true;

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
                    if (nonCompletedYet == true && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.ACCEPTED && inizio_task == 2 && gia_fatto_inattesa==false)
                    {
                        Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false; //blocco i movimenti del player per il dialogo 
                         camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                        //esce dialogo "non hai ancora completato il task"
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci_inProgress, transform.position, Quaternion.identity);
                        dialogue_inattesa = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                        fine_dialogo_inattesa = true;
                        Animations.SetBool("talking", true);
                        gia_fatto_inattesa = true;
                    }

                }
                else
                {
                    startTask.GetComponent<Collider>().enabled = false;
                    Light.GetComponent<ambient_light>().enabled = false;
                    LightGun.GetComponent<LightGun>().enabled = false;
                    Camera.SetActive(false);

                    //si avvicina all'NPC premendo E e ha appena finito questa 
                    if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2 && gia_fatto_completato==false) //se quest comparse è sengnata come fatta
                    {
                        Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false; //blocco i movimenti del player per il dialogo 
                        camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                        Animations.SetBool("talking", true);

                        //esce dialogo " hai completato il task" 
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci_completed, transform.position, Quaternion.identity);
                        dialogue_completato = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                        fine_dialogo_completato = true;
                        nonCompletedYet = false;
                        gia_fatto_completato = true;
                        if (QuestManager.questManager.CheckEverythingDone() && gia_fatto_finishedAllTasks == false)
                        {
                            
                            dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_finishedAllTasks, transform.position, Quaternion.identity);
                            inizio_task = 4;
                            dialogue_finishedAllTasks = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                            fine_dialogo_finishedAllTasks = true;
                            gia_fatto_finishedAllTasks = true;
                        }
                    }



                }
            } else if (gia_fatto_prima_il_caffe == false)
            {
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false; //blocco i movimenti del player per il dialogo 
                camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                Animations.SetBool("talking", true);
                //dialogo da far uscire quando non ha ancora fatto task caffè
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_prima_il_caffe, transform.position, Quaternion.identity);
                dialogue_prima_il_caffe = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_prima_il_caffe = true;
                gia_fatto_prima_il_caffe = true;
            }

        }
        SetQuestMarker();

        

    }
}
