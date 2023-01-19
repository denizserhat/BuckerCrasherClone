using UnityEngine;

namespace Core
{
    public class LevelManager : MonoBehaviour
    {
        public int Level
        {
            get => PlayerPrefs.GetInt(Constants.LevelSaveKey);
            set => PlayerPrefs.SetInt(Constants.LevelSaveKey, value);
        }

        public int LevelUI => Level + 1;

    }
}
