using System;
using System.Linq;
using _2048Figure.Architecture;
using _2048Figure.Architecture.Scripts;
using _2048Figure.Architecture.ServiceLocator;
using _2048Figure.User;
using GamePush;
using TMPro;
using UnityEngine;

namespace _2048Figure.UI
{
    public class ScoreView : MonoBehaviour, IService
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI complimentText;
        [SerializeField] private CoinsView coins;
        [SerializeField] private CubeRecordView cubeRecordView;
        private int _scoreToSave = 5;
        private int _operationsCount; // <- количество соеденений
        private UserInput _userInput;
        private int m_score;
        private SaveLoad _saveLoad;
        private void Start()
        {
            _saveLoad = ServiceLocator.current.Get<SaveLoad>();
            _userInput = ServiceLocator.current.Get<UserInput>();
            cubeRecordView = ServiceLocator.current.Get<CubeRecordView>();
            coins = ServiceLocator.current.Get<CoinsView>();
            m_score = _saveLoad.LoadScore(m_score);
            
            SetText();
        }

        public void SetScore(int value)
        {
            _operationsCount++;
            m_score += value;
            coins.AddCoins(value);

            if (_operationsCount >= _scoreToSave)
            {
                _userInput.Save();
                cubeRecordView.Save();
                                
                coins.Save();
                _saveLoad.SaveScore(m_score);
                
                complimentText.gameObject.SetActive(true);
                _operationsCount = 0;
                Invoke(nameof(DeactivateGO), 0.5f);
            }
            SetText();
        }

 
        private void DeactivateGO()
        {
            complimentText.gameObject.SetActive(false);
        }

        private void SetText()
        {
            scoreText.text = $"{m_score}";
        }

        public int GetScore() => m_score;
    }
}