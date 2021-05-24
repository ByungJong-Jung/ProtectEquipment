using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;

public enum IMAGE_TYPE
{
    _1a형식_화학물질용_보호복,
    _4형식_화학물질용_보호복,
    _6형식_화학물질용_보호복,
    _공기호흡기,
    _반면형_호흡보호구,
    _화학물질용_안전장화,
    _전면형_호흡보호구,
    _화학물질용_안전장갑,
    _복합용_및_겸용의_정화통,
    Counts
}

public class ImageResource : MonoBehaviour
{
    [Header("제목 텍스트")]
    public string[] nameTexts;

    [Header("카드보드 이미지")]
    public Sprite[] cardBoards;

    [Header("보드버튼 이미지")]
    public Sprite[] cardBoardButtons;

    [Header("Product Image")]
    public Sprite[] productImages;

    public const int MAX_INDEX = 5;

    #region 스케치팹 유아이 세팅

    public void SettingUi(MODEL_TYPE _modeltype, VIEW_MODEL_TYPE _viewModelType)
    {
        UiManager uiManager_script = UiManager.GetInstance();
        uiManager_script.ResetPos();

        for (int i = 0; i < MAX_INDEX; i++)
        {
            uiManager_script.cardBoardImages[i].gameObject.SetActive(false);
            uiManager_script.cardBoardButtonImages[i].gameObject.SetActive(true);
        }

        switch (_modeltype)
        {
            case MODEL_TYPE.OPEN_DEVICE:
                uiManager_script.nameText.text = nameTexts[(int)_modeltype];
                OPEN_DEVICE(_viewModelType, uiManager_script);
                break;
            case MODEL_TYPE.SEALED_DEVICE:
                uiManager_script.nameText.text = nameTexts[(int)_modeltype];
                SEALED_DEVICE(_viewModelType, uiManager_script);
                break;
            case MODEL_TYPE.LOAD_UNLOAD_DEVICE:        
                uiManager_script.nameText.text = nameTexts[(int)_modeltype];
                LOAD_UNLOAD_DEVICE(_viewModelType, uiManager_script);
                break;
            case MODEL_TYPE.REPAIR_DEVICE:   
                uiManager_script.nameText.text = nameTexts[(int)_modeltype];
                REPAIR_DEVICE(_viewModelType, uiManager_script);
                break;
            case MODEL_TYPE.WASET_TREATMENT_DEVICE:
                uiManager_script.nameText.text = nameTexts[(int)_modeltype];
                WASET_TREATMENT_DEVICE(_viewModelType, uiManager_script);
                break;
            case MODEL_TYPE.TESTWORK_DEVICE:
                uiManager_script.nameText.text = nameTexts[(int)_modeltype];
                TESTWORK_DEVICE(_viewModelType, uiManager_script);
                break;
            case MODEL_TYPE.OTHER_DEVICE:
                uiManager_script.nameText.text = nameTexts[(int)_modeltype];
                OTHER_DEVICE(_viewModelType, uiManager_script);
                break;
            default:
                break;
        }

        uiManager_script.StartLoadAnimation();
    }

    #endregion

    #region 개방형기기작업
    public void OPEN_DEVICE(VIEW_MODEL_TYPE _viewModelType, UiManager _uiManager_script)
    {
        switch (_viewModelType)
        {
            case VIEW_MODEL_TYPE.ALL:
                _uiManager_script.currentCardBoardCount = 2;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._1a형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._공기호흡기];
                _uiManager_script.cardBoardImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._1a형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._공기호흡기];
                _uiManager_script.cardBoardButtonImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            case VIEW_MODEL_TYPE.CLOTHING:
                _uiManager_script.currentCardBoardCount = 1;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._1a형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._1a형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            case VIEW_MODEL_TYPE.MASK:
                _uiManager_script.currentCardBoardCount = 1;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._공기호흡기];
                _uiManager_script.cardBoardImages[1].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._공기호흡기];
                _uiManager_script.cardBoardButtonImages[1].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 밀폐형기기작업
    public void SEALED_DEVICE(VIEW_MODEL_TYPE _viewModelType, UiManager _uiManager_script)
    {
        switch (_viewModelType)
        {
            case VIEW_MODEL_TYPE.ALL:
                _uiManager_script.currentCardBoardCount = 5;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].sprite = cardBoards[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardImages[4].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].sprite = cardBoardButtons[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[4].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                break;
            case VIEW_MODEL_TYPE.CLOTHING:
                _uiManager_script.currentCardBoardCount = 3;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            case VIEW_MODEL_TYPE.MASK:
                _uiManager_script.currentCardBoardCount = 2;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 상하차 및 완료 이송작업
    public void LOAD_UNLOAD_DEVICE(VIEW_MODEL_TYPE _viewModelType, UiManager _uiManager_script)
    {
        switch (_viewModelType)
        {
            case VIEW_MODEL_TYPE.ALL:
                _uiManager_script.currentCardBoardCount = 5;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].sprite = cardBoards[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardImages[4].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].sprite = cardBoardButtons[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[4].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                break;
            case VIEW_MODEL_TYPE.CLOTHING:
                _uiManager_script.currentCardBoardCount = 3;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            case VIEW_MODEL_TYPE.MASK:
                _uiManager_script.currentCardBoardCount = 2;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion
    
    #region 보수작업
    public void REPAIR_DEVICE(VIEW_MODEL_TYPE _viewModelType, UiManager _uiManager_script)
    {
        switch (_viewModelType)
        {
            case VIEW_MODEL_TYPE.ALL:
                _uiManager_script.currentCardBoardCount = 5;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].sprite = cardBoards[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardImages[4].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].sprite = cardBoardButtons[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[4].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                break;
            case VIEW_MODEL_TYPE.CLOTHING:
                _uiManager_script.currentCardBoardCount = 3;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            case VIEW_MODEL_TYPE.MASK:
                _uiManager_script.currentCardBoardCount = 2;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 누출 및 폐기물 처리작업
    public void WASET_TREATMENT_DEVICE(VIEW_MODEL_TYPE _viewModelType, UiManager _uiManager_script)
    {
        switch (_viewModelType)
        {
            case VIEW_MODEL_TYPE.ALL:
                _uiManager_script.currentCardBoardCount = 5;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].sprite = cardBoards[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardImages[4].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].sprite = cardBoardButtons[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[4].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                break;
            case VIEW_MODEL_TYPE.CLOTHING:
                _uiManager_script.currentCardBoardCount = 3;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._4형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            case VIEW_MODEL_TYPE.MASK:
                _uiManager_script.currentCardBoardCount = 2;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 시험작업
    public void TESTWORK_DEVICE(VIEW_MODEL_TYPE _viewModelType, UiManager _uiManager_script)
    {
        switch (_viewModelType)
        {
            case VIEW_MODEL_TYPE.ALL:
                _uiManager_script.currentCardBoardCount = 5;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._6형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].sprite = cardBoards[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardImages[4].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._6형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].sprite = cardBoardButtons[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[4].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                break;
            case VIEW_MODEL_TYPE.CLOTHING:
                _uiManager_script.currentCardBoardCount = 3;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._6형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._6형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            case VIEW_MODEL_TYPE.MASK:
                _uiManager_script.currentCardBoardCount = 2;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._반면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 기타작업
    public void OTHER_DEVICE(VIEW_MODEL_TYPE _viewModelType, UiManager _uiManager_script)
    {
        switch (_viewModelType)
        {
            case VIEW_MODEL_TYPE.ALL:
                _uiManager_script.currentCardBoardCount = 5;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._6형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].sprite = cardBoards[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardImages[4].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._6형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].sprite = cardBoardButtons[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[4].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                break;
            case VIEW_MODEL_TYPE.CLOTHING:
                _uiManager_script.currentCardBoardCount = 3;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._6형식_화학물질용_보호복];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardImages[2].sprite = cardBoards[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._6형식_화학물질용_보호복];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장갑];
                _uiManager_script.cardBoardButtonImages[2].sprite = cardBoardButtons[(int)IMAGE_TYPE._화학물질용_안전장화];
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            case VIEW_MODEL_TYPE.MASK:
                _uiManager_script.currentCardBoardCount = 2;
                _uiManager_script.cardBoardImages[0].sprite = cardBoards[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardImages[1].sprite = cardBoards[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardImages[4].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[0].sprite = cardBoardButtons[(int)IMAGE_TYPE._전면형_호흡보호구];
                _uiManager_script.cardBoardButtonImages[1].sprite = cardBoardButtons[(int)IMAGE_TYPE._복합용_및_겸용의_정화통];
                _uiManager_script.cardBoardButtonImages[2].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[3].gameObject.SetActive(false);
                _uiManager_script.cardBoardButtonImages[4].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion


    #region Product Images 세팅
    public void SettingProductImages(MODEL_TYPE _modelType, Image _panelProductImage)
    {
        _panelProductImage.sprite = productImages[(int)_modelType];
    }
    #endregion




}
