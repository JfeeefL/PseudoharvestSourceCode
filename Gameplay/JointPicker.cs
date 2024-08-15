using UnityEngine;

namespace Gameplay
{
    public class JointPicker : MonoBehaviour, IPickable
    {
        private DraggablePicker _parentDraggable;
        private void Start()
        {
            _parentDraggable = transform.parent.gameObject.GetComponent<DraggablePicker>();
        }

        public void OnHover()
        {
            _parentDraggable.OnHover();
        }

        public void OnFade()
        {
            _parentDraggable.OnFade();
        }
    }
}
