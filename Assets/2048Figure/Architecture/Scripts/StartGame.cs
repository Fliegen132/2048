using System;
using _2048.Creator;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Transform figurePoolPos;
    [SerializeField] private Transform particlePoolPos;
    [SerializeField] private Transform bombTransform;

    private void Awake()
    {
        Init("Circle");
    }

    public void Init(string figureName)
    {
        Creator creator;
        Creator bombCreator;
        switch (figureName)
        {
            case "Square" : 
                creator = new SquareCreator();
                for (int i = 0; i < 70; i++)
                {
                    creator.CrateFigure(figurePoolPos.transform);
                }
                break;
            case "Circle":
                creator = new CircleCreator();
                for (int i = 0; i < 70; i++)
                {
                    creator.CrateFigure(figurePoolPos.transform);
                }
                break;
           
        }
        bombCreator = new BombCreator();
        bombCreator.CrateFigure(bombTransform);
    }

   
}
