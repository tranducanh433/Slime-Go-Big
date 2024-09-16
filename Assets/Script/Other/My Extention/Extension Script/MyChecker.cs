using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
    public static class MyChecker
    {
        public static bool BoxCheck(Vector2 start, Vector2 size, LayerMask layerToCheck)
        {
            return Physics2D.OverlapBox(start, size, 0, layerToCheck);
        }
    }
}

