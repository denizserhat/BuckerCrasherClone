using System;
using Core.Bricks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Slider levelCompleteProgress;

        private BrickManager _brickManager;
        private GameManager _gameManager;


        private void OnEnable()
        {
            Brick.onExploaded += OnBrickExploded;
        }
        
        private void OnDisable()
        {
            Brick.onExploaded -= OnBrickExploded;
        }

        private void OnBrickExploded(Brick brick)
        {
            _brickManager.ExplodedBrick++;
            SetProgress();
        }

        private void Start()
        {
            _brickManager = FindObjectOfType<BrickManager>();
            _gameManager = FindObjectOfType<GameManager>();
        }


        private void SetProgress()
        {
            levelCompleteProgress.value = Mathf.InverseLerp(0, _brickManager.GetBrickList, _brickManager.ExplodedBrick);
        }
    }
}