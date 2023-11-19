using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{

    public static Player instance;
    public Transform mainCamera;
    public Transform gunCamera;


    private void Awake()
    {
        #region Singleton
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        #endregion

        mainCamera = transform.Find("FirstPersonCharacter");
        gunCamera = transform.Find("GunCamera");
    }

     public void Die()
    { 
        Debug.Log("AAAA");
        GameManager.Instance.PlayerDied();
        
        FirstPersonController fpc = GetComponent<FirstPersonController>();
        if (fpc != null)
        {
            fpc.Die();
        }
    }
}
