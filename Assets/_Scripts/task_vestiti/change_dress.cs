using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class change_dress : MonoBehaviour
{
    [SerializeField] Image imm_1;
    //[SerializeField] Image imm_2;
    //[SerializeField] Image imm_3;
    Image imm_current;

    public void change_image()
    {
        imm_current = Image.Instantiate(imm_1, imm_1.transform);
    }
}
