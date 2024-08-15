using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BlackMiss : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(StartGameFade),3f);
    }

    public void StartGameFade()
    {
        UIManager.Instance.blackScreen.DOColor(new Color(0, 0, 0, 0), ParameterHandler.FadeTime).OnComplete(
            ()=>  UIManager.Instance.blackScreen.gameObject.SetActive(false));
    }
}
