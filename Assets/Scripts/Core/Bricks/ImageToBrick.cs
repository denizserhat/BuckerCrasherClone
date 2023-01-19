using Core;
using Core.Bricks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utility
{
    public class ImageToBrick : MonoBehaviour
    {
        public Texture2D[] images;
        
        [SerializeField] private GameObject brickPrefab;
        [SerializeField] private Color[] pixels;
        
        private BrickManager _brickManager;
        private LevelManager _levelManager;


        private void Awake()
        {
            _brickManager = GetComponent<BrickManager>();
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void Start()
        {
            CreateObject();
        }

        private void CreateObject()
        {
            var selectedImage = _levelManager.Level<images.Length ? images[_levelManager.Level] : images[Random.Range(0,images.Length)];
            pixels = selectedImage.GetPixels();
            
            for (int i = 0; i < pixels.Length; i++)
            {
                bool isTransparent = pixels[i] == Color.clear;
                if (isTransparent)
                    continue;
            
                GameObject cube = Instantiate(brickPrefab,transform);
                MeshRenderer meshRenderer = cube.GetComponent<MeshRenderer>();
                meshRenderer.material.color = pixels[i];
                int x = i % selectedImage.width;
                int y = i / selectedImage.height;
                Brick brick = cube.GetComponent<Brick>();
                _brickManager.brickList.Add(brick);
                var localScale = cube.transform.localScale;
                cube.transform.localPosition = new Vector3(localScale.x * x, localScale.x * y, 0);
            }
        }
    }
}
