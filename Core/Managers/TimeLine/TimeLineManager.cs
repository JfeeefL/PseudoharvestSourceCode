using System;
using System.Collections;
using System.Collections.Generic;
using Core.Core;
using Gameplay.播放动画一系列脚本;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

public class TimeLineManager : Singleton<TimeLineManager>
{
    public PlayableDirector currentDiretor;

    private void OnEnable()
    {
        EventHandler.OnGameEnd += OnGameEnd;
    }

    private void OnDisable()
    {
        EventHandler.OnGameEnd -= OnGameEnd;
    }

    private void OnGameEnd()
    {
        FindObjectOfType<TimeLinePauseAndStartDialog>().StartDirector();
    }


    public void PlayTimeLine(PlayableDirector director)
    {
        if(currentDiretor!=null)
            currentDiretor.Stop();
        currentDiretor = director;
        director.Play();
    }
    public void PauseTimeLine(PlayableDirector director)
    {
        director.Pause();
    }
    public void ResumeTimeLine(PlayableDirector director)
    {
        director.Resume();
    }
}
