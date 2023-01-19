using System;
using Core.UI;
using UnityEngine;

namespace Core.Converter
{
    public class BrickConverter : MonoBehaviour
    {
        public static event Action<Vector3> onMoneyUpdate;
        public static void OnMoneyUpdate(Vector3 startPos) => onMoneyUpdate?.Invoke(startPos);
        
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private Transform coinSpawnPoint;
        
        private int _brickCount;
        private GameManager _manager;
        private Camera _mainCam;

        private void Awake()
        {
            _manager = FindObjectOfType<GameManager>();
            _mainCam = Camera.main;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Brick"))
            {
                Destroy(collision.gameObject);
                _brickCount++;
                CreateCoin();
            }
        }
        
        private void CreateCoin()
        {
            if (_brickCount >= 30)
            {
                var coin = Instantiate(coinPrefab);
                coin.transform.position = coinSpawnPoint.position;
                _manager.gold.Increase(5);
                onMoneyUpdate?.Invoke(_mainCam.WorldToScreenPoint(coinSpawnPoint.position));
                _brickCount = 0;
            }
        }
    }
}