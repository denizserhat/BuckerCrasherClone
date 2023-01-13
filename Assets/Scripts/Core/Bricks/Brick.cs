using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Bricks
{
    public class Brick : MonoBehaviour, IExplodable
    {
        public static event Action<Brick> onExploaded;
        
        public bool isExploded;
        private Rigidbody _rigidbody;
        
        
        public bool IsExploded
        {
            get => isExploded;
            set { isExploded = value; onExploaded?.Invoke(this); }
        }

        public Rigidbody Body => _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnBecameInvisible()
        {
            // ToDo(dnz) UI Animation add
            Destroy(gameObject);
        }
    }
}
