using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLuci : QuestNPC
{
    public GameObject startTask;
    public GameObject Camera;
    public GameObject Light;
    public GameObject LightGun;
    public GameObject dialoguebox_luci;
    private GameObject dialogueBoxClone;
    public GameObject infoLuci;
    private bool info = false;
    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E) && info == false)
        {
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest.id == 3)
            {
                dialogueBoxClone = (GameObject)GameObject.Instantiate(infoLuci, transform.position, Quaternion.identity);
                info = true;
                //abilita il collider del relativo oggetto per iniziare la task (es. sedia regista per task differenze)
                startTask.GetComponent<Collider>().enabled = true;
                Light.GetComponent<ambient_light>().enabled = true;
                Camera.SetActive(true);
                LightGun.GetComponent<LightGun>().enabled = true ;
                //start_task_luci.SetActive(true);

            } else
            {
                startTask.GetComponent<Collider>().enabled = false;
                Light.GetComponent<ambient_light>().enabled = false;
                LightGun.GetComponent<LightGun>().enabled = false;
                Camera.SetActive(false);
            }

            if(info == true){
                if ( Input.GetKeyDown(KeyCode.Space)){
                Destroy(dialogueBoxClone);

                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_luci, transform.position, Quaternion.identity);
            }
        }
        }
        SetQuestMarker();
    }
}
