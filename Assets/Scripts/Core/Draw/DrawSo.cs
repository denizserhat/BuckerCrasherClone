using UnityEngine;

namespace Core.Draw
{
    [CreateAssetMenu(menuName = "Game/Draw/Create DrawSo", fileName = "DrawSo", order = 0)]
    public class DrawSo : ScriptableObject
    {
        public float lineThickness = .25f;
        public float shapeDistance = .1f;
        public Material shapeMaterial;
    }
}
