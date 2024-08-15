using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayingGameImage : MonoBehaviour
{
        public bool isYoung;

        private void Start()
        {
                DialogManager.Instance.isYoung = isYoung;
        }
}
