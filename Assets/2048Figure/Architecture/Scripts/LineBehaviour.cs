using System;
using System.Collections;
using _2048.Figures;
using _2048Figure.Architecture;
using _2048Figure.Architecture.ServiceLocator;
using _2048Figure.UI;
using _2048Figure.User;
using GamePush;
using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    private bool figureEntered = false;
    private bool figureExited = false;
    
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float maxTime = 2f;
    [SerializeField] private Transform cleaningHeight;
    private event Action _action;

    private EndGameView _endGameView;
    private ScoreView _scoreView;
    private UserInput _user;
    private void Start()
    {
        _scoreView = ServiceLocator.current.Get<ScoreView>();
        _endGameView = ServiceLocator.current.Get<EndGameView>();
        _user = ServiceLocator.current.Get<UserInput>();
        _action += _user.FindFigure;
        _action += _user.UpdateCurrentFigure;
    }

    private void Update()
    {
        if(_endGameView.end)
            return;
        if (figureEntered)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= maxTime)
            {
                _endGameView.SetWindowStats(_scoreView.GetScore());
            }
        }
        if (figureExited)
        {
            currentTime = 0f;
            figureEntered = false;
            figureExited = false;
        }
        
    }

    private void OnDisable()
    {
        _action -= _user.FindFigure;
        _action -= _user.UpdateCurrentFigure;
    }

    public void ShowRewarded() => GP_Ads.ShowRewarded("COINS", OnRewardedReward, OnRewardedStart, OnRewardedClose);

    private void OnRewardedStart() => Debug.Log("ON REWARDED: START");
    private void OnRewardedReward(string value)
    {
        currentTime = 0;
        figureExited = true;
        for (int i = 0; i < Pools.figures.Count; i++)
        {
            if (Pools.figures[i].transform.position.y > cleaningHeight.position.y)
            {
                Pools.figures[i].SetDefault();
            }
        }
        _action?.Invoke();
        Invoke(nameof(ContinueGame), 0.5f);

       
    }

    private void ContinueGame()
    {
        _endGameView.end = false;
    }

    private void OnRewardedClose(bool success) => Debug.Log("ON REWARDED: CLOSE");
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!figureEntered && collider.GetComponent<Figure>())
        {
            figureEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (figureEntered && collider.GetComponent<Figure>())
        {
            figureExited = true;
        }
    }
}