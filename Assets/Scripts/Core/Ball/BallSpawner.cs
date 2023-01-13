using System;
using Core.Draw;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility.Pool;
using Utility.Pool.Factory;

namespace Core.Ball
{
    public class BallSpawner : MonoBehaviour
    {
        
        [SerializeField] private Ball spawnObject;
        [SerializeField] private float spawnTime;
        
        public Pool<Ball> ballPool;

        private bool isDrawing;
        private void OnEnable()
        {
            DrawMesh.onStartDraw += StartDraw;
            DrawMesh.onFinishDraw += EndDraw;
        }
        private void OnDisable()
        {
            DrawMesh.onStartDraw -= StartDraw;
            DrawMesh.onFinishDraw -= EndDraw;
        }
        private void Start()
        {
            ballPool = new Pool<Ball>(new PrefabFactory<Ball>(spawnObject.gameObject, transform, "Ball"));
            Spawn();
        }

        private void StartDraw()
        {
            isDrawing = true;
        }

        private void EndDraw()
        {
            isDrawing = false;
            Spawn();
        }

        private async void Spawn()
        {
            while (!isDrawing)
            {
                var ball = ballPool.Allocate();
                ball.gameObject.SetActive(true);
                ball.transform.position = transform.position;

                await UniTask.Delay(TimeSpan.FromSeconds(spawnTime));
            }

        }


        
        
    }
}
