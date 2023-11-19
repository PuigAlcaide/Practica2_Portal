using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("Lava");
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<Player>().Die();
        } 
    }
}
