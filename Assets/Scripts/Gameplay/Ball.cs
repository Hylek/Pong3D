using Messages;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class Ball : ExtendedBehaviour
    {
        [SerializeField] private float speed, startAngleX, startAngleY, minSpeed;

        private Rigidbody _rb;
        private bool _isDead;

        protected override void Awake()
        {
            base.Awake();
            
            _rb = GetComponent<Rigidbody>();
        }

        private void Start() => ApplyStartForce();

        private void FixedUpdate()
        {
            if (_isDead) return;
            
            if (_rb.linearVelocity.magnitude < minSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * minSpeed;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("DeadZone"))
            {
                var script = other.gameObject.GetComponent<DeadZone>();
                var side = script.GetSide();
                
                Die();
                Locator.EBus.Publish(new BallEnteredDeadZoneMessage(this, side));
            }
        }

        private void Die()
        {
            _isDead = true;
            _rb.linearVelocity = Vector3.zero;
            Invoke(nameof(ApplyDissolve), .5f);
        }

        private void Reset()
        {
            transform.position = Vector3.zero;
            _rb.linearVelocity = Vector3.zero;
            RevertDissolve();
            Invoke(nameof(ApplyStartForce), 1f);
        }
        
        private void ApplyStartForce()
        {
            _rb.linearVelocity = new Vector3(
                Mathf.Cos(startAngleX) * speed, 0f, Mathf.Sin(startAngleY) * speed);
        }

        private void ApplyDissolve()
        {
            // todo
        }

        private void RevertDissolve()
        {
            // todo
        }
    }
}