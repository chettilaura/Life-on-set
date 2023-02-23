using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class QuestSuoni : QuestNPC
{
    public GameObject startTask;
    public GameObject Player;
    //suoni 
    public GameObject motor_engine_sound;
    public GameObject talking_people_sound;
    public GameObject rain_sound;
    public GameObject leaves_sound;
    private GameObject spiegazione_canvas;
    private GameObject dialogueBoxClone;
    public GameObject infoFonico;
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto
    private bool nonCompletedYet = true; 
    private bool _coffeeReceived = false; //questa variabile diventa true quando il player ha consegnato il caffè
    public Animator Animations;
    public GameObject suonoAmbienteGioco;
    public GameObject Caffe;


    //6 dialoghi
    public GameObject dialoguebox_sound; //questo è il dialogo dell'NPC che da la task per la prima volta
    public GameObject dialoguebox_sound_inProgress; //questo è il dialogo dell'NPC che ripete la task quando il player ci ritorna 
    public GameObject dialoguebox_sound_completed; //questo è il dialogo dell'NPC che dice di aver concluso la task 
    public GameObject dialoguebox_prima_il_caffe;
    public GameObject dialoguebox_caffe_ricevuto; 
    public GameObject dialoguebox_finishedAllTasks;

    //movimento camera dialoghi 
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


    //animazione vassoio
    public Animator coffeeAnimator;
    public GameObject Vassoio;
    public List<GameObject> tazzine;


     //check che per evitare che premendo E ricominci il dialogo mentre sta parlando NPC 
    private bool gia_fatto_iniziale = false;
    private bool gia_fatto_completato = false;
    private bool gia_fatto_prima_il_caffe = false;
    private bool gia_fatto_finishedAllTasks = false;
    private bool gia_fatto_inattesa = false;    
    private bool gia_fatto_caffe_ricevuto = false;

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

        if (fine_dialogo_caffe_ricevuto == true){
            if(dialogue_ricevuto.fine_dialogo == true && dialogue_ricevuto != null){
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_caffe_ricevuto = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                Animations.SetBool("talking", false);
                gia_fatto_caffe_ricevuto = false;
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
                gia_fatto_finishedAllTasks = false;
            }
        }



        //istanzia il primo dialogo di partenza se è stato premuto spazio dopo aver visto la spiegazione
        if( inizio_task == 1){
            if (Input.GetKeyDown(KeyCode.Mouse0) && gia_fatto_iniziale == false){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound, transform.position, Quaternion.identity);
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
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_finishedAllTasks, transform.position, Quaternion.identity);
                inizio_task = 4;
            }
        }
        */




        //check principale: entro nel trigger & premo E + blocco movimenti player
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && Player.GetComponent<Cinemachine.Examples.CharacterMovement>().speed<0.001f )
        {
            //NPC si gira verso il player
            LookAtPlayer(Player.transform);

                    //controllo prima task caffe completata
                    if (QuestManager.questManager.FirstTaskDone)
                    {
                        QuestManager.questManager.QuestRequest(this); //qui assegna come corrente la quest che ha questo script

                        //se la quest attiva è quella dei suoni 
                        if (QuestManager.questManager.currentQuest.id == 4)
                        {
                            startTask.GetComponent<Collider>().enabled = true;
                            Player.GetComponent<sound>().enabled = true; //attivo lo script per la raccolta dei suoni
                                                                        //abbassa il volume del gioco 
                            suonoAmbienteGioco.GetComponent<AudioSource>().volume = 0.005f;


                            //instanzia la spiegazione
                            if (inizio_task == 0 && gia_fatto_canvas == false)
                            {
                                 Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false;  //blocco il movimento del player durante dialogo 
                                camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                                Animations.SetBool("talking", true);
                                spiegazione_canvas = (GameObject)GameObject.Instantiate(infoFonico, transform.position, Quaternion.identity);
                                inizio_task = 1;
                                gia_fatto_canvas = true;

                                //attiva i suoni nell'ambiente 
                                motor_engine_sound.SetActive(true);
                                talking_people_sound.SetActive(true);
                                rain_sound.SetActive(true);
                                leaves_sound.SetActive(true);
                            }

                            //si avvicina all'NPC premendo E ma non ha ancora finito questa task
                            if (nonCompletedYet == true && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.ACCEPTED && inizio_task == 2 && gia_fatto_inattesa==false)
                            {
                                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false;  //blocco il movimento del player durante dialogo 
                                camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                                Animations.SetBool("talking", true);
                                //esce dialogo "non hai ancora completato il task"
                                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound_inProgress, transform.position, Quaternion.identity);
                                dialogue_inattesa = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                                fine_dialogo_inattesa = true;
                                gia_fatto_inattesa = true;

                            }



                        }
                        //se la task corrente non è più quella dei suoni allora controlla se è appena finita 
                        else
                        {
                            //rialza il volume del gioco perchè non è più attivo il task dei suoni  
                            suonoAmbienteGioco.GetComponent<AudioSource>().volume = 0.1f;
                            //disattivo 
                            startTask.GetComponent<Collider>().enabled = false;
                            //disattivo lo script per la raccolta dei suoni perchè non è attivo il task dei suoni  
                            Player.GetComponent<sound>().enabled = false;



                            //si avvicina all'NPC premendo E e ha appena finito questa 
                            if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2 && gia_fatto_completato==false) //se quest suoni è sengnata come fatta
                            {
                                 Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false;  //blocco il movimento del player durante dialogo 
                                camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                                //esce dialogo " hai completato il task" 
                                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_sound_completed, transform.position, Quaternion.identity);
                                dialogue_completato = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                                fine_dialogo_completato = true;
                                nonCompletedYet = false;
                                Animations.SetBool("talking", true);
                                gia_fatto_completato = true;

                        //se oltre a questa task ha completato anche TUTTE le altre
                        if (QuestManager.questManager.CheckEverythingDone() && gia_fatto_finishedAllTasks == false)
                                {
                                    //inizio_task = 3;
                                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_finishedAllTasks, transform.position, Quaternion.identity);
                                    inizio_task = 4;
                                    dialogue_finishedAllTasks = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                                    fine_dialogo_finishedAllTasks = true;
                                    gia_fatto_finishedAllTasks = true;
                                }
                            }
                        }

                    //se prima task caffe non è ancora completata e se NON ha il caffè in consegna allora deve prima fare caffè         
                    } else if(!Player.GetComponent<task_caffe>().CaffePreso && gia_fatto_prima_il_caffe == false)
                    {
                         Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false;  //blocco il movimento del player durante dialogo 
                        camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                        //qui dialogo per dire che non ha ancora fatto task caffè
                        Debug.Log("Fai prima la task del caffè");
                        Animations.SetBool("talking", true);
                        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_prima_il_caffe, transform.position, Quaternion.identity);
                        dialogue_prima_il_caffe = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                        fine_dialogo_prima_il_caffe = true;
                        gia_fatto_prima_il_caffe = true;
                    }


            //si avvicina all'NPC premendo E ma è attiva la task caffe
            if (Player.GetComponent<task_caffe>().CaffePreso && !_coffeeReceived && gia_fatto_caffe_ricevuto == false)
            {
                 Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false;  //blocco il movimento del player durante dialogo 
                camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                QuestManager.questManager.currentQuest.questObjectiveCount++;
                Animations.SetBool("talking", true);
                //caffe consegnato 
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_ricevuto, transform.position, Quaternion.identity);
                 dialogue_ricevuto = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_caffe_ricevuto = true;
                _coffeeReceived = true;
                //disattivo tazzina sulla testa
                Caffe.SetActive(false);
                //TOLGO TAZZINA DA VASSOIO
                tazzine[2].SetActive(false);
                gia_fatto_caffe_ricevuto = true;

                //se questo era l'ultimo caffe da consegnare allora task caffe completata per intero 
                if (QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement){
                    QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
                      //DISATTIVA VASSOIO E CAMMINATA RELATIVA
                    coffeeAnimator.SetBool("coffeeTask", false);
                    Vassoio.SetActive(false);
                }
            }

        } //fine trigger & premo E 



        SetQuestMarker();   //controlla se la task è finita e se è finita mette il punto esclamativo

    

    }
}
