using System;
using System.Collections.Generic;
using Core.Bricks;

namespace Core
{
    public static class EventManager
    {
        public static event Action onLevelProgress;
        
        public static void OnLevelProgress() => onLevelProgress?.Invoke();
    }
}