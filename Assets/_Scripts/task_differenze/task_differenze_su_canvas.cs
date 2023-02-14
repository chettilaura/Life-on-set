using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class task_differenze_su_canvas : MonoBehaviour
{
    public GameObject immagini;
    public GameObject canvas_differenze;
    public GameObject endgame_text;
    
    private bool completed=false;
    private bool[] pressedButtons = {false, false, false, false, false, false};
    
    
    // Start is called before the first frame update
    public void updatediff(){
        QuestManager.questManager.currentQuest.questObjectiveCount++;
    }

    public void diff1()
    {
        if (pressedButtons[0] == false)
        {
            pressedButtons[0] = true;
            updatediff();
        }
    }

    public void diff2()
    {
        if (pressedButtons[1] == false)
        {
            pressedButtons[1] = true;
            updatediff();
        }
    }

    public void diff3()
    {
        if (pressedButtons[2] == false)
        {
            pressedButtons[2] = true;
            updatediff();
        }
    }

    public void diff4()
    {
        if (pressedButtons[3] == false)
        {
            pressedButtons[3] = true;
            updatediff();
        }
    }

    public void diff5()
    {
        if (pressedButtons[4] == false)
        {
            pressedButtons[4] = true;
            updatediff();
        }
    }

    public void diff6()
    {
        if (pressedButtons[5] == false)
        {
            pressedButtons[5] = true;
            updatediff();
        }
    }


    void Update(){
        if (completed==false && QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement)
        {
            Debug.Log("Hai trovato tutte le differenze!");
            //cancellla le due immagini 
            immagini.SetActive(false);
            

            
            //compare scritta di fine gioco
            //endgame_text.SetActive(true);
            //Invoke("end_text", 5);
            QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
            completed=true;

        }
    } 

    public void end_text(){
        
        endgame_text.SetActive(false);
        Debug.Log("end_text");
        canvas_differenze.SetActive(false);
    }
}