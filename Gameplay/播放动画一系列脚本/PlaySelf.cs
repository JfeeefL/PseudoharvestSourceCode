using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[ExecuteInEditMode]
[RequireComponent(typeof(PlayableDirector))]
public class PlaySelf : MonoBehaviour
{
    public PlayableDirector m_Director;
    public DialogContent Dialog_SO;

    private void Start()
    {
        m_Director = GetComponent<PlayableDirector>();
    }

    public void StartDirector()
    {
        TimeLineManager.Instance.PlayTimeLine(m_Director);
    }

    public void PauseDirector()
    {
        TimeLineManager.Instance.PauseTimeLine(m_Director);
    }
    public void StartDialog()
    {
        DialogManager.Instance.BeginDialog(Dialog_SO);
    }
    
}
