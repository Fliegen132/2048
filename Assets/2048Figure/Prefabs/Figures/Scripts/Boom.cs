using System;
using _2048Figure.Architecture.ServiceLocator;
using _2048Figure.UI;
using _2048Figure.User;
using GamePush;
using UnityEngine;

namespace _2048.Figures
{
    public class Boom : MonoBehaviour
    {
        private UserInput _user;
        private ScoreView _scoreView;
        private CoinsView _coinsView;

        private void Start()
        {
            _user = ServiceLocator.current.Get<UserInput>();
            _scoreView = ServiceLocator.current.Get<ScoreView>();
            _coinsView = ServiceLocator.current.Get<CoinsView>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Figure figure = collider.GetComponent<Figure>();
            if (figure != null)
            {
                figure.AddScoreAfterBoom();
                figure.SetDefault();
                Save();
                SetDefault();
            }
        }
        private void SetDefault()
        {
            gameObject.SetActive(false);
        }
        private void Save()
        {
            _user.Save();
            int a = _scoreView.GetScore();
            _coinsView.Save();
            GP_Player.Set("localscore", a);
            if (a > GP_Player.GetInt("score"))
            {
                GP_Player.Set("score", a);
            }
            GP_Player.Sync();
            GP_Player.Sync(true);
        }
    }
}