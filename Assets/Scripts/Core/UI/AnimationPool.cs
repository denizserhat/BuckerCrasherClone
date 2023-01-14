using UnityEngine;
using UnityEngine.UI;
using Utility.Pool;

namespace Core.UI
{
    public class AnimationPool : MonoBehaviour, IResettable
    {
        public Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void Reset()
        {
            gameObject.SetActive(false);
        }
    }
}