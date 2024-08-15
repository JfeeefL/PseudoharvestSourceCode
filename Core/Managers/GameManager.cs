using System;
using System.Collections;
using System.Collections.Generic;
using Core.Core;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DectectState NowDectectState=DectectState.Nothing;
    public GameState NowGameState=GameState.Playing;

    public RiceDetector riceDetector;
    [ValueDropdown("SceneName")]
    public string FirstLoadScene;
    //结算界面不能暂停（特定给了个Settlement表示正在结算），对话界面可以暂停

    public string LastScene;
    public float GameCountTime;

    private void Start()
    {
        SceneController.Instance.GotoScene("",FirstLoadScene);
    }

    private void OnEnable()
    {
        EventHandler.OnGameBegin += OnGameBegin;
        EventHandler.OnGameEnd += OnGameEnd;
    }

    private void OnDisable()
    {
        EventHandler.OnGameBegin -= OnGameBegin;
        EventHandler.OnGameEnd -= OnGameEnd;
    }

    private void OnGameEnd()
    {
        NowGameState = GameState.Animation;
        DetectRice();
    }

    private void OnGameBegin(float obj)
    {
        NowGameState = GameState.Playing;
    }

    /// <summary>
    /// 检测有无大米
    /// </summary>
    public void DetectRice()
    {
        NowDectectState = DectectState.Nothing; 
        if(riceDetector!=null)
            StartCoroutine(riceDetector.DetectAll());
    }
    #region 场景名获取
#if UNITY_EDITOR
    private static IEnumerable SceneName()
    {
        List<string> stringList = new List<string>(); //新列表（空）
        
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                //scene.enabled 是否在中启用此场景，以获取如何使用此类的示例。另请参见：EditorBuildSettingsScene、EditorBuildSettings.scenes
                string str1 = scene.path.Substring(scene.path.LastIndexOf('/') + 1); //获取尾地址场景名字（xxx.asset）
                string str2 = str1.Substring(0, str1.Length - 6); //只取除.asset 6个字符以外的名字
                stringList.Add(str2);
            }
        }

        return stringList;
    }
#endif
    #endregion

}
