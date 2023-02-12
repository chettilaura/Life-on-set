using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task_caffè : MonoBehaviour
{
    [SerializeField] private GameObject coffee_bar;

    private GameObject dialogueBoxClone;
    public Coroutine co;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 18)
        {
            co = StartCoroutine(CoffeePreparation(other));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 18)
        {
            StopCoroutine(co);

            Destroy(dialogueBoxClone, 0.5f);
        }
    }

    IEnumerator CoffeePreparation(Collider collider)
    {
        dialogueBoxClone = (GameObject)GameObject.Instantiate(coffee_bar, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);

        //Destroy(collider.gameObject);
        collider.enabled = false;
        Destroy(dialogueBoxClone, 4f);
        QuestManager.questManager.currentQuest.questObjectiveCount = 6;
        QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;

    }
}
