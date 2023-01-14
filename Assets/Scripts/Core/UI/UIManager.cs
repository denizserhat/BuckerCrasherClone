using System;
using Core.Bricks;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private int winRate;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Slider levelCompleteProgress;
        [SerializeField] private GameObject nextLevelUI;
        private BrickManager _brickManager;
        private GameManager _gameManager;
        private LevelManager _levelManager;


        private void Awake()
        {
            _brickManager = FindObjectOfType<BrickManager>();
            _gameManager = FindObjectOfType<GameManager>();
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void Start()
        {
            UpdateGold();
            levelText.text = $"LEVEL "+_levelManager.LevelUI;
        }
        
        private void OnEnable()
        {
            Brick.onExploaded += OnBrickExploded;
            EventManager.onGoldUpdate += UpdateGold;
        }
        
        private void OnDisable()
        {
            Brick.onExploaded -= OnBrickExploded;
            EventManager.onGoldUpdate -= UpdateGold;

        }

        private void OnBrickExploded(Brick brick)
        {
            _brickManager.ExplodedBrick++;
            SetProgress();
        }

        private void SetProgress()
        {
            levelCompleteProgress.value = Mathf.InverseLerp(0, _brickManager.GetBrickList, _brickManager.ExplodedBrick);
            if (levelCompleteProgress.value>(winRate*.01))
            {
                _gameManager.gameState = GameState.Finished;
                LoadNextUI();
            }
        }

        private void UpdateGold()
        {
            var value = int.Parse(coinText.text);
            var endValue = _gameManager.gold.value;
            DOTween.To(() => value, x => value = x, endValue, .2f).OnUpdate(() =>
            {
                coinText.text = value.ToString();
            });
        }

        private void LoadNextUI()
        {
            nextLevelUI.SetActive(true);
            nextLevelUI.transform.DOScale(Vector3.one, 2);
        }

        public void NextLevel()
        {
            _levelManager.Level++;
            SceneManager.LoadScene(0);
        }
    }
}