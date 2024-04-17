using Core;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class DeadZone : ExtendedBehaviour
    {
        [SerializeField] private PongSide side;
        [SerializeField] private bool showGizmo;

        private void OnDrawGizmos()
        {
            if (!showGizmo) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }

        public PongSide GetSide() => side;
    }
}