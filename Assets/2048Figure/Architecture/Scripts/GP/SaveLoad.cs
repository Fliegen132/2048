using System;
using _2048Figure.Architecture;
using _2048Figure.Architecture.Scripts;
using _2048Figure.Architecture.ServiceLocator;
using GamePush;
using TMPro;
using UnityEngine;

public class SaveLoad : IService
{
    private string allJsonFigures; 
    private string allActiveFigures; 
    private int _activeFigureCount = 0;

    public void SaveTransform(TransformData transformData)
    {
        string json = JsonUtility.ToJson(transformData);
        if (!string.IsNullOrEmpty(allJsonFigures))
        {
            allJsonFigures += ";";
        }
        allJsonFigures += json;
        _activeFigureCount++;
        if (_activeFigureCount >= Pools.figures.Count)
        {
            SaveTransformData();
            _activeFigureCount = 0;
            allJsonFigures = "";
        }
    }
    
    private void SaveTransformData()
    {
        GP_Player.Set("figurestransforms", allJsonFigures);
    }

    public void LoadTransforms()
    {
        string jsonFiguresTransforms = GP_Player.GetString("figurestransforms");
        if (!string.IsNullOrEmpty(jsonFiguresTransforms))
        {
            string[] jsonFigures = jsonFiguresTransforms.Split(';');
            for (int i = 0; i < jsonFigures.Length && i < Pools.figures.Count; i++)
            {
                TransformData transformData = JsonUtility.FromJson<TransformData>(jsonFigures[i]);
                Pools.figures[i].transform.position = transformData.position;
                Pools.figures[i].transform.rotation = transformData.rotation;
                Pools.figures[i].GetData().CurrentOrder = transformData.currentOrder;
                Pools.figures[i].GetData().CurrentSize = transformData.currentSize;
                Pools.figures[i].LoadFigure(Pools.figures[i].GetData().CurrentSize);
            }
        }
    }

    public void SaveActiveFigures()
    {
        allActiveFigures = "";
        for (int i = 0; i < Pools.figures.Count; i++)
        {
            if (Pools.figures[i].GetData().IsActive)
                allActiveFigures += i + " ";
        }
        GP_Player.Set("activefigure", allActiveFigures);
    }

    public bool LoadActiveFigures()
    {
        string _allActiveFigures = GP_Player.GetString("activefigure");
        if (string.IsNullOrEmpty(_allActiveFigures))
            return false;

        string[] activeFigures = _allActiveFigures.Split(' ');
        Debug.Log(activeFigures[activeFigures.Length - 2]);
        if (activeFigures.Length <= 1)
            return false;

        for (int i = 0; i < activeFigures.Length; i++)
        {
            for (int j = 0; j < Pools.figures.Count; j++)
            {
                if (Int32.TryParse(activeFigures[i], out int figureIndex) && figureIndex == j)
                {
                    Pools.figures[j].gameObject.SetActive(true);
                    Pools.figures[j].GetData().IsActive = true;
                    if (Int32.Parse(activeFigures[activeFigures.Length - 2])== j)
                    {
                        Pools.figures[j].GetComponent<Rigidbody2D>().simulated = false; // Change to false
                        continue;
                    } 
                    Pools.figures[j].GetComponent<Rigidbody2D>().simulated = true;
                }
            }
        }

        return true;
    }

    public void SaveScore(int score)
    {
        if (GP_Player.GetInt("score") < score)
        {
            GP_Player.Set("score", score);
        }
        GP_Player.Set("localscore", score);
        
        GP_Player.Sync();
        GP_Player.Sync(true);
    }
    public int LoadScore(int score)
    {
        if (GP_Player.Has("score"))
        {
            score = GP_Player.GetInt("localscore");
        }
        return score;
    }
}
