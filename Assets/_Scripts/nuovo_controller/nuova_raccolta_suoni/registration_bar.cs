using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class registration_bar : MonoBehaviour
{

    public int rec = 0;
    public int rec_max = 100;
    public int seconds;

    //public bool flag;

    public Image barra;
    

    void Start()
    {
        rec = 0;
        StartCoroutine(aggiornaemtno());
    }


    // Update is called once per frame
    void Update()
    {
        rec = 0;
     //StartCoroutine(aggiornaemtno());
     //GetCurrentFill();
    }

     void GetCurrentFill()
    {
        float percentuale = (float)rec / (float)rec_max;
        barra.fillAmount = percentuale;

    }

    IEnumerator aggiornaemtno() {
        rec = 0;

        yield return new WaitForSeconds(seconds);
        rec = 20;
        GetCurrentFill();
        
        yield return new WaitForSeconds(seconds);
        rec = 40;
        GetCurrentFill();
        
        yield return new WaitForSeconds(seconds);
        rec =  60;
        GetCurrentFill();
        
        yield return new WaitForSeconds(seconds);
        rec = 80;
        GetCurrentFill();
        
        yield return new WaitForSeconds(seconds);
        rec = 100;
        GetCurrentFill();
        

    }
}
