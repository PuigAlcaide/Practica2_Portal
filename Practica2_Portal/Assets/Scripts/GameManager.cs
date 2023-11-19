using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    private static GameManager instance; // Singleton instance

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public GameObject gameOver;
    public Checkpoint checkpoint;
    private FirstPersonController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<FirstPersonController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // OnDestroy is called when the object is being destroyed
    private void OnDestroy()
    {
        if (this == instance)
        {
            instance = null;
        }
    }

    public void PlayerDied()
    {
        gameOver.SetActive(true);
        playerController.enabled = false;
        // Show the mouse cursor
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        // Move the gameObject to the checkpoint position
        playerController.gameObject.transform.position = checkpoint.transform.position;

        // Activate the FirstPersonController component again
        Invoke("EnableController", 0.5f);

    }
    
    private void EnableController()
    {
        // Activate the FirstPersonController component again
        playerController.enabled = true;
        gameOver.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateCheckpoint(Checkpoint checkpoint)
    {
        this.checkpoint = checkpoint;
    }
}