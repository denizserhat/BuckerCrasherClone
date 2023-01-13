using Core.Bricks;
using UnityEngine;

namespace Utility
{
    public class ImageToObject : MonoBehaviour
    {
        public Texture2D image;
        
        [SerializeField] private GameObject brickPrefab;
        [SerializeField] private Color[] pixels;
        private BrickManager _brickManager;
        
    
        private void Start()
        {
            _brickManager = GetComponent<BrickManager>();
            CreateObject();
        }

        private void CreateObject()
        {
            pixels = image.GetPixels();
            for (int i = 0; i < pixels.Length; i++)
            {
                bool isTransparent = pixels[i] == Color.clear;
                if (isTransparent)
                    continue;
            
                GameObject cube = Instantiate(brickPrefab,transform);
                MeshRenderer meshRenderer = cube.GetComponent<MeshRenderer>();
                meshRenderer.material.color = pixels[i];
                int x = i % image.width;
                int y = i / image.height;
                Brick brick = cube.GetComponent<Brick>();
                _brickManager.brickList.Add(brick);
                var localScale = cube.transform.localScale;
                cube.transform.position = new Vector3(localScale.x * x, localScale.x * y, 0);
            }
        }
    }
}
