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
        }

        private void Start() => ApplyStartForce();

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
            Locator.EBus.Publish(new BallEnteredDeadZoneMessage(this, side));
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
        }
        
        private void OnBallDissolveComplete()
        {
            Invoke(_isDead ? nameof(ResetBall) : nameof(ApplyStartForce), 1f);
        }
        
        private void ApplyStartForce()
        {
            var randomAngle = Random.Range(startAngleX, startAngleY);
            var randomAngleRadians = randomAngle * Mathf.Deg2Rad;
            
            var randomDirection = Random.Range(0f, 1f) > 0.5f;
            
            var xVelocity = Mathf.Cos(randomAngleRadians) * (randomDirection ? 1f : -1f) * speed;
            var zVelocity = Mathf.Sin(randomAngleRadians) * speed;

            var startVector = new Vector3(xVelocity, 0f, zVelocity);
            
            Debug.Log($"Start Vector: {startVector}");
            
            _rb.linearVelocity = startVector;
        }

        private void ApplyDissolve() => _dissolver.StartDissolve(DissolveState.Dissolve);

        private void RevertDissolve() => _dissolver.StartDissolve(DissolveState.Restore);
    }
}