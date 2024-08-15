using System.Collections;
using System.Collections.Generic;
using Core.Core;
using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("黑屏")]
    public Image blackScreen;
    public MMFaderRound blackFeedBack;
    [Header("ClickToContinue")]
    public GameObject clickPictrue;
    [Header("计数器")]
    public GameObject countPanel;
    public TextMeshProUGUI countText;
    [Header("重启按钮")] 
    public GameObject reloadButton;
    [Header("跳过对话按钮")]
    public GameObject skipButton;

    [Header("设置按钮")] 
    public GameObject settingButton;

    [Header("滑条")] 
    public Slider MusicSlider,VFXSlider;
    private void OnEnable()
    {
        EventHandler.OnSceneLoadBegin += OnSceneLoadBeginEvent;
        EventHandler.OnSceneLoadEnd += OnSceneLoadEndEvent;
        EventHandler.OnGameBegin += OnGameBegin;
        EventHandler.OnGameEnd+= OnGameEnd;
    }

    private void OnDisable()
    {
        EventHandler.OnSceneLoadBegin -= OnSceneLoadBeginEvent;
        EventHandler.OnSceneLoadEnd -= OnSceneLoadEndEvent;
        EventHandler.OnGameBegin -= OnGameBegin;
        EventHandler.OnGameEnd-= OnGameEnd;
    }

    private void OnGameEnd()
    {
        reloadButton.SetActive(false);
    }

    private void OnGameBegin(float obj)
    {
        reloadButton.SetActive(true);
    }

    #region 场景加载用
    private void OnSceneLoadBeginEvent(string from)
    {
        blackFeedBack.FadeIn(ParameterHandler.FadeTime);
        GameObject.FindWithTag("GlobalLight")?.SetActive(false);
        reloadButton.SetActive(false);
    }

    private void OnSceneLoadEndEvent(string to)
    {
        blackFeedBack.FadeOut(ParameterHandler.FadeTime);
      

    }
    #endregion

}

