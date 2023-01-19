using System;

namespace Core
{
    public enum GameState
    {
        Started,
        Finished
    }
    public static class GameStateHandler
    {
        public static event Action<GameState> onStateChanged;

        private static GameState gameState;
        public static  GameState GameState
        {
            get => gameState;
            set
            {
                gameState = value;
                onStateChanged?.Invoke(gameState);
            }
        }
    }
}