using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFinderScript1 : MonoBehaviour
{
    public bool Active = false;

   [SerializeField] Transform tfLight;

 
 void  Start ()
 {
     // find the revealing light named "Sun":
     // var goLight= GameObject.Find("Sun");
     // if (goLight) tfLight = goLight.transform;
 }

    

    private void  Update ()
 {
     if (tfLight)
     {
         GetComponent<Renderer>().material.SetVector("_LightPos", tfLight.position);
         GetComponent<Renderer>().material.SetVector("_LightDir", tfLight.forward);
     }
 }


  
}