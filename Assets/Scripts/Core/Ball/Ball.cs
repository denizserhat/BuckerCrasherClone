using System;
using Core.Bricks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Ball
{
    public class Ball : MonoBehaviour
    {
        public Rigidbody Body => _rigidbody;
        
        [SerializeField] private float physicsAssistThickness;
        [SerializeField] private float randomAssistValue;
        [SerializeField] private LayerMask layerMask;
        
        private Rigidbody _rigidbody;



        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Brick"))
            {
                Explode();
            }

            if (collision.transform.CompareTag("Platform"))
            {
                _rigidbody.AddForce((new Vector3(10, 2) * Random.Range(1,randomAssistValue)) * physicsAssistThickness);
            }
        }

        private void Explode()
        {
            Collider[] exploables = Physics.OverlapSphere(transform.position, 1.3f,layerMask);

            foreach (Collider explodable in exploables)
            {
                if (explodable.TryGetComponent<IExplodable>(out var brick) && !brick.IsExploded)
                {
                    brick.IsExploded = true;
                    brick.Body.isKinematic = false;
                    brick.Body.AddForce(Vector3.left * 2, ForceMode.Impulse);
                }
                explodable.gameObject.layer = 9; // Exploded Layer
            }
        }

    }
}
