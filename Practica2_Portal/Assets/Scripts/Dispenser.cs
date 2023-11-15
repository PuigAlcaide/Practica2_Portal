using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : TriggeredObject
{
    public GameObject cube;

    public override void Trigger()
    {
        Debug.Log("Dispense");
        Instantiate(cube, gameObject.transform.position,Quaternion.identity);
    }
}
