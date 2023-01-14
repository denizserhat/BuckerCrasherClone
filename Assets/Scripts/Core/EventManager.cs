using System;
using System.Collections.Generic;
using Core.Bricks;

namespace Core
{
    public static class EventManager
    {
        public static event Action onGoldUpdate;
        
        public static void OnGoldUpdate() => onGoldUpdate?.Invoke();
    }
}