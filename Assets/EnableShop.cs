using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShop : MonoBehaviour
{
    [SerializeField] private EndGameView _endGameView;

    private void OnEnable()
    {
        _endGameView.end = true;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        Invoke(nameof(Continue), 0.1f);

    }

    private void Continue()
    {
        _endGameView.end = false;
    }
}
