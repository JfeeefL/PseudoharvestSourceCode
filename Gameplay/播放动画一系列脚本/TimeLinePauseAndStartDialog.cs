using UnityEngine;

namespace Gameplay.播放动画一系列脚本
{
    public class TimeLinePauseAndStartDialog : PlaySelf
    {
        public void PauseTimeAndStartDialog()
        {
            GameManager.Instance.DetectRice();
            StartDialog();
        }
    }
}