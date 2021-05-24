using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum QULITY_TYPE
{
    MIDDLE,
    HIGH,
    LOW
}
public class StartScene : MonoBehaviour
{
    [Header ("Panels")]
    public GameObject Panel_QuiltySetting;
    public GameObject Panel_LoadingScene;

    [Header("Loading Buttons")]
    public Slider progressbar;
    public GameObject startButton;
    bool nextScene;
    public Image fill;

    [Header("Performence Buttons")]
    public QULITY_TYPE curQulityState;
    [SerializeField] private Button highButton;
    [SerializeField] private Button middleButton;
    [SerializeField] private Button lowButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button NoButton;

    public AllIn1Shader[] buttonEffectScript;
    public string[] names;
    public string[] qulityNames;
    public Text infoText;

    public GameObject qulityButtons;
    public GameObject checkBoard;
    public void Init_StartScene()
    {
        highButton.onClick.AddListener(OnClickButton_HighButton);
        middleButton.onClick.AddListener(OnClickButton_MiddleButton);
        lowButton.onClick.AddListener(OnClickButton_LowButton);
        yesButton.onClick.AddListener(OnClickButton_YES);
        NoButton.onClick.AddListener(OnClickButton_NO);

        qulityNames = new string[3];
        for (int i = 0; i < qulityNames.Length; i++)
            qulityNames[i] = QualitySettings.names[i];

        names = new string[3];
        names[0] = "일반화질";
        names[1] = "고화질";
        names[2] = "저화질";

        progressbar.gameObject.SetActive(false);
    }
    void Start()
    {
        Init_StartScene();
    }
    public void OnClickButton_YES()
    {
        QualitySettings.SetQualityLevel((int)curQulityState, true);
        Panel_QuiltySetting.SetActive(false);
        Panel_LoadingScene.SetActive(true);
    }
    public void OnClickButton_NO()
    {
        checkBoard.SetActive(false);
        qulityButtons.SetActive(true);
        PointEnter_noButton(false);
    }

    public void OnClickButton_HighButton()
    {
        infoText.text = names[(int)QULITY_TYPE.HIGH] + "로 진행 하시겠습니까?";
        curQulityState = QULITY_TYPE.HIGH;
        StartCoroutine(Animation());
    }
    public void OnClickButton_MiddleButton()
    {
        infoText.text = names[(int)QULITY_TYPE.MIDDLE] + "로 진행 하시겠습니까?";
        curQulityState = QULITY_TYPE.MIDDLE;
        StartCoroutine(Animation());
    }
    public void OnClickButton_LowButton()
    {
        infoText.text = names[(int)QULITY_TYPE.LOW] + "로 진행 하시겠습니까?";
        curQulityState = QULITY_TYPE.LOW;
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        qulityButtons.SetActive(false);
        checkBoard.SetActive(true);
        PointEnter_HighButton(false);
        Vector3 purPosVec = checkBoard.transform.localScale;
        checkBoard.transform.localScale = Vector3.zero;
        float speed = 9f;
        while (true)
        {
            checkBoard.transform.localScale = Vector3.Lerp(checkBoard.transform.localScale, purPosVec, speed * Time.deltaTime);

            if (Vector3.Distance(checkBoard.transform.localScale, purPosVec) < 0.1f)
            {
                checkBoard.transform.localScale = purPosVec;
                yield break;
            }

            yield return null;
        }
    }


    public void PointEnter_HighButton(bool _activity)
    {
        buttonEffectScript[0].SetKeyword("INNEROUTLINE_ON", _activity);
    }
    public void PointEnter_MiddleButton(bool _activity)
    {
        buttonEffectScript[1].SetKeyword("INNEROUTLINE_ON", _activity);
    }
    public void PointEnter_LowButton(bool _activity)
    {
        buttonEffectScript[2].SetKeyword("INNEROUTLINE_ON", _activity);
    }
    public void PointEnter_yesButton(bool _activity)
    {
        buttonEffectScript[3].SetKeyword("INNEROUTLINE_ON", _activity);
    }
    public void PointEnter_noButton(bool _activity)
    {
        buttonEffectScript[4].SetKeyword("INNEROUTLINE_ON", _activity);
    }


    public void OnClickStartButton()
    {
        progressbar.gameObject.SetActive(true);
        startButton.SetActive(false);
        StartCoroutine(LoadNextScene());
    }


    IEnumerator LoadNextScene()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        while (!operation.isDone && !operation.allowSceneActivation)
        {
            yield return null;

            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }
            else if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if (progressbar.value >= 1f)
            {

                yield return new WaitForSeconds(.5f);
                nextScene = true;

            }

            if (nextScene && progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }

        yield return null;
    }

}