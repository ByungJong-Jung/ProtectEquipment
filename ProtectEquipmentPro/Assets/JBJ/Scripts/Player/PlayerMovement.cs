using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public enum PLAYER_STATE
{
    IDLE,
    WALK,
    RUN
}

public enum PLAYER_VIEW_MODE
{
    ONE,
    THREE
}

public enum MOUSE_CURSOR_TYPE
{
    DEFAULT,
    MOVE,
    CLICK
}

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;
    public static PlayerMovement GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
            if (!instance)
            {
                Debug.LogError("no one");
            }
        }
        return instance;
    }

    public Transform character;
    public Transform cameraArm;

    [Header("== 카메라 움직임 ==")]
    [Range(100f, 300f)]
    [SerializeField] private float cameraRotateSpeed;
    public Camera mainCamera;
    [SerializeField] private Vector3 cameraCenterPoint;
    [SerializeField] private Transform targetRangeTransform;
    [SerializeField] private Vector3 targetRangePos;
    [SerializeField] private Vector2 cameraScrollRange;
    private float rayDistanceRange;

    [Header("== 플레이어 컨트롤러 ==")]
    [SerializeField] private CharacterController playerController;
    [SerializeField] private Vector3 finalMoveDir;
    [SerializeField] private bool isMoving;

    [Header("== 플레이어 속도 ==")]
    [Range(1f, 3f)]
    public float moveSpeed;
    [Range(5f, 10f)]
    public float runSpeed;
    private float verticalMoveSpeed;
    private float verticalRunSpeed;

    [SerializeField] private bool isRunState;
    private bool isMove;

    [Header("== 스크롤 스피드 ==")]
    [SerializeField] private float scrollSpeed;

    [Header("== 에니메이터 ==")]
    [SerializeField] private Animator playerAnim;
    public float Horizontal;
    public float Vertical;

    [Header("== 플레이어 상태 ==")]
    [SerializeField] private PLAYER_STATE playerState;

    [Header("== 중력 ==")]
    [Range(10f, 50f)]
    [SerializeField] private float gravity;
    private float verticalVelocity;
    public bool eventTrigger;

    [Header("부스모델")]
    BoothModel bootModelTemp;

    [Header("아웃라인 오브젝트")]
    public Transform outlineObjectParnet;
    public OutlineModel[] outlineModels;

    [Header("마우스 커서")]
    [SerializeField] private Texture2D[] cursorTexture;

    #region EDITOR
    [Button]
    public void SettingOutlineModels()
    {
        outlineModels = new OutlineModel[outlineObjectParnet.childCount];
        for (int i = 0; i < outlineModels.Length; i++)
            outlineModels[i] = outlineObjectParnet.GetChild(i).GetComponent<OutlineModel>();
    }
    #endregion


    public void Init_PlayerMovement()
    {
        StartCoroutine(State_Walk());
        cameraCenterPoint = new Vector3(mainCamera.pixelWidth * 0.5f, mainCamera.pixelHeight * 0.5f, 0f);
        targetRangeTransform.position = new Vector3(cameraCenterPoint.x, cameraCenterPoint.y, targetRangeTransform.position.z);
        targetRangePos = targetRangeTransform.position;
        rayDistanceRange = Vector3.Distance(targetRangePos, mainCamera.transform.position);
        playerController = gameObject.GetComponent<CharacterController>();
        verticalMoveSpeed = moveSpeed - 0.5f;
        verticalRunSpeed = runSpeed - 2f;
        curViewMode = PLAYER_VIEW_MODE.THREE;
        Init_MouseCursor();
    }

    public void Init_MouseCursor()
    {
        ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.DEFAULT);
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void ChangeMouseCursorImage(MOUSE_CURSOR_TYPE _cursorType)
    {
        Cursor.SetCursor(cursorTexture[(int)_cursorType], Vector2.zero, CursorMode.Auto);
    }
    public void AllOutlineModelOff()
    {
        for (int i = 0; i < outlineModels.Length; i++)
            outlineModels[i].ActiveObject(false);
    }

    private void Update()
    {
        if (!eventTrigger)
        {
            MoveCharacter();
            CameraSetting();
        }
    }


    #region Animation
    IEnumerator State_Walk()
    {
        playerState = PLAYER_STATE.WALK;
        playerAnim.SetBool("IsRun", false);


        while (playerState == PLAYER_STATE.WALK)
        {
            playerAnim.SetFloat("Horizontal", Horizontal);
            playerAnim.SetFloat("Vertical", Vertical);
            if (isRunState)
            {
                StartCoroutine(State_Run());
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator State_Run()
    {
        playerState = PLAYER_STATE.RUN;
        playerAnim.SetBool("IsRun", isRunState);
        while (playerState == PLAYER_STATE.RUN)
        {
            playerAnim.SetFloat("Horizontal", Horizontal);
            playerAnim.SetFloat("Vertical", Vertical);
            if (!isRunState)
            {
                StartCoroutine(State_Walk());
                yield break;
            }
            yield return null;
        }
    }
    #endregion

    #region 캐릭터 움직임 연산 
    private void MoveCharacter()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(Horizontal, Vertical);
        isMove = moveInput.magnitude != 0;
        float speed = moveSpeed;

        // 중력 처리
        if (!playerController.isGrounded)
        {
            verticalVelocity -= (gravity * Time.deltaTime);
            Debug.Log(verticalVelocity);
        }
        if (isMove && playerController.isGrounded)
        {
            isMoving = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunState = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRunState = false;
            }

            if (!isRunState)
            {
                speed = moveSpeed;
                if (Vertical < 0f) speed = verticalMoveSpeed;
            }
            else if (isRunState)
            {
                speed = runSpeed;
                if (Vertical < 0f) speed = verticalRunSpeed;
            }

            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            character.forward = lookForward;

            finalMoveDir = moveDir * Time.deltaTime * speed;
        }
        else if(!isMove && isMoving)
        {
            StopPlayer();
            isMoving = false;
        }
        finalMoveDir.y = verticalVelocity;
        playerController.Move(finalMoveDir);
        character.localPosition = Vector3.zero;
    }
    private void StopPlayer()
    {
        Horizontal = 0f; Vertical = 0f;
        finalMoveDir = Vector3.zero;
        playerController.Move(Vector3.zero);

    }
    #endregion

    #region 카메라 
    private void CameraSetting()
    {
        LookAround();

        ClickModel();

        ScrollCamera();

        ChangePointOfView();
    }


    #region LookAround
    private void LookAround()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.MOVE);
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            float speed = cameraRotateSpeed * Time.deltaTime;
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed);
            Vector3 cameraAngle = cameraArm.rotation.eulerAngles;

            float rotateX = cameraAngle.x - mouseDelta.y;

            if (rotateX < 180f)
            {
                rotateX = Mathf.Clamp(rotateX, -1f, 70f);
            }
            else
            {
                rotateX = Mathf.Clamp(rotateX, 330f, 361f);
            }
            cameraArm.rotation = Quaternion.Euler(rotateX, cameraAngle.y + mouseDelta.x, cameraAngle.z);
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.DEFAULT);
        }
    }

    #endregion

    #region ClickModel

    [Header("== Click Event ==")]
    public bool isClickModel;
    public bool isCLickProduct;
    public bool isClickCardNews;
    public bool isModelClick;
    public bool isVideoClick;
    public bool isClickVideoPlayer;
    private void ClickModel()
    {
        //Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, Time.deltaTime);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            SwitchCaseClickModel(hit.transform);

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.CompareTag("Model"))
                {
                    isClickModel = true;
                }
                else if (hit.transform.CompareTag("Product"))
                {
                    isCLickProduct = true;
                }
                else if (hit.transform.CompareTag("CardNews"))
                {
                    isClickCardNews = true;
                }
                else if(hit.transform.CompareTag("VideoPlayer"))
                {
                    isClickVideoPlayer = true;
                }
                
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (isClickModel)
                {
                    isClickModel = false;
                    if (hit.transform.CompareTag("Model"))
                    {
                        StopPlayer();
                        eventTrigger = true;
                        isClickModel = true;
                        if (bootModelTemp != null) SketchfabManager.GetInstance().Active_On_BoxCustom(bootModelTemp.myType);
                        ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.DEFAULT);
                    }
                }
                else if (isCLickProduct)
                {
                    isCLickProduct = false;
                    if (hit.transform.CompareTag("Product"))
                    {
                        StopPlayer();
                        eventTrigger = true;
                        ProductUi productUi = hit.transform.GetComponent<ProductUi>();
                        UiManager.GetInstance().PanelProductUiActive(true, productUi.myType);
                        ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.DEFAULT);

                    }
                }
                else if (isClickCardNews)
                {
                    isClickCardNews = false;
                    if (hit.transform.CompareTag("CardNews"))
                    {
                        StopPlayer();
                        eventTrigger = true;
                        UiManager.GetInstance().CardNewsViewOn();
                        ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.DEFAULT);
                    }
                }
                else if (isClickVideoPlayer)
                {
                    isClickVideoPlayer = false;
                    if(hit.transform.CompareTag("VideoPlayer"))
                    {
                        StopPlayer();
                        eventTrigger = true;
                        ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.DEFAULT);
                        UiManager.GetInstance().VideoPlayerViewOn();
                    }
                }
            }
        }
    }
    public void SwitchCaseClickModel(Transform hitTransform)
    {
        switch (hitTransform.tag)
        {
            case "Model":
                if (!isModelClick)
                {
                    isModelClick = true;
                    isVideoClick = false;
                    AllOutlineModelOff();
                    bootModelTemp = hitTransform.GetComponent<BoothModel>();
                    outlineModels[(int)bootModelTemp.myType].ActiveObject(true);
                    ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.CLICK);
                }
                break;
            case "Product":
                if (isModelClick || isVideoClick)
                {
                    isModelClick = false;
                    isVideoClick = false;
                    AllOutlineModelOff();
                }
                ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.CLICK);
                break;
            case "CardNews":
                if (isModelClick || isVideoClick)
                {
                    isModelClick = false;
                    isVideoClick = false;
                    AllOutlineModelOff();
                }
                ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.CLICK);
                break;
            case "VideoPlayer":
                if (!isVideoClick)
                {
                    isVideoClick = true;
                    isModelClick = false;
                    AllOutlineModelOff();
                    outlineModels[(int)MODEL_TYPE.Counts].ActiveObject(true);
                    ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.CLICK);
                }
                break;
            default:
                if (isModelClick || isVideoClick)
                {
                    isModelClick = false;
                    isVideoClick = false;
                    AllOutlineModelOff();
                }
                ChangeMouseCursorImage(MOUSE_CURSOR_TYPE.DEFAULT);
                break;
        }
    }

    #endregion

    #region ScrollCamera
    private void ScrollCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        bool isScrollOn = scroll != 0f;
        if (isScrollOn && (curViewMode == PLAYER_VIEW_MODE.ONE))
        {
            float curCameraFiledOfView = mainCamera.fieldOfView;
            float cameraFiledOfView = curCameraFiledOfView - scroll;
            cameraFiledOfView = Mathf.Clamp(cameraFiledOfView, cameraScrollRange.x, cameraScrollRange.y);
            mainCamera.fieldOfView = cameraFiledOfView;
        }
    }

    #endregion

    #region ChanagePointOfView

    [Header("== 카메라 시점변경 ==")]
    [SerializeField] private bool isChangingNow = false;
    [SerializeField] private PLAYER_VIEW_MODE curViewMode;
    private void ChangePointOfView()
    {
        Vector3 cameraCurrentPos = mainCamera.transform.localPosition;
        // 3인칭
        if (Input.GetKeyDown("1") && curViewMode == PLAYER_VIEW_MODE.ONE)
        {
            mainCamera.fieldOfView = 60f;
            curViewMode = PLAYER_VIEW_MODE.THREE;
            Vector3 purposePos = cameraCurrentPos.ModifiedZ(-1f);
            if (!isChangingNow) StartCoroutine(MoveCameraForView(purposePos));
            //mainCamera.transform.localPosition = cameraCurrentPos.ModifiedZ(-1f);
        }
        // 1인칭
        else if (Input.GetKeyDown("2") && curViewMode == PLAYER_VIEW_MODE.THREE)
        {
            curViewMode = PLAYER_VIEW_MODE.ONE;
            Vector3 purposePos = cameraCurrentPos.ModifiedZ(0.2f);
            if (!isChangingNow) StartCoroutine(MoveCameraForView(purposePos));
            //mainCamera.transform.localPosition = cameraCurrentPos.ModifiedZ(1f);
        }
    }
    IEnumerator MoveCameraForView(Vector3 _purposePos)
    {
        isChangingNow = true;

        while (true)
        {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, _purposePos, .12f);

            if (Vector3.Distance(mainCamera.transform.localPosition, _purposePos) < 0.05f)
            {
                mainCamera.transform.localPosition = _purposePos;
                isChangingNow = false;
                yield break;
            }

            yield return null;
        }
    }

    #endregion

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(cameraArm.position, .1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetRangeTransform.position, .3f);
    }

}

