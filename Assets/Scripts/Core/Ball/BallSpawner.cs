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
        
        private Transform _drawTransform;
        private bool _isDrawing = true;
        private CancellationTokenSource _drawingCancellationTokenSource;
        private GameState _currentState;
        
        private void OnEnable()
        {
            DrawMesh.onStartDraw += StartDraw;
            DrawMesh.onFinishDraw += EndDraw;
            GameStateHandler.onStateChanged += OnStateChanged;
        }
        
        private void OnDisable()
        {
            DrawMesh.onStartDraw -= StartDraw;
            DrawMesh.onFinishDraw -= EndDraw;
            GameStateHandler.onStateChanged -= OnStateChanged;
        }

        private void Start()
        {
            BallPool = new Pool<Ball>(new PrefabFactory<Ball>(spawnObject.gameObject, transform, "Ball"));
            _drawingCancellationTokenSource = new CancellationTokenSource();
            Spawn();
        }

        private void StartDraw()
        {
            _isDrawing = true;
            _drawingCancellationTokenSource.Cancel();
        }

        private void OnStateChanged(GameState newState)
        {
            _currentState = newState;
        }
        
        private void EndDraw(Transform drawObject)
        {
            _drawTransform = drawObject;
            _isDrawing = false;
            _drawingCancellationTokenSource = new CancellationTokenSource();
            Spawn();
        }

        private async void Spawn()
        {
            while (!_isDrawing && _currentState != GameState.Finished)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(spawnTime), cancellationToken: _drawingCancellationTokenSource.Token).SuppressCancellationThrow();
                if (!_drawingCancellationTokenSource.Token.IsCancellationRequested)
                {
                    var ball = BallPool.Allocate();
                    ball.gameObject.SetActive(true);
                    ball.transform.position = new Vector3(transform.position.x,transform.position.y,_drawTransform.position.z);
                    
                }
  
            }
        }
    }
}
