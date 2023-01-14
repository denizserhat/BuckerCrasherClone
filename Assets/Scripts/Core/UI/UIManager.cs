using System;
using Core.Bricks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility.Pool;
using Utility.Pool.Factory;

namespace Core.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private int winRate;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Slider levelCompleteProgress;
        [SerializeField] private GameObject nextLevelUI;
        [SerializeField] private GameObject goldImagePrefab;
        [SerializeField] private Image goldImage;
        
        private BrickManager _brickManager;
        private GameManager _gameManager;
        private LevelManager _levelManager;
        private Pool<AnimationPool> _animationPool;
        private Camera _mainCam;


        private void Awake()
        {
            _brickManager = FindObjectOfType<BrickManager>();
            _gameManager = FindObjectOfType<GameManager>();
            _levelManager = FindObjectOfType<LevelManager>();
            _animationPool = new Pool<AnimationPool>(new PrefabFactory<AnimationPool>(goldImagePrefab, transform));
        }

        private void Start()
        {
            _mainCam = Camera.main;
            UpdateGold();
            levelText.text = $"LEVEL "+_levelManager.LevelUI;
        }
        
        private void OnEnable()
        {
            Brick.onExploaded += OnBrickExploded;
            EventManager.onMoneyUpdate += OnMoneyCollect;
        }
        
        private void OnDisable()
        {
            Brick.onExploaded -= OnBrickExploded;
            EventManager.onMoneyUpdate -= OnMoneyCollect;
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
        
        private async void OnMoneyCollect(Vector3 startPos)
        {
            
            for (int i = 0; i < 4; i++)
            { 
                await UniTask.Delay(50);
                var animRect = _animationPool.Allocate();
                animRect.gameObject.SetActive(true);
                animRect.transform.position = startPos;
                animRect.transform.DOMove(goldImage.transform.position, 1)
                    .OnComplete(() =>
                    {
                        animRect.transform.DOScale(Vector3.one, .5f);
                       _animationPool.Release(animRect);
                       UpdateGold();
                    });      
            }
        }


        private void LoadNextUI()
        {
            nextLevelUI.SetActive(true);
            nextLevelUI.transform.DOScale(Vector3.one, 2);
        }

        public void NextLevel()
        {
            DOTween.KillAll();
            _levelManager.Level++;
            SceneManager.LoadScene(0);
        }
    }
}