using Messages;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class PaddleAI : ExtendedBehaviour
    {
        [SerializeField] private float speed, movementRange;
        [SerializeField] private float ballDistanceToMove;
        [SerializeField] private float minZ, maxZ;

        private bool _canMove;
        private GameObject _ball;

        protected override void Awake()
        {
            base.Awake();

            _ball = GameObject.FindGameObjectWithTag("Ball");
            
            _canMove = true;
            
            Subscribe<BallResetMessage>(OnBallReset);
            Subscribe<BallEnteredDeadZoneMessage>(OnBallDead);
        }

        private void FixedUpdate()
        {
            if (!_canMove) return;
             
            if (!IsBallWithinDistance(_ball.transform.position.x, ballDistanceToMove)) return;
            
            var desiredZ = Mathf.Clamp(_ball.transform.position.z, 
                transform.position.z - movementRange / 2f, 
                transform.position.z + movementRange / 2f);
            
            desiredZ = Mathf.Clamp(desiredZ, minZ, maxZ);
            
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(transform.position.x, transform.position.y, desiredZ), speed * Time.deltaTime);
        }
        
        private bool IsBallWithinDistance(float ballX, float triggerDistance)
        {
            var distanceToPaddle = Mathf.Abs(ballX - transform.position.x);
            
            return distanceToPaddle < triggerDistance;
        }
        
        private void OnBallDead(BallEnteredDeadZoneMessage obj)
        {
            _canMove = false;
        }
        
        private void OnBallReset(BallResetMessage obj)
        {
            _canMove = true;
        }
    }
}