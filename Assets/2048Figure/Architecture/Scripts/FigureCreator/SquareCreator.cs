using System.Collections.Generic;
using _2048.Figures;
using _2048Figure.Architecture;
using _2048Figure.Architecture.ServiceLocator;
using UnityEngine;

namespace _2048.Creator
{
    public class SquareCreator : Creator
    {
        public override void CrateFigure(Transform parent)
        {
            var prefab = Resources.Load<GameObject>("Figures/Square");
            var skins = Resources.LoadAll<Sprite>("Skins/Square");
            ServiceLocator.current.Get<FigureTexture>()._skins = new List<Sprite>(skins);
            GameObject go = Object.Instantiate(prefab, parent);
            var figure = go.AddComponent<Figure>();
            figure.Init("Square");
            Pools.figures.Add(figure);
            go.SetActive(false);
        }
    }
}