using System;
using System.Collections.Generic;
using Core.Bricks;
using UnityEngine;

namespace Core
{
    public static class EventManager
    {

        public static event Action<Vector3> onMoneyUpdate;
        
        public static void OnMoneyUpdate(Vector3 startPos) => onMoneyUpdate?.Invoke(startPos);
    }
}