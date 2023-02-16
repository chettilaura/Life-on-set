using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    
    public List<GameObject> lista_tutorial;
    public int contatore = 0;


    public void RightArrow()
    {
        
        lista_tutorial[contatore].SetActive(false);
        if(contatore == lista_tutorial.Count - 1)
        {
            contatore = 0;
        } else
        {
            contatore++;
        }
        lista_tutorial[contatore].SetActive(true);
    }

    public void LeftArrow()
    {
        lista_tutorial[contatore].SetActive(false);
        if (contatore == 0)
        {
            contatore = lista_tutorial.Count - 1;
        }
        else
        {
            contatore--;
        }
        lista_tutorial[contatore].SetActive(true);
    }






}
