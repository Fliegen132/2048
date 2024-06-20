using System;
using _2048Figure.Architecture;
using _2048Figure.Architecture.ServiceLocator;
using _2048Figure.User;
using GamePush;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsView : MonoBehaviour, IService
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private EnableShopp _shopp;
    [SerializeField] private Button _button;
    private int _coinsCount;
    private UserInput user;
    
    private void Start()
    {
        user = ServiceLocator.current.Get<UserInput>();
        int coinsStart = 0;
        coinsStart = GP_Player.GetInt("coins");
        AddCoins(coinsStart);
        
        _button.onClick.Invoke();
    }

    public void AddCoins(int value)
    {
        _coinsCount += value;
        coinsText.text = _coinsCount.ToString();
        
    }

    public void BuyBomb()
    {
        _coinsCount -= 5000;
        coinsText.text = _coinsCount.ToString();
        user.SetCurrentFigure(Pools.bomb);
        _shopp.wasBuy = true;
        _shopp.buyIcon.GetComponent<Animator>().Play("Close");
    }

    public void Save()
    {
        GP_Player.Set("coins", _coinsCount);
    }

}
