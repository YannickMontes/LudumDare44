using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves_Settings : MonoBehaviour
{

     float pannerSpeed ;
     float vertexOffset ;

    Renderer meshRenderer;
    Material instancedMaterial; 


    // Start is called before the first frame update
    void Start()
    {
        pannerSpeed = Random.Range(0.1f, 1.5f);
        vertexOffset = Random.Range(0.0f, 1.0f);
        meshRenderer = gameObject.GetComponent<Renderer>();
        instancedMaterial = meshRenderer.material;
        instancedMaterial.SetFloat("_OffsetValue", vertexOffset);
        instancedMaterial.SetFloat("_Speed", pannerSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
