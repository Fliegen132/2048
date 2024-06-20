using _2048Figure.UI;
using _2048Figure.User;
using UnityEngine;

namespace _2048Figure.Architecture.ServiceLocator
{
    public class SERVICELOCAROR_GAMEPLAY : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private EndGameView _endGameView;
        [SerializeField] private CubeRecordView _cubeRecordView;
        [SerializeField] private UserInput _user;
        FigureTexture figureTexturing = new FigureTexture();
        SaveLoad _saveLoad = new SaveLoad();
        
        private void Awake()
        {
            ServiceLocator serviceLocator = new ServiceLocator();;
            ServiceLocator.current.Register(figureTexturing);
            ServiceLocator.current.Register(_coinsView);
            ServiceLocator.current.Register(_user);
            ServiceLocator.current.Register(_saveLoad);
            ServiceLocator.current.Register(_cubeRecordView);
            ServiceLocator.current.Register(_scoreView);
            ServiceLocator.current.Register(_endGameView);
        }
        
    }
}