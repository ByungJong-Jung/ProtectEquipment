using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AiManager : MonoBehaviour
{
    private static AiManager instance;
    public static AiManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(AiManager)) as AiManager;
            if (!instance)
            {
                Debug.LogError("no one");
            }
        }
        return instance;
    }

    [Header("Ai")]
    public Ai[] ais;

    #region EDITOR
    [Button]
    public void SettingAis()
    {
        ais = FindObjectsOfType(typeof(Ai)) as Ai[];
    }
    #endregion


    public void Init_AiManager()
    {
        for (int i = 0; i < ais.Length; i++)
            ais[i].Init_Ai();
    }
}
