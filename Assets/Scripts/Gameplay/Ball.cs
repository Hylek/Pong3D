using System;
using Messages;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class Ball : ExtendedBehaviour
    {
        [SerializeField] private float speed, startAngleX, startAngleY, minSpeed;

        private Rigidbody _rb;
        private BallDissolver _dissolver;
        private bool _isDead;
        private readonly Vector3 _resetVector = new Vector3(0f, 0.55f, 0f);

        protected override void Awake()
        {
            base.Awake();
            
            _rb = GetComponent<Rigidbody>();
            _dissolver = new BallDissolver(GetComponent<Renderer>().material);
            _dissolver.DissolveComplete += OnBallDissolveComplete;
            
            Subscribe<StartGameMessage>(OnStartGame);
        }

        private void Update() => _dissolver.Update();

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
            if (_isDead) return;
            
            if (!other.gameObject.CompareTag("DeadZone")) return;
            
            var script = other.gameObject.GetComponent<DeadZone>();
            var side = script.GetSide();
                
            Debug.Log($"Ball has hit the {side} dead-zone!");
                
            Die();
            Locator.EventHub.Publish(new BallEnteredDeadZoneMessage(this, side));
        }
        
        private void OnStartGame(StartGameMessage message)
        {
            Invoke(nameof(ApplyStartForce), .5f);
        }

        private void Die()
        {
            _isDead = true;
            _rb.linearVelocity = Vector3.zero;
            Invoke(nameof(ApplyDissolve), .5f);
        }

        private void ResetBall()
        {
            _isDead = false;
            transform.position = _resetVector;
            _rb.linearVelocity = Vector3.zero;
            RevertDissolve();
            Locator.EventHub.Publish(new BallResetMessage());
        }
        
        private void OnBallDissolveComplete()
        {
            Invoke(_isDead ? nameof(ResetBall) : nameof(ApplyStartForce), 1f);
        }
        
        private void ApplyStartForce()
        {
            var direction = Random.onUnitSphere;
            var randomSpeed = Random.Range(minSpeed, speed);
            
            // Clamp direction x position so ball does not get stuck.
            var clampedX = Mathf.Max(4f * Mathf.Sign(direction.x));
            direction.x = clampedX;

            _rb.linearVelocity = direction.normalized * randomSpeed;
        }

        private void ApplyDissolve() => _dissolver.StartDissolve(DissolveState.Dissolve);

        private void RevertDissolve() => _dissolver.StartDissolve(DissolveState.Restore);
    }
}