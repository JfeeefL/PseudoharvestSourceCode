using System;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

namespace Gameplay.播放动画一系列脚本
{
    public class TimeLinePressAnyToPlay : PlaySelf
    {
        private bool isPressed;
        public PlayableDirector stopDirector;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(3f);
            while (!isPressed)
            {
                if (Input.anyKeyDown)
                {
                    if (stopDirector != null) 
                        stopDirector.Stop();
                    UIManager.Instance.clickPictrue.SetActive(false);
                    PauseDirector();
                    isPressed = true;
                    StartDirector();
                }

                yield return null;
            }
        }
    }
}