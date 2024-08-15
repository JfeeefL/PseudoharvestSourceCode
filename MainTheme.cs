using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class MainTheme : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayInController("MainTheme","Music");
        
    }

    private void OnEnable()
    {
        EventHandler.OnGameEnd += SwitchMusic;
    }

    private void OnDisable()
    {
        EventHandler.OnGameEnd += SwitchMusic;
    }

    void SwitchMusic()
    {
        AudioManager.Instance.GradualStop("Music");
        AudioManager.Instance.GradualPlayInController("End", "Music");
    }
}
