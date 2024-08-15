using System.Collections;
using Core.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public bool canPress=true;
    
    /// <summary>
    /// 加载To,卸载from,执行加载卸载方法.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public void GotoScene(string from, string to)
    {
        StopAllCoroutines();
        StartCoroutine(SceneLoad(from, to));
    }
    
    private IEnumerator SceneLoad(string from, string to)
    {
        GameManager.Instance.LastScene = from;
        EventHandler.CallOnSceneLoadBegin(from);
        yield return null;
        yield return new WaitForSeconds(ParameterHandler.FadeTime);
        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);
        if(from!="")
            yield return SceneManager.UnloadSceneAsync(from);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(to));
        yield return new WaitForSeconds(ParameterHandler.LoadingTime);
        yield return new WaitForSeconds(ParameterHandler.FadeTime);
        yield return null;
        EventHandler.CallOnSceneLoadEnd(to);

    }

}
