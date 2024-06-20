using System.Collections.Generic;
using _2048.Creator;
using _2048.Figures;
using _2048Figure.Architecture;
using _2048Figure.Architecture.ServiceLocator;
using UnityEngine;

public class CircleCreator : Creator
{
    public override void CrateFigure(Transform parent)
    {
        var prefab = Resources.Load<GameObject>("Figures/Circle");
        var skins = Resources.LoadAll<Sprite>("Skins/Circle");
        ServiceLocator.current.Get<FigureTexture>()._skins = new List<Sprite>(skins);
        GameObject go = Object.Instantiate(prefab, parent);
        var figure = go.AddComponent<Figure>();
        figure.Init("Circle");
        Pools.figures.Add(figure);
        go.SetActive(false);
        
    }
}
