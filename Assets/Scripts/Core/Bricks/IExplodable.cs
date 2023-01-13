using UnityEngine;

namespace Core.Bricks
{
    public interface IExplodable
    {
        public bool IsExploded { get; set; }
        public Rigidbody Body { get; }
    }
}
