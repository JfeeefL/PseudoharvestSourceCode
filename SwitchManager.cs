using System.Collections;
using System.Collections.Generic;
using Core.Core;
using Gameplay;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
[RequireComponent(typeof(LevelSceneLoad))]
public class SwitchManager : MonoBehaviour
{
    [SerializeField] private GameObject[] victorySprite, failureSprite;

    private LevelSceneLoad thisLevelSceneLoad;
    
    [SerializeField]
    private TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {

        InputManager.Instance.allowDrag = true;
        thisLevelSceneLoad = GetComponent<LevelSceneLoad>();

        if (GameManager.Instance.NowDectectState == DectectState.Perfect)
        {
            victorySprite.ForEach(s=>s.SetActive(true));
            failureSprite.ForEach(s=>s.SetActive(false));
            tmp.text = "Next Year";
            AudioManager.Instance.PlayInController("Victory","Music");
        }
        else
        {
            tmp.text = "Restart";
            victorySprite.ForEach(s=>s.SetActive(false));
            failureSprite.ForEach(s=>s.SetActive(true));
            AudioManager.Instance.PlayInController("Failure","Music");
        }
    }

    public void NextScene()
    {
        if (InputManager.Instance._raycastHit2D) return;
        AudioManager.Instance.PlayInController("Click","SFX");
        //判断并选择正确场景加载
       thisLevelSceneLoad.JudgyAndSwitchLevel();
    }
}
