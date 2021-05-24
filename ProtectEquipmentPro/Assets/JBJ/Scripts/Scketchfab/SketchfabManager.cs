using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class SketchfabManager : MonoBehaviour
{
    private static SketchfabManager instance;
    public static SketchfabManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(SketchfabManager)) as SketchfabManager;
            if (!instance)
            {
                Debug.LogError("no one");
            }
        }
        return instance;
    }

    #region EDITOR
    [Button]
    public void SettingInitalizeAllModels()
    {
        sketchfabModels = FindObjectsOfType(typeof(SketchfabModel)) as SketchfabModel[];
        boothModels = FindObjectsOfType(typeof(BoothModel)) as BoothModel[];

    }
    [Button]
    public void SettingSketchfabModel()
    {
        if (sketchfabModels.Length != (int)MODEL_TYPE.Counts)
        {
            Debug.LogError("Not Enough SketchfabModels Counts");
            return;
        }

        for (int i = 0; i < sketchfabModels.Length; i++)
        {
            sketchfabModels[i].gameObject.name = "SketchfabModel_" + sketchfabModels[i].myType.ToString();
            sketchfabModels[i].sketchfabPoints = sketchfabPointX;
        }
    }

    [Button]
    public void SettingBoothModel()
    {
        if (boothModels.Length != (int)MODEL_TYPE.Counts)
        {
            Debug.LogError("Not Enough boothModels Counts");
            return;
        }

        for (int i = 0; i < boothModels.Length; i++)
        {
            boothModels[i].gameObject.name = "boothModel_" + boothModels[i].myType.ToString();
        }
    }

    [Button]
    public void RookAtTest()
    {
        sketchfabPointY.LookAt(sketchfabCamera.transform);
    }
    #endregion

    public Camera mainCamera;
    public Camera sketchfabCamera;

    [Header("Models")]
    public SketchfabModel[] sketchfabModels;
    public BoothModel[] boothModels;

    [Header("Sketchfab Point")]
    public Transform sketchfabPointY;
    public Transform sketchfabPointX;

    [Header("For Rotate")]
    [SerializeField] private Transform modelingTransform;
    public SketchfabModel curSketchfabModel_script;

    [Header("현재 뷰 타입")]
    [SerializeField] private VIEW_MODEL_TYPE curViewModelType;

    public void Init_SketchfabManager()
    {
        mainCamera.gameObject.SetActive(true);
        sketchfabCamera.gameObject.SetActive(false);
        for (int i = 0; i < sketchfabModels.Length; i++)
        {
            SketchfabModel temp = sketchfabModels[i];
            modelingTransform = temp.ModelActive(VIEW_MODEL_TYPE.ALL, false);
            break;
        }
    }

    

    public void SettingSketchfabView(MODEL_TYPE _modelType)
    {
        for (int i = 0; i < sketchfabModels.Length; i++)
        {
            SketchfabModel temp = sketchfabModels[i];
            if (temp.myType == _modelType)
            {
                curSketchfabModel_script = temp;
                modelingTransform = temp.ModelActive(VIEW_MODEL_TYPE.ALL, true);
                break;
            }
        }

        UiManager uiManager_script = UiManager.GetInstance();
        uiManager_script.SketchfabViewOn(true);
        uiManager_script.ModelPanelActive(_modelType,true);
    }

    public void Active_On_BoxCustom(MODEL_TYPE modelType)
    {
        // 스케치펩 뷰 초기화
        SettingSketchfabView(modelType);
        mainCamera.gameObject.SetActive(false);
        sketchfabCamera.gameObject.SetActive(true);
        StartCoroutine(CustomMoving());
    }

    public void Active_Off_CustomBox()
    {
        PlayerMovement.GetInstance().eventTrigger = false;
        mainCamera.gameObject.SetActive(true);
        sketchfabCamera.gameObject.SetActive(false);
        curSketchfabModel_script.ResetModelRotate();
        sketchfabPointX.localRotation = Quaternion.Euler(Vector3.zero);
        sketchfabPointY.LookAt(sketchfabCamera.transform);
        curSketchfabModel_script.ModelActive(VIEW_MODEL_TYPE.ALL, false);
        isRotateStart = false;
        isFirstTourch = false;
        StopAllCoroutines();
    }

    public void ModelActive(VIEW_MODEL_TYPE _viewModelType,bool _activity)
    {
        curViewModelType = _viewModelType;
        curSketchfabModel_script.ModelActive(_viewModelType, _activity);
    }

    IEnumerator CustomMoving()
    {
        while (true)
        {
            Swipe();
            ScrollObject();
            yield return null;
        }
    }

    [Header("Rotate Objects")]
    [Range(1f, 10f)]
    [SerializeField] private float rotationSpeed;
    [Range(1f,5f)]
    [SerializeField] private float scrollSpeed;

    [SerializeField] private bool isRotateStart;
    [SerializeField] private bool isFirstTourch;
    [SerializeField] private float sketchfabOutoRotateSpeed;

    private void Swipe()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = sketchfabCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Modeling"))
                {
                    isRotateStart = true;
                    isFirstTourch = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotateStart = false;
        }

        if (isRotateStart)
        {
            // get the user touch input
            if (Input.GetMouseButton(0))
            {
                if (isRotateStart)
                {
                    float rotX = Input.GetAxis("Mouse X") * Mathf.Rad2Deg * Time.deltaTime * rotationSpeed * -1f;
                    float rotY = Input.GetAxis("Mouse Y") * Mathf.Rad2Deg * Time.deltaTime * rotationSpeed * -1f;

                    Vector3 dirVec = sketchfabPointY.position - sketchfabCamera.transform.position;
                    
                    Vector3 curLotationY = sketchfabPointY.localRotation.eulerAngles;
                    Vector3 curLotationX = sketchfabPointX.localRotation.eulerAngles;
                    sketchfabPointX.localRotation = Quaternion.Euler(0f, rotX + curLotationX.y, 0f);
                    sketchfabPointY.Rotate(rotY,0f,0f);
                }
            }
        }
        else if(!isRotateStart && !isFirstTourch)
        {
            sketchfabPointX.Rotate(0f, -1f * sketchfabOutoRotateSpeed * Time.deltaTime, 0f);
        }
        
    }

    [Header("Sketchfab Scroll Range")]
    [SerializeField] private Vector3 minVec;
    [SerializeField] private Vector3 maxVec;
    [SerializeField] private float radius;

    private void ScrollObject()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        bool isScrollOn = scroll != 0f;
        if(isScrollOn && (curViewModelType != VIEW_MODEL_TYPE.MASK))
        {
            Vector3 sketchfabVec = sketchfabPointY.position;
            Vector3 dirVec = sketchfabVec - sketchfabCamera.transform.position;
            dirVec = dirVec.normalized;
            Vector3 sv = sketchfabVec + dirVec * scroll * -.5f;
            sketchfabPointY.position = new Vector3(
                Mathf.Clamp(sv.x, minVec.x, maxVec.x),
                Mathf.Clamp(sv.y, minVec.y, maxVec.y),
                Mathf.Clamp(sv.z, minVec.z, maxVec.z));
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(sketchfabPointX.position, .15f);
    }
}
