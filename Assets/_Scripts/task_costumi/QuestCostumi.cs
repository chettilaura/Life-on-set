using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class QuestCostumi : QuestNPC
{
    public GameObject startTask; //canvas 2d task
    private GameObject dialogueBoxClone;
    private GameObject spiegazione_canvas;
    public GameObject infoCosumista;
    public GameObject FinishedAllTasks;
    public GameObject Player;
    public GameObject Manichino;
    public GameObject TutaAstronauta;
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto, 3-> finito
    public Animator Animations;


    //5 dialoghi 
    public GameObject dialoguebox_costumi_iniziale;
    public GameObject dialoguebox_costumi_completed;
    public GameObject dialoguebox_prima_il_caffe;
    public GameObject dialoguebox_finishedAllTasks;

    //movimento camera dialoghi 
    public CinemachineVirtualCamera camera_dialoghi; //camera per i dialoghi
    public CinemachineVirtualCamera camera_astronauta; //camera per inqaudrare astronauta
    public DialogueScript dialogue_iniziale;
    public DialogueScript dialogue_completato;
    public DialogueScript dialogue_prima_il_caffe;
    public DialogueScript dialogue_finishedAllTasks;
    
    private bool fine_dialogo_iniziale = false;
    private bool fine_dialogo_completato = false;
    private bool fine_dialogo_prima_il_caffe = false;
    private bool fine_dialogo_finishedAllTasks = false;

    //inquadratura su manichino dopo 5 secondi 
    private bool fine_primo_piano_manichino = false;
    private bool attivo_contatore = false;
    private float timer;
    private float soglia = 5;

     //check che per evitare che premendo E ricominci il dialogo mentre sta parlando NPC 
    private bool gia_fatto_iniziale = false;
    private bool gia_fatto_completato = false;
    private bool gia_fatto_prima_il_caffe = false;
    private bool gia_fatto_finishedAllTasks = false;
    private bool gia_fatto_canvas = false;


    void Update()
    {     //4 movimenti di camera dei 4 dialoghi 
        if(attivo_contatore == true){
            timer += Time.deltaTime;
            if(timer > soglia){
                camera_astronauta.Priority = camera_astronauta.Priority - 20; 
                Debug.Log("camera astronauta diasattivata");
                attivo_contatore = false;
                
            }
        }
         
        if (fine_dialogo_iniziale == true){
            if(dialogue_iniziale.fine_dialogo == true && dialogue_iniziale != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_iniziale = false;  
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                gia_fatto_iniziale = false;
                Animations.SetBool("talking", false);
            }
        }

        if (fine_dialogo_completato == true && fine_primo_piano_manichino == false ){
            if(dialogue_completato.fine_dialogo == true && dialogue_completato != null){
                //camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                Debug.Log("camera astronauta partita");
                camera_astronauta.Priority = camera_astronauta.Priority + 20;
                fine_primo_piano_manichino = true;
                fine_dialogo_completato = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true; 
                attivo_contatore = true;
                gia_fatto_completato = false;
                Animations.SetBool("talking", false);
            }
        }
        if(fine_dialogo_prima_il_caffe == true){
            if(dialogue_prima_il_caffe.fine_dialogo == true && dialogue_prima_il_caffe != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_prima_il_caffe = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true; 
                Debug.Log("player torna a muoversi");
                gia_fatto_prima_il_caffe= false;
                Animations.SetBool("talking", false);
            }
        }

        if(fine_dialogo_finishedAllTasks == true){
            if(dialogue_finishedAllTasks.fine_dialogo == true && dialogue_finishedAllTasks != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_finishedAllTasks = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true; 
                gia_fatto_finishedAllTasks = false;
                Animations.SetBool("talking", false);
            }
        }

        if(fine_primo_piano_manichino == true && fine_dialogo_completato == false ){
           camera_dialoghi.Priority = camera_dialoghi.Priority -10; 
            fine_primo_piano_manichino = false;
        
        }




        //istanzia la tuta da astronauta
        if (QuestManager.questManager.questList[2].progress == Quest.QuestProgress.COMPLETE)
        {
            Manichino.SetActive(false);
            TutaAstronauta.SetActive(true);
        }



        //istanzia primo dialogo post spiegazione (non ha check su trigger e E perché va fatto obbligatoriamente post spiegazione)
         if( inizio_task == 1 && gia_fatto_iniziale == false){
            if (Input.GetKeyDown(KeyCode.Mouse0)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_costumi_iniziale, transform.position, Quaternion.identity);
                dialogue_iniziale = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_iniziale = true; 
                inizio_task = 2;
                gia_fatto_iniziale = true;
            }
         }



        /*
        //istanzia il dialogo super finale

        if (inizio_task == 3)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                dialogueBoxClone = (GameObject)GameObject.Instantiate(FinishedAllTasks, transform.position, Quaternion.identity);
                inizio_task = 4;
            }
        }
        */

         //check principale: entro nel trigger & premo E + sei fermo 
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && Player.GetComponent<Cinemachine.Examples.CharacterMovement>().speed<0.001f)
        {
            //NPC si gira verso il player
            LookAtPlayer(Player.transform);
            //controllo prima task caffe completata
            if (QuestManager.questManager.FirstTaskDone)
             {

                if (inizio_task == 0 && gia_fatto_canvas==false)
                {
                    Animations.SetBool("talking", true);
                    //blocco il movimento del player durante dialogo 
                    Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false; 
                    camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                    spiegazione_canvas = (GameObject)GameObject.Instantiate(infoCosumista, transform.position, Quaternion.identity);
                    inizio_task = 1;
                    gia_fatto_canvas = true;

                }

                
                
                QuestManager.questManager.QuestRequest(this); //mette questa come quest corrente

                
                if (QuestManager.questManager.currentQuest.id == 2)
                    startTask.GetComponent<Collider>().enabled = true; //abilita canvas costumi
                else
                    startTask.GetComponent<Collider>().enabled = false; //disabilita canvas costumi 


                 if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2 && gia_fatto_completato == false)
                {
                    Animations.SetBool("talking", true);
                    //blocco il movimento del player durante dialogo 
                    Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false; 
                    camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                    //esce dialogo " hai completato il task" & diventa verde 
                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_costumi_completed, transform.position, Quaternion.identity);
                    dialogue_completato = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                    fine_dialogo_completato = true;
                    gia_fatto_completato = true;
                    //co = StartCoroutine(astronauta(camera_astronauta));

                    if (QuestManager.questManager.CheckEverythingDone() && gia_fatto_finishedAllTasks == false)
                    {
                        
                        //inizio_task = 3;
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(FinishedAllTasks, transform.position, Quaternion.identity);
                        inizio_task = 4;
                        dialogue_finishedAllTasks = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                        fine_dialogo_finishedAllTasks = true;
                        gia_fatto_finishedAllTasks = true;

                    }
                }
                    

            } else if(gia_fatto_prima_il_caffe == false)  //se prima task caffe non è ancora completata e se NON ha il caffè in consegna allora deve prima fare caffè
            {
                Animations.SetBool("talking", true);
                //blocco il movimento del player durante dialogo 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false; 
                camera_dialoghi.Priority = camera_dialoghi.Priority +10;
               dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_prima_il_caffe, transform.position, Quaternion.identity);
               dialogue_prima_il_caffe = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
               fine_dialogo_prima_il_caffe = true;
               gia_fatto_prima_il_caffe = true;
                
            } 


        } //fine intrigger + E

    

        SetQuestMarker();
    }

}
