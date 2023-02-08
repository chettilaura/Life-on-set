using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class change_dress : MonoBehaviour
{
    [SerializeField] Image imm_1;
    [SerializeField] Image imm_2;
    [SerializeField] Image imm_3;
    Image imm_current;
    // Start is called before the first frame update
    void Start()
    {
        imm_current = imm_1;
        imm_current.maskable = false;
    }

    // Update is called once per frame
    void Update()
    {
        imm_current.maskable = false;
    }

    public void change_image()
    {
        if (imm_current == imm_1)
        {
            imm_current = imm_2;
        }

        if(imm_current == imm_2)
        {
            imm_current = imm_3;
        }

        if(imm_current == imm_3)
        {
            imm_current = imm_1;
        }
    }
}
