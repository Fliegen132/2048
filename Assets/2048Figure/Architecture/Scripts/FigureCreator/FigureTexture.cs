using System;
using System.Collections.Generic;
using _2048.Figures;
using _2048Figure.Architecture.ServiceLocator;
using UnityEngine;

public class FigureTexture : IService
{
    public List<Sprite> _skins;
    
    //resprite
    public void UseSprites(Figure go)
    {
        if(_skins.Count <=0)
            return;
        go.GetComponent<SpriteRenderer>().color = Color.white;
        go.GetComponent<SpriteRenderer>().sprite = _skins[0];
        
    }

    public void ChangeSprite(Figure go)
    {
        int a = Int32.Parse(go.GetComponent<SpriteRenderer>().sprite.name.Trim('_', '0'));
        if (a >= _skins.Count)
        {
            a = 0;
        }
        go.GetComponent<SpriteRenderer>().sprite = _skins[a];
    }

    //recolor
    public void ChangeColor(Figure go)
    {
        Color currentColor = go.GetComponent<SpriteRenderer>().color;
        Color newColor = CalculateNewColor(currentColor);
        go.GetComponent<SpriteRenderer>().color = newColor;
    }

    private Color CalculateNewColor(Color currentColor)
    {
        float h, s, v;
        Color.RGBToHSV(currentColor, out h, out s, out v);
        h = (h + 0.1f) % 1.0f;
        return Color.HSVToRGB(h, s, v);
    }
}
