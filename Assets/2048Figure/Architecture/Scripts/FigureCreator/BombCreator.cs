using _2048.Figures;
using _2048Figure.Architecture;
using UnityEngine;

namespace _2048.Creator
{
    public class BombCreator : Creator
    {
        public override void CrateFigure(Transform parent)
        {
            var prefab = Resources.Load<GameObject>("Figures/Bomb");
            var skin = Resources.Load<Sprite>("Skins/Bomb");
            GameObject go = Object.Instantiate(prefab, parent);
            var figure = go.AddComponent<BombBeh>();
            figure.gameObject.GetComponent<SpriteRenderer>().sprite = skin;
            Pools.bomb = figure.gameObject;
            go.SetActive(false);
        }
    }
}