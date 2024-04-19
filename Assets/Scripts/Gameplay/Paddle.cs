using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Gameplay
{
    public class Paddle : ExtendedBehaviour
    {
        [SerializeField] private float paddleSpeed;
        [SerializeField] private float minZ, maxZ;

        private PlayerInput _input;
        private InputAction _movement;
        private Vector2 _moveVector;

        protected override void Awake()
        {
            base.Awake();
            
            _input = GetComponent<PlayerInput>();
            
            // todo: Better way than just hard coded string? Will break if action name ever changes (unlikely).
            // todo: Generating a script based on the input map prevents this but is more complex and less powerful (Cannot change schemas)
            _movement = _input.actions["Movement"];
        }

        private void Update()
        {
            _moveVector = _movement.ReadValue<Vector2>();
            var newZ = Mathf.Clamp(transform.position.z + _moveVector.y * paddleSpeed * Time.deltaTime, minZ, maxZ);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
    }

}