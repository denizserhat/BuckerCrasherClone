using System;
using UnityEngine;

namespace Core.Converter
{
    public class BrickConverter : MonoBehaviour
    {
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
                EventManager.OnMoneyUpdate(_mainCam.WorldToScreenPoint(coinSpawnPoint.position));
                _brickCount = 0;
            }
        }
    }
}