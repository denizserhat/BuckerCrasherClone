using UnityEngine;

namespace Core.Ball
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private Ball spawnObject;
        [SerializeField] private float spawnTime;

        private void Start()
        { 
            InvokeRepeating(nameof(Spawn),2f,1f);
        }

        private void Spawn()
        {
            var ball = Instantiate(spawnObject);
            ball.transform.position = transform.position;
        }


        
        
    }
}
