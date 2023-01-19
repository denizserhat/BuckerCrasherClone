using UnityEngine;
using Utility.Pool;

namespace Core.UI
{
    public class AnimationUIPool : MonoBehaviour, IResettable
    {
        public void Reset()
        {
            gameObject.SetActive(false);
        }
    }
}