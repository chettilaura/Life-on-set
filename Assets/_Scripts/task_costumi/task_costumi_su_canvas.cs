using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class task_costumi_su_canvas : MonoBehaviour
{
    public List<GameObject> head;
    public List<GameObject> body;
    public List<GameObject> legs;
    private int contatore_testa = 1;
    private int contatore_body = 2;
    private int contatore_legs = 0;
    public GameObject canvas_task;
    public GameObject error_text;

    public void left_head_arrow()
    {
        head[contatore_testa].SetActive(false);

        if( contatore_testa == 0)
        {
            contatore_testa = 2;
        } else
        {
            contatore_testa--;
        }
        head[contatore_testa].SetActive(true);
    }

    public void right_head_arrow()
    {
        head[contatore_testa].SetActive(false);
        Debug.Log(contatore_testa);

        if(contatore_testa == 2)
        {
            contatore_testa = 0;
        }else
        {
            contatore_testa++;
        }
        head[contatore_testa].SetActive(true);
    }

    public void left_body_arrow()
    {
        body[contatore_body].SetActive(false);

        if (contatore_body == 0)
        {
            contatore_body = 2;
        }
        else
        {
            contatore_body--;
        }
        body[contatore_body].SetActive(true);
    }

    public void right_body_arrow()
    {
        body[contatore_body].SetActive(false);

        if (contatore_body == 2)
        {
            contatore_body = 0;
        }
        else
        {
            contatore_body++;
        }
        body[contatore_body].SetActive(true);
    }

    public void left_legs_arrow()
    {
        legs[contatore_legs].SetActive(false);

        if (contatore_legs == 0)
        {
            contatore_legs = 2;
        }
        else
        {
            contatore_legs--;
        }
        legs[contatore_legs].SetActive(true);
    }

    public void right_legs_arrow()
    {
        legs[contatore_legs].SetActive(false);

        if (contatore_legs == 2)
        {
            contatore_legs = 0;
        }
        else
        {
            contatore_legs++;
        }
        legs[contatore_legs].SetActive(true);
    }

    public void correct_set()
    {
        //controllo austronautaaaaagnese
        if(contatore_testa == 1 && contatore_body == 1 && contatore_legs == 1)
        {
            canvas_task.SetActive(false);
            QuestManager.questManager.currentQuest.questObjectiveCount++;
            QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
        }
        else
        {
            error_text.SetActive(true);
        }
        
    }
}
