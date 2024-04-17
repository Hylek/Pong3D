using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Gameplay
{
    public class Paddle : ExtendedBehaviour
    {
        [SerializeField] private InputActionAsset paddleMovement;
        
        public float speed;
        public float minY;
        public float maxY;

        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //var movement = Input.GetAxis("Vertical") * speed; // Get input for movement
            
            //movement = Mathf.Clamp(movement, minY - transform.localScale.y / 2, maxY + transform.localScale.y / 2);

            //_rb.MovePosition(_rb.position + new Vector2(0, movement * Time.deltaTime));
        }
    }

}