using UnityEngine;

namespace Core.Variables
{
    [CreateAssetMenu(menuName = "Game/Variables/Create Int Variable", fileName = "IntVariable", order = 0)]
    public class IntVariable : Variable<int>
    {
        public void Increase(int count)
        {
            value += count;
        }
    }
}
