using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves_Settings : MonoBehaviour
{

    float pannerSpeed = Random.Range(0.0f, 1.0f);
    float vertexOffset = Random.Range(0.0f, 1.0f);

    Renderer meshRenderer;
    Material instancedMaterial; 


    // Start is called before the first frame update
    void Start()
    {

        meshRenderer = gameObject.GetComponent<Renderer>();
        instancedMaterial = meshRenderer.material; 
        //instancedMaterial.SetFloat ("_OffsetValue")

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
