using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLuci : QuestNPC
{
    public GameObject startTask;
    public GameObject Camera;
    public GameObject Light;
    public GameObject LightGun;
    void Update()
    {
        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            QuestManager.questManager.QuestRequest(this);
            if (QuestManager.questManager.currentQuest != null)
            {
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


        }
        SetQuestMarker();
    }
}
