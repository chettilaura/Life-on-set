using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteractable : MonoBehaviour
{

    private Animator _animator;
    private GameObject dialogueBoxClone;


    [SerializeField] private GameObject dialogueBox_assegno_compito;
    [SerializeField] private GameObject dialogueBox_compitodacompletare;
    [SerializeField] private GameObject dialogueBox_compito_finito;
    [SerializeField] private GameObject _dialogue_ended;

    private GameObject _clone_dialogue_ended;

    //prova dialogo doppio

    //private RaccoltaSuoni player_raccolta;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }



    public void Interact(int num, int max)
    {
        Debug.Log("interazione con NPC");


        if (num == 0)
            dialogueBoxClone = (GameObject)GameObject.Instantiate(dialogueBox_assegno_compito, transform.position, Quaternion.identity);
        Debug.Log(transform.position);
        Debug.Log("istanziato assegnazione compito");
        _animator.SetBool("talk", true);

        if (num < max && num != 0)
            dialogueBoxClone = (GameObject)GameObject.Instantiate(dialogueBox_compitodacompletare, transform.position, Quaternion.identity);
        Debug.Log("compito da concludere");
        _animator.SetBool("talk", true);

        if (num == max)
            dialogueBoxClone = (GameObject)GameObject.Instantiate(dialogueBox_compito_finito, transform.position, Quaternion.identity);
        Debug.Log("compito concluso");
        _animator.SetBool("talk", true);

    }


    public void StopInteract()
    {
        Debug.Log("stop interazione con NPC");
        _animator.SetBool("talk", false);

        //chiudo dialogue Box
        Destroy(dialogueBoxClone);
        Debug.Log("distrutto dialogBox");

        //confermo chiusura dialogo 
        //mostro a schermo per 2 secondi la scritta dove si dice di premere E per chiudere il dialogo
        _clone_dialogue_ended = (GameObject)GameObject.Instantiate(_dialogue_ended, transform.position, Quaternion.identity);
        Destroy(_clone_dialogue_ended, 2f);


    }



}
