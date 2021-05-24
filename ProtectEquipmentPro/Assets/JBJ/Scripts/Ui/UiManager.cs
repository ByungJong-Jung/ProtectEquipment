using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class UiManager : MonoBehaviour
{
    private static UiManager instance;
    public static UiManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(UiManager)) as UiManager;
            if (!instance)
            {
                Debug.LogError("no one");
            }
        }
        return instance;
    }


    [Header("== Main Ui ==")]
    public GameObject panel_Main;

    [Header("== Sketchfab Ui ==")]
    public GameObject backGroundImage;
    public GameObject panel_Sketchfab;
    public ModelPanel[] modelPanels;
    public Text nameText;
    public Transform SketchfabUiParent;
    public Image[] cardBoardImages;
    public Image[] cardBoardButtonImages;

    public int currentCardBoardCount;
    [SerializeField] private int beforeCardBoardCount;


    [Header("== Buttons ==")]
    public Button closedSketchfabButton;
    public Button allButton;
    public Button clothingutton;
    public Button maskButton;

    [Header("== Image Resources ==")]
    [SerializeField] private ImageResource imageResource_script;
    [SerializeField] private MODEL_TYPE curModelType;

    [Header("== Product Image ==")]
    [SerializeField] private GameObject panel_Product;
    [SerializeField] private Image productIamge;
    [SerializeField] private Button closedProductButton;

    [Header("== CardNews Image ==")]
    [SerializeField] private GameObject panel_CardNews;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Button closedCardNesButton;
    [SerializeField] private RectTransform cardNesContainer;
    [SerializeField] private Image[] underImages;
    [SerializeField] private Sprite underBeforeImage;
    [SerializeField] private Sprite underClickImage;

    [Header("== Video Player ==")]
    [SerializeField] private GameObject panel_VideoPlayer;
    [SerializeField] private Button closedVideoPlayerButton;


    #region EDITOR
    [Button]
    public void SettingModelPanels()
    {
        modelPanels = FindObjectsOfType(typeof(ModelPanel)) as ModelPanel[];
    }

    [Button]
    public void SettingSketchfabUi()
    {
        if (modelPanels.Length != (int)MODEL_TYPE.Counts)
        {
            Debug.LogError("Not Enough modelPanels Counts");
            return;
        }

        for (int i = 0; i < modelPanels.Length; i++)
        {
            modelPanels[i].myType = (MODEL_TYPE)i;
            modelPanels[i].gameObject.name = "panel_" + modelPanels[i].myType.ToString();
        }
    }


    [Button]
    public void SettingCardBoard()
    {
        int cardBoardMaxCount = 4;
        cardBoardImages = new Image[cardBoardMaxCount];
        cardBoardButtonImages = new Image[cardBoardMaxCount];
        for (int i = 0; i < cardBoardMaxCount; i++)
            cardBoardButtonImages[i] = SketchfabUiParent.GetChild(i + 1).GetComponent<Image>();

        for (int i = 0; i < cardBoardMaxCount; i++)
            cardBoardImages[i] = SketchfabUiParent.GetChild(i + 5).GetComponent<Image>();
    }


    #endregion

    public void Init_UiManager()
    {
        Initialize_Buttons();
        SketchfabViewOn(false);
        PanelProductUiActive(false, MODEL_TYPE.OPEN_DEVICE);
        panel_CardNews.gameObject.SetActive(false);
        VideoPlayerActive(false);
    }

    public void Initialize_Buttons()
    {
        closedSketchfabButton.onClick.AddListener(OnClickButton_closedSketchfabButton);
        allButton.onClick.AddListener(OnClickButton_allButton);
        clothingutton.onClick.AddListener(OnClickButton_clothingutton);
        maskButton.onClick.AddListener(OnClickButton_maskButton);
        closedProductButton.onClick.AddListener(OnClickButton_ClosedProductButton);
        leftButton.onClick.AddListener(OnClickButton_Left);
        RightButton.onClick.AddListener(OnClickButton_Right);
        closedCardNesButton.onClick.AddListener(OnClickButton_ClosedCardNesPanel);
        closedVideoPlayerButton.onClick.AddListener(OnClickButton_ClosedVideoPlayerButton);

        for (int i = 0; i < cardBoardButtonImages.Length; i++)
        {
            int index = i;
            cardBoardButtonImages[index].GetComponent<Button>().
                onClick.AddListener(delegate { OnClickButton_cardBoardButton(index); });
        }
    }


    #region Panel Sketchfab

    #region 스케츠팹 뷰 관련
    public void SketchfabViewOn(bool _activity)
    {
        backGroundImage.SetActive(_activity);
        panel_Main.SetActive(!_activity);
        panel_Sketchfab.SetActive(_activity);
        if (!_activity) PageOff();
    }

    public void ModelPanelActive(MODEL_TYPE _modelType, bool _activity)
    {
        if (_activity)
        {
            SketchfabUiParent.gameObject.SetActive(_activity);
            imageResource_script.SettingUi(_modelType, VIEW_MODEL_TYPE.ALL);
            curModelType = _modelType;
        }
        else if (!_activity)
        {
            SketchfabUiParent.gameObject.SetActive(_activity);
        }
    }

    #endregion

    #region 버튼 로드 슬라이드
    [Header("Button Slide Animation")]
    [SerializeField] private float slidePurPosX = 350f;
    // 버튼 로드 슬라이드-------------------------------------
    public void StartLoadAnimation()
    {
        float startPosDis = 70f;
        for (int i = 0; i < currentCardBoardCount; i++)
        {
            Image cardBoardButtonImage = cardBoardButtonImages[i];
            Vector3 tempVec = cardBoardButtonImage.rectTransform.localPosition;
            cardBoardButtonImage.rectTransform.localPosition = tempVec.ModifiedX(tempVec.x + startPosDis);
            startPosDis += startPosDis;
        }
        isFinishLoadAnimation = new bool[currentCardBoardCount];
        // 애니메이션 스타트    
        for (int i = 0; i < currentCardBoardCount; i++)
        {
            isFinishLoadAnimation[i] = false;
            StartCoroutine(StartLoadAnimation_Coroutine(i, cardBoardButtonImages[i]));
        }

        beforeCardBoardCount = currentCardBoardCount;
    }
    // 버튼 로드 슬라이드-------------------------------------

    // 버튼 로드 슬라이드 코루틴애니메이션-------------------------------------
    [Header("Load Slide Animation speed")]
    [Range(0f, 10f)]
    [SerializeField] private float LoadSlideSpeed;
    private bool[] isFinishLoadAnimation;

    IEnumerator StartLoadAnimation_Coroutine(int _boolCountIndex, Image _image)
    {
        Vector3 purPosVec = _image.rectTransform.localPosition.ModifiedX(slidePurPosX);
        while (true)
        {
            _image.rectTransform.localPosition = Vector3.Lerp(_image.rectTransform.localPosition, purPosVec, LoadSlideSpeed * Time.deltaTime);

            if (Vector2.Distance(_image.rectTransform.localPosition, purPosVec) < 1f)
            {
                _image.rectTransform.localPosition = purPosVec;
                isFinishLoadAnimation[_boolCountIndex] = true;
                yield break;
            }

            yield return null;
        }
    }

    public bool IsFinishLoadAnim()
    {
        for (int i = 0; i < isFinishLoadAnimation.Length; i++)
        {
            if (!isFinishLoadAnimation[i]) return false;
        }

        return true;
    }
    // 버튼 로드 슬라이드 코루틴애니메이션-------------------------------------

    public void ResetPos()
    {
        StopAllCoroutines();
        for (int i = 0; i < currentCardBoardCount; i++)
        {
            cardBoardButtonImages[i].rectTransform.localPosition =
                cardBoardButtonImages[i].rectTransform.localPosition.ModifiedX(350f);
        }
    }

    #endregion

    #region OnClickButton

    public void OnClickButton_closedSketchfabButton()
    {
        PageOff();
        ResetPos();
        SketchfabViewOn(false);
        SketchfabManager.GetInstance().Active_Off_CustomBox();
    }

    public void OnClickButton_allButton()
    {
        PageOff();
        imageResource_script.SettingUi(curModelType, VIEW_MODEL_TYPE.ALL);
        SketchfabManager.GetInstance().ModelActive(VIEW_MODEL_TYPE.ALL, true);
    }

    public void OnClickButton_clothingutton()
    {
        PageOff();
        imageResource_script.SettingUi(curModelType, VIEW_MODEL_TYPE.CLOTHING);
        SketchfabManager.GetInstance().ModelActive(VIEW_MODEL_TYPE.CLOTHING, true);
    }

    public void OnClickButton_maskButton()
    {
        PageOff();
        imageResource_script.SettingUi(curModelType, VIEW_MODEL_TYPE.MASK);
        SketchfabManager.GetInstance().ModelActive(VIEW_MODEL_TYPE.MASK, true);
    }

    public void OnClickButton_cardBoardButton(int _buttonIndex)
    {
        PageOff();
        PageOn(_buttonIndex);
    }

    // 버튼 클릭 슬라이드 -------------------------------------
    public void PageOn(int _buttonIndex)
    {
        if (!IsFinishLoadAnim()) return;

        cardBoardButtonImages[_buttonIndex].gameObject.SetActive(false);

        int forCount = currentCardBoardCount;
        Debug.Log(forCount);
        for (int i = _buttonIndex + 1; i < forCount; i++)
        {
            Vector3 temp = cardBoardButtonImages[i].rectTransform.localPosition;
            cardBoardButtonImages[i].rectTransform.localPosition = temp.ModifiedY(temp.y + slideDistance_ForCardBoardButton);
        }

        //cardBoardImages[_buttonIndex].gameObject.SetActive(true);
        StartCoroutine(CardBoardAnimation(cardBoardImages[_buttonIndex]));

        beforeIndex = _buttonIndex;
    }

    [Header("Page Animation")]
    [SerializeField] private float carBoardPosY = -30f;
    [SerializeField] private float slideDistance_ForCardBoardButton = -140f;

    [SerializeField] private int beforeIndex = -1;
    [SerializeField] private Image rebackImage;
    [SerializeField] private float rebackImagePosY = 50f;


    // 정화통 이벤트 함수
    public void REBACK_SIZE()
    {
        if (rebackImage == null) return;

        Vector3 rebackImageVec = rebackImage.rectTransform.localPosition;
        rebackImage.rectTransform.localPosition = rebackImageVec.ModifiedY(rebackImageVec.y + rebackImagePosY);
        rebackImage.rectTransform.sizeDelta = rebackImage.rectTransform.sizeDelta.ModifiedY(250f);
        rebackImage = null;
    }

    public void PageOff()
    {
        if (beforeIndex == -1) return;

        StopAllCoroutines();
        REBACK_SIZE();
        cardBoardButtonImages[beforeIndex].gameObject.SetActive(true);

        int forCount = beforeCardBoardCount;

        for (int i = beforeIndex + 1; i < forCount; i++)
        {
            Vector3 tempBoardButton = cardBoardButtonImages[i].rectTransform.localPosition;
            cardBoardButtonImages[i].rectTransform.localPosition = tempBoardButton.ModifiedY(tempBoardButton.y - slideDistance_ForCardBoardButton);
        }

        cardBoardImages[beforeIndex].gameObject.SetActive(false);
        beforeIndex = -1;
    }
    // 버튼 클릭 슬라이드 -------------------------------------

    // 버튼 클릭 슬라이드 코루틴애니메이션-------------------------------------
    [Header("Button Click Animation Speed")]
    [Range(0f, 10f)]
    public float clickAnimationSpeed;
    IEnumerator CardBoardAnimation(Image _cardBoardImage)
    {
        Sprite compSprite = imageResource_script.cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
        Vector3 curLocalPos = _cardBoardImage.rectTransform.localPosition;
        Vector2 purPosVec = _cardBoardImage.rectTransform.sizeDelta;

        if (_cardBoardImage.sprite.Equals(compSprite))
        {
            rebackImage = _cardBoardImage;
            _cardBoardImage.rectTransform.localPosition = curLocalPos.ModifiedY(curLocalPos.y - rebackImagePosY);
            purPosVec = purPosVec.ModifiedY(350f);
        }
        else REBACK_SIZE();

        _cardBoardImage.gameObject.SetActive(true);

        _cardBoardImage.rectTransform.sizeDelta = new Vector2(400f, 0f);
        while (true)
        {
            _cardBoardImage.rectTransform.sizeDelta =
                Vector2.Lerp(_cardBoardImage.rectTransform.sizeDelta, purPosVec, clickAnimationSpeed * Time.deltaTime);

            if (Vector2.Distance(_cardBoardImage.rectTransform.sizeDelta, purPosVec) < 0.5f)
            {
                _cardBoardImage.rectTransform.sizeDelta = purPosVec;
                yield break;
            }

            yield return null;
        }
    }
    // 버튼 클릭 슬라이드 코루틴애니메이션-------------------------------------
    #endregion

    #endregion

    #region Panel_ProductUi

    #region OnClickButton
    public void OnClickButton_ClosedProductButton()
    {
        panel_Product.SetActive(false);
        panel_Main.SetActive(true);
        PlayerMovement.GetInstance().eventTrigger = false;
    }

    #endregion

    #region 상품 판넬
    [Header("== Product Panel Animation ==")]
    [Range(0f,10f)]
    [SerializeField] private float panelSpeed;
    public void PanelProductUiActive(bool _activity, MODEL_TYPE _modelType)
    {
        panel_Product.SetActive(_activity);
        panel_Main.SetActive(!_activity);
        MoveAnimation(_activity, _modelType);
    }

    public void MoveAnimation(bool _panelActivity, MODEL_TYPE _modelType)
    {
        if(!_panelActivity) panel_Main.SetActive(!_panelActivity);
        else if(_panelActivity)
        {
            StartCoroutine(AnimationProductPanel(_panelActivity, _modelType));
        }
    }

    IEnumerator AnimationProductPanel(bool _panelActivity, MODEL_TYPE _modelType)
    {
        imageResource_script.SettingProductImages(_modelType, productIamge);
        Vector3 initVec = Vector3.zero;
        Vector3 purPosVec = productIamge.rectTransform.localScale;
        productIamge.rectTransform.localScale = initVec;
        while (true)
        {
            productIamge.rectTransform.localScale =
                Vector3.Lerp(productIamge.rectTransform.localScale, purPosVec, panelSpeed * Time.deltaTime);

            if(Vector3.Distance(productIamge.rectTransform.localScale,purPosVec) < .01f)
            {
                productIamge.rectTransform.localScale = purPosVec;
                panel_Main.SetActive(!_panelActivity);
                yield break;
            }

     
            yield return null;
        }
    }    

    #endregion

    #endregion

    #region Panel CardNews
    private const int LEFT = -1;
    private const int RIGHT = 1;
    private const int ALLBUTTON = 0;

    [Header("=== Card News ===")]
    [SerializeField] private int allPageCounts;
    [SerializeField] private int curPageIndex;
    [SerializeField] private float initPosX = 2890;
    [SerializeField] private float pageDistance = 1920f;
    [Range(0f, 10f)]
    [SerializeField] private float cardNewsSlideSpeed;

    private bool isClickLeftRightButton;

    public void CardNewsViewOn()
    {
        curPageIndex = 0;
        panel_Main.gameObject.SetActive(false);
        panel_CardNews.gameObject.SetActive(true);
        ActiveButton(ALLBUTTON, true);
        cardNesContainer.localPosition = Vector3.zero.ModifiedX(initPosX);
    }

    public void OnClickButton_Left()
    {
        if (!isClickLeftRightButton)
        {
            isClickLeftRightButton = true;
            StartCoroutine(AnimationCardNews(LEFT));
        }
    }

    public void OnClickButton_Right()
    {
        if (!isClickLeftRightButton)
        {
            isClickLeftRightButton = true;
            StartCoroutine(AnimationCardNews(RIGHT));
        }
    }
    IEnumerator AnimationCardNews(int _leftRight)
    {
        curPageIndex += _leftRight;
        CheckCurButtons(); 
        ChangeUnderIamge();
         Vector3 curVec = cardNesContainer.localPosition;
        Vector3 purPosVec = Vector3.zero;
        if (_leftRight == LEFT) purPosVec = curVec.ModifiedX(curVec.x + pageDistance);
        else if (_leftRight == RIGHT) purPosVec = curVec.ModifiedX(curVec.x - pageDistance);

        Debug.Log(purPosVec);

        while (true)
        {
            cardNesContainer.localPosition = Vector3.Lerp(cardNesContainer.localPosition, purPosVec, cardNewsSlideSpeed * Time.deltaTime);

            if (Vector3.Distance(cardNesContainer.localPosition, purPosVec) < 1f)
            {
                cardNesContainer.localPosition = purPosVec;
                isClickLeftRightButton = false;
                yield break;
            }
            yield return null;
        }
    }

    public void CheckCurButtons()
    {
        if (curPageIndex == 0) ActiveButton(LEFT, false);
        else if (curPageIndex == (allPageCounts - 1)) ActiveButton(RIGHT, false);
        else ActiveButton(ALLBUTTON, true);
    }

    public void ChangeUnderIamge()
    {
        for(int i = 0;i<underImages.Length;i++)
        {
            if (i == curPageIndex) underImages[i].sprite = underClickImage;
            else underImages[i].sprite = underBeforeImage;
        }
    }

    public void ActiveButton(int _leftRight, bool _activity)
    {
        if (_leftRight == LEFT) leftButton.gameObject.SetActive(_activity);
        else if (_leftRight == RIGHT) RightButton.gameObject.SetActive(_activity);
        else
        {
            leftButton.gameObject.SetActive(_activity);
            RightButton.gameObject.SetActive(_activity);
        }
    }

    public void OnClickButton_ClosedCardNesPanel()
    {
        panel_CardNews.gameObject.SetActive(false);
        panel_Main.SetActive(true);
        PlayerMovement.GetInstance().eventTrigger = false;
    }

    #endregion

    #region Panel VideoPlayer

    public void VideoPlayerViewOn()
    {
        panel_Main.SetActive(false);
        VideoPlayerActive(true);
        VideoManager.GetInstance().VideoPlayerViewOn();
    }
 

    public void VideoPlayerActive(bool _activty)
    {
        panel_VideoPlayer.SetActive(_activty);
    }

    public void OnClickButton_ClosedVideoPlayerButton()
    {
        VideoPlayerActive(false);
        PlayerMovement.GetInstance().eventTrigger = false;
        VideoManager.GetInstance().VideoPlayerViewOff();
    }

    #endregion

}