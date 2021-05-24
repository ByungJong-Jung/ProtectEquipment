using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ModelPanel : MonoBehaviour
{
    public MODEL_TYPE myType;


    [Header("테두리 색 관련")]
    public Image[] viewKindImages;
    [SerializeField] private Color nomalColor;
    [SerializeField] private Color selectColor;


    /*
     *  ui요소를 효과적으로 데이터 저장하는 법을 생각해야함.....
     * 
     */
    #region EDITOR
   

    #endregion


}
