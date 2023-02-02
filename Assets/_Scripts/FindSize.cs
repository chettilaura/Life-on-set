using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float scalex = transform.lossyScale.x;
        float sizex = scalex;
        float scalez = transform.lossyScale.z;
        float sizez = scalez;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3 size = mesh.bounds.size;
        float length = size.z*sizez;
        float width = size.x*sizex;
        Debug.Log("Mesh length: " + length + " meters");
        Debug.Log("Mesh width: " + width + " meters");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
