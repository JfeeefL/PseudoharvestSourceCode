using System.Collections;
using System.Collections.Generic;
using Core.Core;
using Gameplay;
using TMPro;
using UnityEngine;

public class EndManager : MonoBehaviour
{

    private LevelSceneLoad thisLevelSceneLoad;
    
    [SerializeField]
    private TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {

        InputManager.Instance.allowDrag = true;
        thisLevelSceneLoad = GetComponent<LevelSceneLoad>();
        AudioManager.Instance.PlayInController("Victory","Music");
        tmp.text = "Play Again";
    }

    public void NextScene()
    {
        if (InputManager.Instance._raycastHit2D) return;
        AudioManager.Instance.PlayInController("Click","SFX");
        //判断并选择正确场景加载
        thisLevelSceneLoad.JudgyAndSwitchLevel();
    }
}
