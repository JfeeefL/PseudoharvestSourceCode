using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    /// <summary>
    /// 对话事件
    /// </summary>
    public static event Action<bool> OnDialog;

    /// <summary>
    /// 对话事件调用
    /// </summary>
    public static void CallOnDialog(bool isDialoging)
    {
        OnDialog?.Invoke(isDialoging);
    }
    /// <summary>
    /// 场景加载前
    /// </summary>
    public static event Action<string> OnSceneLoadBegin;

    /// <summary>
    /// 场景加载前事件调用
    /// </summary>
    public static void CallOnSceneLoadBegin(string from)
    {
        OnSceneLoadBegin?.Invoke(from);
    }
    /// <summary>
    /// 场景加载后
    /// </summary>
    public static event Action<string> OnSceneLoadEnd;

    /// <summary>
    /// 场景加载后事件调用
    /// </summary>
    public static void CallOnSceneLoadEnd(string to)
    {
        OnSceneLoadEnd?.Invoke(to);
    }
    
    /// <summary>
    /// 游戏开始时
    /// </summary>
    public static event Action<float> OnGameBegin;

    /// <summary>
    /// 游戏开始时调用
    /// </summary>
    public static void CallOnGameBegin(float time)
    {
        OnGameBegin?.Invoke(time);
    }
    /// <summary>
    /// 游戏结束后
    /// </summary>
    public static event Action OnGameEnd;

    /// <summary>
    /// 游戏结束后事件调用
    /// </summary>
    public static void CallOnGameEnd()
    {
        OnGameEnd?.Invoke();
    }
  
    /// <summary>
    /// 游戏暂停时
    /// </summary>
    public static event Action OnGameStop;

    /// <summary>
    /// 游戏暂停时事件调用
    /// </summary>
    public static void CallOnGameStop()
    {
        OnGameStop?.Invoke();
    }
    /// <summary>
    /// 游戏继续时
    /// </summary>
    public static event Action OnGameContinue;

    /// <summary>
    /// 游戏继续时事件调用
    /// </summary>
    public static void CallOnGameContinue()
    {
        OnGameContinue?.Invoke();
    }
}
