using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Bricks
{
    public class BrickManager : MonoBehaviour
    {
        public List<Brick> brickList;
        public int BrickCount => brickList.Count;

        public int ExplodedBrick { get; set; }
        
    }
    
}