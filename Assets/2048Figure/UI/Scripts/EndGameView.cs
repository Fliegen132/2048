using System;
using _2048Figure.Architecture.ServiceLocator;
using TMPro;
using UnityEngine;

public class EndGameView : MonoBehaviour, IService
{
    [SerializeField] private GameObject window;
    [SerializeField] private TextMeshProUGUI scoreText;
    public bool end = false;
    private void Awake()
    {
        window.SetActive(false);
    }

    public void SetWindowStats(int score)
    {
        window.SetActive(true);
        scoreText.text = $"Счёт: {score}";

        end = true;
    }

}
