using System;
using System.Collections;
using Core;
using Core.Core;
using Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechanics
{
    public class DragAnchor : MonoBehaviour
    {
        private TargetJoint2D _hingeJoint2D;

        private Camera _mainCamera;

        InputManager _playerInput;

        [SerializeField]
        private string soundType;

        private float lastAudio;

        private float allowedAudioPeriod = 0.4f;
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (soundType.Length == 0 || col.gameObject.CompareTag("Rice")) return;
            float t = Time.time;
            if (lastAudio + allowedAudioPeriod > t)
            {
                lastAudio = t;
                return;
            }

            lastAudio = t;
            AudioManager.Instance.PlayInController(soundType, "SFX");
        }

        private void Start()
        {
            _playerInput = InputManager.Instance;
        }

        public void OnClicked()
        {
            _hingeJoint2D = gameObject.AddComponent<TargetJoint2D>();
            _hingeJoint2D.anchor = transform.InverseTransformPoint(_playerInput.mouseWorldPosition);
            StartCoroutine(FollowAnchor());
        }

        public void OnReleased()
        {
            Destroy(_hingeJoint2D);
            StopAllCoroutines();
        }

        IEnumerator FollowAnchor()
        {
            while (true)
            {
                _hingeJoint2D.target = _playerInput.mouseWorldPosition;
                yield return null;
            }
        }
        
    }
}
