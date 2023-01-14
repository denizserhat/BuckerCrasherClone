using UnityEngine;

namespace Core
{
    public class LevelManager : MonoBehaviour
    {
        public int Level
        {
            get => PlayerPrefs.GetInt("Level");
            set => PlayerPrefs.SetInt("Level",value);
        }

        public int LevelUI => Level + 1;

    }
}
