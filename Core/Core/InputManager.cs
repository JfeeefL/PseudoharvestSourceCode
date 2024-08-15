using System;
using System.Net;
using Gameplay;
using Mechanics;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using RaycastHit2D = UnityEngine.RaycastHit2D;

namespace Core.Core
{
    public class InputManager : MonoBehaviour
    {
        static public InputManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private Camera _mainCamera;

        private DragAnchor _target = null;

        [HideInInspector]
        public bool isPressed = false;
        
        [HideInInspector]
        public Vector2 mouseWorldPosition, mousePosition;

        private Ray _mouseRay;

        public RaycastHit2D _raycastHit2D;

        private IPickable _pickableTarget;

        private bool _isPicking;

        
        public bool allowDrag = false;

        [SerializeField] private Texture2D cursorPoint, cursorPick, cursorHover;
        private void Start()
        {
            //allowDrag = false;
            Cursor.SetCursor(cursorPoint,Vector2.zero, CursorMode.Auto);
        }
        
        public event UnityAction OnReleased = delegate {  }, OnClicked = delegate {  };
        private void OnEnable()
        {
            OnClicked += DragCheck;
            OnReleased += ReleaseDrag;
            EventHandler.OnGameBegin+=OnGameBegin;
            EventHandler.OnDialog += OnDialog;
            EventHandler.OnGameEnd+=OnGameEnd;
            EventHandler.OnGameContinue += OnGameContinue;
            EventHandler.OnGameStop += OnGameStop;
            Debug.Log("Pickable Enabled");
            _pickableTarget = null;
        }

        private void OnDisable()
        {
            OnClicked -= DragCheck;
            OnReleased -= ReleaseDrag;
            EventHandler.OnGameBegin-=OnGameBegin;
            EventHandler.OnDialog -= OnDialog;
            EventHandler.OnGameEnd-=OnGameEnd;
            EventHandler.OnGameContinue += OnGameContinue;
            EventHandler.OnGameStop += OnGameStop;
            Debug.Log("Pickable Destroyed.");
            Instance._pickableTarget = null;
        }

        private void OnGameStop()
        {
            allowDrag = false;
        }

        private void OnGameContinue()
        {
            allowDrag = true;
        }

        private void OnGameEnd()
        {
            allowDrag = false;
        }

        private void OnDialog(bool obj)
        {
            if(GameManager.Instance.NowGameState==GameState.Playing)
                allowDrag = !obj;
        }

        private void OnGameBegin(float obj)
        {
            allowDrag = true;
        }

        void PickProcess()
        {
            if (_isPicking) return;
            _raycastHit2D = Physics2D.Raycast(_mouseRay.origin, _mouseRay.direction);
            
            if (_raycastHit2D)
            {
                var nowTarget = _raycastHit2D.collider.GetComponent<IPickable>();
                //if (_pickableTarget == null && nowTarget == null) return;
                //if (_pickableTarget == null) _pickableTarget = nowTarget;
                if (nowTarget != _pickableTarget&& nowTarget!=null)
                {
                    if(nowTarget != null)
                        Cursor.SetCursor(cursorHover,Vector2.zero, CursorMode.Auto);
                    _pickableTarget?.OnFade();
                    nowTarget?.OnHover();
                    _pickableTarget = nowTarget;
                }
            }
            else
            {
                Cursor.SetCursor(cursorPoint,Vector2.zero, CursorMode.Auto);
                _pickableTarget?.OnFade();
                _pickableTarget = null;
            }
        }
        private void Update()
        {
            mousePosition = Mouse.current.position.ReadValue();
            _mouseRay = Camera.main.ScreenPointToRay(mousePosition);
            mouseWorldPosition = _mouseRay.origin;
            
            PickProcess();
            
            if (Input.GetMouseButton(0))
            {
                if (!isPressed) OnClicked.Invoke();
                isPressed = true;
            }
            else
            {
                if (isPressed) OnReleased.Invoke();
                isPressed = false;
            }
        }
        
        public bool keyOut = false; 
        private void DragCheck()
        {
            if (!allowDrag) return;
            if (!_raycastHit2D) return;
            if (_raycastHit2D.collider.CompareTag("Talkable"))
            {
                DialogManager.Instance.BeginDialog(null);
                return;
            }
            _target = _raycastHit2D.collider.gameObject.GetComponent<DragAnchor>();
            if(_target)
            {
                AudioManager.Instance.PlayInController("Pick","SFX");
                _isPicking = true;
                Cursor.SetCursor(cursorPick,Vector2.zero, CursorMode.Auto);
                _target.OnClicked();
            }
        }

        private void ReleaseDrag()
        {
            _isPicking = false;
            if(_target == null) return;
            _target.OnReleased();
        }
    }
}