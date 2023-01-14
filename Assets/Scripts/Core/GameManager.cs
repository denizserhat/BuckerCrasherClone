using Core.Variables;
using UnityEngine;

namespace Core
{

    public enum GameState
    {
        Started,
        Finished
    }
    public class GameManager : MonoBehaviour
    {
        public IntVariable gold;
        public GameState gameState;
    }
}