using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mechanics
{
    public class clothRenderer : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _renderer;
        [SerializeField] private List<Transform> _list;

        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<LineRenderer>();
            _renderer.positionCount = _list.Count;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateRenderer();
        }

        void UpdateRenderer()
        {
            _renderer.positionCount = _list.Count;
            _renderer.SetPositions(_list.Select(p=>p.position).ToArray());
        }

        private void OnDrawGizmos()
        {
            UpdateRenderer();
        }
    }
}
