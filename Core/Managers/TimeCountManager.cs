using System;
using System.Collections;
using System.Collections.Generic;
using Core.Core;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountManager : Singleton<TimeCountManager>
{
    public float TotalTime;
    public bool isBeginCount;
    private bool isPause;
    private void OnEnable()
    {
        EventHandler.OnGameBegin += OnGameBeginEvent;
        EventHandler.OnDialog += OnDialog;
        EventHandler.OnGameEnd += OnGameEnd;
        EventHandler.OnGameContinue += OnGameContinue;
        EventHandler.OnGameStop += OnGameStop;
        EventHandler.OnSceneLoadBegin += OnSceneLoadBegin;
        EventHandler.OnSceneLoadEnd += OnSceneLoadEnd;
    }

    private void OnDisable()
    {
        EventHandler.OnGameBegin -= OnGameBeginEvent;
        EventHandler.OnDialog -= OnDialog;
        EventHandler.OnGameEnd -= OnGameEnd;
        EventHandler.OnGameContinue -= OnGameContinue;
        EventHandler.OnGameStop -= OnGameStop;
        EventHandler.OnSceneLoadBegin -= OnSceneLoadBegin;
        EventHandler.OnSceneLoadEnd -= OnSceneLoadEnd;
    }

    private void OnSceneLoadEnd(string obj)
    {
        isPause = true;
    }


    private void OnSceneLoadBegin(string obj)
    {
        if(countCor!=null)
            StopCoroutine(countCor);
        isPause = false;
    }


    private Coroutine countCor;

    private void OnGameStop()
    {
        isPause = true;
    }

    private void OnGameContinue()
    {
        isPause = false;
    }

    private void OnGameEnd()
    {
        TotalTime = 0;
       
    }

    private void OnDialog(bool obj)
    {
        isPause = obj;
    }
   
    private void OnGameBeginEvent(float time)
    {
        CountDown(time); 
    }


    private void Start()
    {
        //CountDown(50);
    }

    public void CountDown(float time)
    {
        countCor= StartCoroutine(CountDownIenum(time));
    }
    IEnumerator CountDownIenum(float time)
    {
        isBeginCount = true;
        UIManager.Instance.countPanel.SetActive(true);
        TotalTime = time;
        while (TotalTime>=0)
        {
            if(!isPause)
                TotalTime -= Time.deltaTime;
            UIManager.Instance.countText.text=(int)TotalTime+"s";
            yield return null;
        }
        UIManager.Instance.countPanel.SetActive(false);
        EventHandler.CallOnGameEnd();
        isBeginCount = false;
    }
}
