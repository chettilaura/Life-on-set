using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;

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
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto, 3->Finito
    public Animator Animations;
    public GameObject Caffe;

     //movimento camera dialoghi 
    public DialogueScript dialogue_iniziale;
    public DialogueScript dialogue_inattesa; 
    public DialogueScript dialogue_ricevuto;
    public DialogueScript dialogue_completato;
    public CinemachineVirtualCamera camera_dialoghi; //camera per i dialoghi 
    private bool fine_dialogo_iniziale = false;
    private bool fine_dialogo_ricevuto = false;
    private bool fine_dialogo_inattesa = false;
    private bool fine_dialogo_completato = false;

    //animazione vassoio
    public Animator coffeeAnimator;
    public GameObject Vassoio;
    public List<GameObject> tazzine;
    private bool tazzine_istanziate = false;


     //check che per evitare che premendo E ricominci il dialogo mentre sta parlando NPC 
    private bool gia_fatto_iniziale = false;
    private bool gia_fatto_completato = false;
    private bool gia_fatto_inattesa = false;    
    private bool gia_fatto_caffe_ricevuto = false;

    private bool gia_fatto_canvas = false;





    void Update()
    {

        //dopo essere andato alla macchinetta del caffe compaiono le tazzine
        if (Player.GetComponent<task_caffe>().CaffePreso && tazzine_istanziate==false)
        {
            for(int i=0; i<tazzine.Count; i++)
            {
                tazzine[i].SetActive(true);
            }
            tazzine_istanziate = true;
        }

        

        if (fine_dialogo_iniziale == true){
            if(dialogue_iniziale.fine_dialogo == true && dialogue_iniziale != null){
                Animations.SetBool("talking", false);
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_iniziale = false;  
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                gia_fatto_iniziale = false;
            }
        }

        if (fine_dialogo_ricevuto == true){
            if(dialogue_ricevuto.fine_dialogo == true && dialogue_ricevuto != null){
                Animations.SetBool("talking", false);
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_ricevuto = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                gia_fatto_caffe_ricevuto = false;
            }
        }  

        if (fine_dialogo_inattesa == true){
            if(dialogue_inattesa.fine_dialogo == true && dialogue_inattesa != null){
                Animations.SetBool("talking", false);
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_inattesa = false;
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;  
                gia_fatto_inattesa = false;
            }
        }

        if (fine_dialogo_completato == true){
            if(dialogue_completato.fine_dialogo == true && dialogue_completato != null){
                Animations.SetBool("talking", false);
                camera_dialoghi.Priority = camera_dialoghi.Priority -10;
                fine_dialogo_completato = false; 
                Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = true;
                gia_fatto_completato = false; 
            }
        }






        //se si preme spazio dopo la spiegazione parte il primo dialogo 
        if(inizio_task == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && gia_fatto_iniziale == false){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe, transform.position, Quaternion.identity);
                inizio_task = 2; 
                fine_dialogo_iniziale = true; 
                dialogue_iniziale = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                gia_fatto_iniziale = true;
            }
        
        }

        //se si preme E vicino al regista 
        //l'ultima condizione è per obbligarlo a fermarsi prima di parlare (se no si blocca in posizioni strane)
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && Player.GetComponent<Cinemachine.Examples.CharacterMovement>().speed<0.001f)
        {
            //NPC si gira verso il player
            LookAtPlayer(Player.transform);
            


            //se è la prima volta che si preme E vicino al regista mostra spiegazione regista + Compare vassoio
            if (inizio_task == 0 && QuestManager.questManager.questList[0].progress != Quest.QuestProgress.DONE && gia_fatto_canvas==false)
            {
                 Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false;  //blocco il movimento del player durante dialogo 
                camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                Animations.SetBool("talking", true);
                spiegazione_canvas = (GameObject)GameObject.Instantiate(spiegazione_canvas, transform.position, Quaternion.identity);
                inizio_task = 1;
                //ATTIVA VASSOIO E CAMMINATA RELATIVA
                coffeeAnimator.SetBool("coffeeTask", true);
                Vassoio.SetActive(true);
                gia_fatto_canvas = true;
            }

            QuestManager.questManager.QuestRequest(this); // assegna come corrente la task caffe

            //se la current quest è la task caffe abilita macchinetta caffe
            if (QuestManager.questManager.currentQuest.id == 0)
                coffeeMachine.GetComponent<Collider>().enabled = true;
            else
                coffeeMachine.GetComponent<Collider>().enabled = false;


             //si avvicina all'NPC premendo E ma non ha ancora finito questa task
                if (!Player.GetComponent<task_caffe>().CaffePreso && QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.ACCEPTED && inizio_task == 2 && QuestManager.questManager.currentQuest.id == 0 && gia_fatto_inattesa==false)
                {
                     Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false;  //blocco il movimento del player durante dialogo 
                     camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                    Animations.SetBool("talking", true);
                    //esce dialogo "non hai ancora completato il task"
                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_inAttesa, transform.position, Quaternion.identity);
                    dialogue_inattesa = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                    fine_dialogo_inattesa = true;
                    gia_fatto_inattesa = true;
                }

            //si avvicina all'NPC premendo E mentre è attiva la task caffe e non ha ancora ricevuto il caffe
            if(Player.GetComponent<task_caffe>().CaffePreso && !_coffeeReceived && gia_fatto_caffe_ricevuto==false){
                 Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false;  //blocco il movimento del player durante dialogo 
                camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                Animations.SetBool("talking", true);
                QuestManager.questManager.currentQuest.questObjectiveCount++;
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_ricevuto , transform.position, Quaternion.identity);
                dialogue_ricevuto = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_ricevuto = true;
                gia_fatto_caffe_ricevuto = true;
                //disattivo tazzina sopra la testa
                Caffe.SetActive(false);
                //TOLGO TAZZINA DA VASSOIO
                tazzine[0].SetActive(false);


                if(QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement){
                    QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
                    Debug.Log("disattiva vassoio");
                    //DISATTIVA VASSOIO E CAMMINATA RELATIVA
                    coffeeAnimator.SetBool("coffeeTask", false);
                    Vassoio.SetActive(false);
                }
                _coffeeReceived = true;
            }

             //se ha portato tutti e tre i caffè ha finito 
            if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2 && gia_fatto_completato==false){
                 Player.GetComponent<Cinemachine.Examples.CharacterMovement>().enabled = false;  //blocco il movimento del player durante dialogo 
                 camera_dialoghi.Priority = camera_dialoghi.Priority +10;
                Animations.SetBool("talking", true);
                Debug.Log("questObjectiveCount==questObjectiveRequirement");
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_caffe_completed , transform.position, Quaternion.identity);
                dialogue_completato = ((dialogueBoxClone.transform.Find("Canvas_dialogue")?.gameObject).transform.Find("dialogueBox")?.gameObject).GetComponent<DialogueScript>();
                fine_dialogo_completato = true;
                QuestManager.questManager.FirstTaskDone = true;
                gia_fatto_completato = true;

            }

        }

        SetQuestMarker(); //check sui quest markers

    } //update



}


