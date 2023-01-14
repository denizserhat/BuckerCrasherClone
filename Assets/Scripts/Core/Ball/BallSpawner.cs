using System;
using System.Threading;
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
        
        public Pool<Ball> BallPool;

        private bool _isDrawing;
        private CancellationTokenSource _drawingCancellationTokenSource;
        private GameManager _manager;


        private void Awake()
        {
            _manager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            BallPool = new Pool<Ball>(new PrefabFactory<Ball>(spawnObject.gameObject, transform, "Ball"));
            _drawingCancellationTokenSource = new CancellationTokenSource();
            Spawn();
        }
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
        private void StartDraw()
        {
            _isDrawing = true;
            _drawingCancellationTokenSource.Cancel();
        }

        private void EndDraw()
        {
            _isDrawing = false;
            _drawingCancellationTokenSource = new CancellationTokenSource();
            Spawn();
        }

        private async void Spawn()
        {
            while (!_isDrawing && _manager.gameState != GameState.Finished)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(spawnTime), cancellationToken: _drawingCancellationTokenSource.Token).SuppressCancellationThrow();
                if (!_drawingCancellationTokenSource.Token.IsCancellationRequested)
                {
                    var ball = BallPool.Allocate();
                    ball.gameObject.SetActive(true);
                    ball.transform.position = transform.position;
                }
  
            }
        }
    }
}
