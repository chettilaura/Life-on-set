using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task_caffe : MonoBehaviour
{
    [SerializeField] private GameObject _coffee_bar;
    [SerializeField] private GameObject _coffeeMachine;

    private GameObject dialogueBoxClone;
    public Coroutine co;
    public bool CaffePreso = false;
    public bool GiaEntrato = false;
    public List<GameObject> Coffees;

    private void OnTriggerEnter(Collider other)
    {
        //quando player si avvicina alla macchinetta parte suono e animazione preparazione caffe
        if (other.gameObject.layer == 18 && !GiaEntrato)
        {
            co = StartCoroutine(CoffeePreparation(other));
            _coffeeMachine.GetComponent<AudioSource>().enabled = true;
            GiaEntrato = true;
        } 

    }

    private void OnTriggerExit(Collider other)
    {
        //quando player si allontana dalla macchinetta
        if (other.gameObject.layer == 18)
        {
            StopCoroutine(co);

            Destroy(dialogueBoxClone, 0.5f);
            _coffeeMachine.GetComponent<AudioSource>().enabled = false;
            GiaEntrato = false;
        }
    }


    IEnumerator CoffeePreparation(Collider collider)
    {
        dialogueBoxClone = (GameObject)GameObject.Instantiate(_coffee_bar, transform.position, Quaternion.identity);
        Debug.Log("mannaggia");

        yield return new WaitForSecondsRealtime(2);
        yield return new WaitForSecondsRealtime(2);
        yield return new WaitForSecondsRealtime(2);
        yield return new WaitForSecondsRealtime(2);
        yield return new WaitForSecondsRealtime(2);

        //Destroy(collider.gameObject);
        collider.enabled = false;
        
        Destroy(dialogueBoxClone, 3f);
        Debug.Log("Caffe distrutto");
        CaffePreso = true;
        for(int i=0; i<Coffees.Count; i++)
        {
            Coffees[i].SetActive(true);
        }
    }

}
