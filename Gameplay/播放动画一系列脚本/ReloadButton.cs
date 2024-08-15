using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour
{
    
    public void ReloadScene()
    {
        if (SceneController.Instance.canPress)
        {
            AudioManager.Instance.PlayInController("Click", "SFX");
            SceneController.Instance.canPress = false;
            UIManager.Instance.countPanel.SetActive(false);
            SceneController.Instance.GotoScene(SceneManager.GetActiveScene().name,"ReloadScene");
            GameManager.Instance.NowGameState = GameState.Animation;
        }
        
    }
}
