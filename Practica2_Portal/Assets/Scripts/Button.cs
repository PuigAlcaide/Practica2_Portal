using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public TriggeredObject triggerObject;

    private void Start()
    {
        Debug.Log("AAA");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("Trigger");
        triggerObject.Trigger(); 
    }
}
