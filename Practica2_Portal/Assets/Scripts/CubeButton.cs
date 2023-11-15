using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeButton : Button
{
    override OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("Trigger");
        triggerObject.Trigger(); 
    }
}
