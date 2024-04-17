using UnityEngine;
using Utils;

namespace Gameplay
{
    public enum DeadZoneSide
    {
        Left, Right
    }
    
    public class DeadZone : ExtendedBehaviour
    {
        [SerializeField] private DeadZoneSide side;
        [SerializeField] private bool showGizmo;

        private void OnDrawGizmos()
        {
            if (!showGizmo) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }

        public DeadZoneSide GetSide() => side;
    }
}