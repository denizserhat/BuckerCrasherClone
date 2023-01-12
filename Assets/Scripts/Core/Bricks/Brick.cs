using UnityEngine;

namespace Core.Bricks
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private bool isExploded;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Ball") && !isExploded)
            {
                isExploded = true;
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(Vector3.left * 10, ForceMode.Impulse);
            }
        }
    }
}
