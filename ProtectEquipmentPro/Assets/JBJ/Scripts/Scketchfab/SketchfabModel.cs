using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum VIEW_MODEL_TYPE
{
    ALL,
    CLOTHING,
    MASK
}

public class SketchfabModel : MonoBehaviour
{
    public MODEL_TYPE myType;
    public Transform[] models;
    [SerializeField] private Vector3[] fisrtModelsLocalPos;
    [SerializeField] private Vector3 firstSketchfabPoints;

    public Transform sketchfabPoints;
    public Transform pivot;

    #region EDITOR

    [Button]
    public void SettingModels()
    {
        int forCount = pivot.transform.childCount + pivot.transform.GetChild(0).childCount;
        models = new Transform[forCount];
        models[0] = pivot.transform.GetChild(0);
        models[0].gameObject.SetActive(false);
        for (int i = 1; i < forCount; i++)
        {
            models[i] = models[0].GetChild(i - 1);
            models[i].gameObject.SetActive(false);
        }

        fisrtModelsLocalPos = new Vector3[models.Length];
        for(int i = 0;i<fisrtModelsLocalPos.Length;i++)
        {
            fisrtModelsLocalPos[i] = models[i].localPosition;
        }

        firstSketchfabPoints = sketchfabPoints.position;
    }
    #endregion


    public Transform ModelActive(VIEW_MODEL_TYPE _viewModel, bool _acivity)
    {
        // 1. 엑티브 띄우고
        // 2. 포지션 바꾸고
        // 3. 모델 초기화 시키고
        if (_acivity) pivot.SetParent(sketchfabPoints);
        else if (!_acivity)
        {
            pivot.SetParent(transform);
            pivot.localPosition = new Vector3(0f,1f,0f);
        }
        sketchfabPoints.parent.position = firstSketchfabPoints;
        sketchfabPoints.position = firstSketchfabPoints;
      

        switch (_viewModel)
        {
            case VIEW_MODEL_TYPE.ALL:
                for (int i = 0; i < models.Length; i++)
                {
                    Transform temp = models[i];
                    temp.gameObject.SetActive(_acivity);
                    if (i == 0) temp.localPosition = Vector3.zero.ModifiedY(-1f);
                    else temp.localPosition = fisrtModelsLocalPos[i];
                }
                break;
            case VIEW_MODEL_TYPE.CLOTHING:
                for (int i = 0; i < models.Length; i++)
                {
                    Transform temp = models[i];
                    if (i == (int)_viewModel)
                    {
                        temp.gameObject.SetActive(_acivity);
                        temp.position = sketchfabPoints.position;
                    }
                    else if(i != (int)_viewModel && i != 0)
                    {
                        temp.gameObject.SetActive(false);
                    }
                }
                break;
            case VIEW_MODEL_TYPE.MASK:
                for (int i = 0; i < models.Length; i++)
                {
                    Transform temp = models[i];
                    if (i == (int)_viewModel)
                    {
                        temp.gameObject.SetActive(_acivity);
                        temp.position = sketchfabPoints.position;
                        sketchfabPoints.parent.localPosition = 
                            new Vector3(-0.05f, sketchfabPoints.parent.localPosition.y, 0.007f);
                    }
                    else if (i != (int)_viewModel && i != 0)
                    { 
                        temp.gameObject.SetActive(false); 
                    }
                }
                break;
            default:
                break;
        }

        return models[(int)_viewModel];
    }

    public void ResetModelRotate()
    {
        for (int i = 0; i < models.Length; i++)
            models[i].localRotation = Quaternion.Euler(Vector3.zero);
       
        sketchfabPoints.position = firstSketchfabPoints;
        sketchfabPoints.parent.position = firstSketchfabPoints;

    }

    public void ResetSketchfabRotation()
    {
        Vector3 pivotRoVec = pivot.rotation.eulerAngles;
        pivot.localPosition = Vector3.zero.ModifiedY(1f);
        sketchfabPoints.parent.localRotation = Quaternion.Euler(Vector3.zero);
        pivot.rotation = Quaternion.Euler(pivotRoVec);
    }


}
