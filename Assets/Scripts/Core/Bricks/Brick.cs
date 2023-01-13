using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Bricks
{
    public class Brick : MonoBehaviour, IExplodable
    {
        [SerializeField] private bool isExploded;
        private Rigidbody _rigidbody;

        public bool IsExploded { get => isExploded; set => isExploded = value; }
        public Rigidbody Body => _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}
