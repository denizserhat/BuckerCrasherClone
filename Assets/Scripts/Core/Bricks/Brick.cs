using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Bricks
{
    public class Brick : MonoBehaviour, IExplodable
    {
        public static event Action<Brick> onExploaded;
        public Rigidbody Body => _rigidbody;

        private bool _isExploded;
        private Rigidbody _rigidbody;
        private bool _isGround;
        
        public bool IsExploded
        {
            get => _isExploded;
            set { _isExploded = value; onExploaded?.Invoke(this); }
        }
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            if (_isExploded && _isGround)
            {
                _rigidbody.AddForce(Vector3.left*30);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Ground"))
            {
                _isGround = true;
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
