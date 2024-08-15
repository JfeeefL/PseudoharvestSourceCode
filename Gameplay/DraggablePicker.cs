using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class DraggablePicker : MonoBehaviour, IPickable
    {
        [SerializeField]
        private float maxOffset = 0.002f;
        
        private float _offset, _target;
        
        private Material _material;

        private bool _approaching;

        [SerializeField] private string shaderOffset = "_OutLineOffset";

        private void Start()
        {
            _material = GetComponent<Renderer>().material;
            _material.SetFloat(shaderOffset, 0);
            _offset = 0;
        }

        private readonly WaitForFixedUpdate _period = new WaitForFixedUpdate();
        
        IEnumerator MoveTo()
        {
            _approaching = true;
            while (Math.Abs(_offset - _target) > 1e-6)
            {
                if(!gameObject) yield break;
                _offset = Mathf.MoveTowards(_offset, _target, maxOffset*5*Time.deltaTime);
                _material.SetFloat(shaderOffset, _offset);
                yield return _period;
            }
            
            _material.SetFloat(shaderOffset, _target);

            _approaching = false;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void OnHover()
        {
            if (!gameObject) return;
            _target = maxOffset;
            if (!_approaching) StartCoroutine(MoveTo());
        }

        public void OnFade()
        {
            if (!gameObject) return; 
            _target = 0;
            if (!_approaching) StartCoroutine(MoveTo());
        }
    }
}