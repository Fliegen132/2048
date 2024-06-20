using System.Collections.Generic;
using _2048.Figures;
using UnityEngine;

namespace _2048Figure.Architecture
{
    public static class Pools
    {
        public static List<Figure> figures = new List<Figure>();
        public static GameObject bomb;
    }
}