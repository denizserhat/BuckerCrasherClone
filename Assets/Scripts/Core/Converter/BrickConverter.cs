using System;
using Core.Variables;
using UnityEngine;

namespace Core.Converter
{
    public class BrickConverter : MonoBehaviour
    {
        public static event Action<Vector3> onMoneyUpdate;
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private Transform coinSpawnPoint;
        [SerializeField] private IntVariable gold;

        private int _brickCount;
        private Camera _mainCam;

        private void Awake()
        {
            _mainCam = Camera.main;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(Constants.BrickTag))
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
                gold.Increase(5);
                onMoneyUpdate?.Invoke(_mainCam.WorldToScreenPoint(coinSpawnPoint.position));
                _brickCount = 0;
            }
        }
    }
}