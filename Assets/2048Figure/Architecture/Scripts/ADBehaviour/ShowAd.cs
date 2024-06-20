using System;
using GamePush;
using TMPro;
using UnityEngine;
namespace _2048Figure.Architecture.Scripts.ADBehaviour
{
    public class ShowAd : MonoBehaviour
    {
        private EndGameView _endGameView;
        private void Awake()
        {
            ShowPreloader();
            ShowSticky();
        }

        private void Start()
        {
            _endGameView = ServiceLocator.ServiceLocator.current.Get<EndGameView>();
        }

        private float _currentTime;
        private float _maxTime = 90f;
        private bool _canUpdate = true;
        private void Update()
        {
            if(_canUpdate == false)
                return;
            if (_currentTime < _maxTime)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                ActiveAd();
            }


        }

        private void ActiveAd()
        {
            if (GP_Ads.IsFullscreenAvailable())
            {
                ShowFullscreen();
                _endGameView.end = true;
                _canUpdate = false;
            }
        }

      
        private void ShowFullscreen() => GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);
        private void OnFullscreenStart() => Debug.Log("ON FULLSCREEN START");

        private void OnFullscreenClose(bool success)
        {
            _endGameView.end = false;
            _canUpdate = true;
            _currentTime = 0;
        }

        public void ShowSticky() => GP_Ads.ShowSticky();
        public void ShowPreloader() => GP_Ads.ShowPreloader(OnPreloaderStart, OnPreloaderClose);
        private void OnPreloaderStart() => Debug.Log("ON PRELOADER: START");
        private void OnPreloaderClose(bool success) => Debug.Log("ON PRELOADER: CLOSE");
    
    }
}