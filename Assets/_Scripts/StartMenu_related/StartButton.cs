using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;
    [SerializeField] private Animator _ciackOpen;
    [SerializeField] private GameObject _container;
    public AudioSource Clap;

    void Start()
    {   
        _container.GetComponent<Animator>();
    }

    void Update(){
        if(isPressed){
            _ciackOpen.Play("startGame");
            StartCoroutine(PlayClap());
            StartCoroutine(TimeDelay());
            //Debug.Log(_ciackOpen.GetBool("startGame"));
        }
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(1);
        Scene_Loader.Load(Scene_Loader.Scene.Esterno);
    }


    public IEnumerator PlayClap()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        Clap.Play();
    }

  
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      isPressed = false;
    }
}

   
