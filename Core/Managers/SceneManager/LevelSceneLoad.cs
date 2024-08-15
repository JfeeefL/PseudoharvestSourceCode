using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneLoad : MonoBehaviour
{
    [ValueDropdown("SceneName")]
    public string from;
    [ValueDropdown("SceneName")]
    public string to;

    /// <summary>
    /// 加载下一个关卡
    /// </summary>
    public void LoadNextLevel()
    {
        SceneController.Instance.GotoScene(from,to);    
    }
/// <summary>
/// 通过游玩情况加载当前场景或者是下一关卡
/// </summary>
    public void JudgyAndSwitchLevel()
    {
        if(GameManager.Instance.NowDectectState==DectectState.Perfect)
            SceneController.Instance.GotoScene(from,to); 
        else
            SceneController.Instance.GotoScene(from,GameManager.Instance.LastScene); 
            
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
