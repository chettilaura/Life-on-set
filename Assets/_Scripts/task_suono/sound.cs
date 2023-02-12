using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{  
    [SerializeField] private GameObject registration_bar;

    private GameObject dialogueBoxClone;
    Coroutine co;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 12){
            co = StartCoroutine(registration(other));
        }

    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == 12){
            StopCoroutine(co);

            Destroy(dialogueBoxClone, 0.5f);
        }
    }

    IEnumerator registration(Collider collider){
        dialogueBoxClone = (GameObject)GameObject.Instantiate(registration_bar, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);

        Destroy(collider.gameObject);
        Destroy(dialogueBoxClone, 4f);
        QuestManager.questManager.currentQuest.questObjectiveCount++;
        if(QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement)
        {
            QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
        }
    }
}
