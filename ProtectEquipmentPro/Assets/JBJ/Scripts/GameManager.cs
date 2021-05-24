using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            if (!instance)
            {
                Debug.LogError("no one");
            }
        }
        return instance;
    }


  
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        SketchfabManager.GetInstance().Init_SketchfabManager();
        UiManager.GetInstance().Init_UiManager();
        AiManager.GetInstance().Init_AiManager();
        VideoManager.GetInstance().Init_videoPlaye();
        PlayerMovement.GetInstance().Init_PlayerMovement();
    }


   
}
