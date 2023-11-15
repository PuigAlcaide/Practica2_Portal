using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    // Start is called before the first frame update
    void Start()
    {
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
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Cursor.lockState = CursorLockMode.Locked;
    }
}