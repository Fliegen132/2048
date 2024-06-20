using System;
using GamePush;
using UnityEngine;

public class CoinsAd : MonoBehaviour
{
    [SerializeField] private float currentTime;
    [SerializeField] private float maxTime;
    [SerializeField] private CoinsView _coins;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentTime >= maxTime)
        {
            anim.Play("Open");       
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
    public void Click()
    {
        anim.Play("Close");
        ShowRewarded();
    }
    public void ShowRewarded() => GP_Ads.ShowRewarded("COINS", OnRewardedReward, OnRewardedStart, OnRewardedClose);

    private void OnRewardedStart() => Debug.Log("ON REWARDED: START");
    private void OnRewardedReward(string value)
    {
        GP_Player.Add("coins", 100);
        _coins.AddCoins(100);
        currentTime = 0;
        GP_Player.Sync();
        GP_Player.Sync(true);
    }
    private void OnRewardedClose(bool success) => Debug.Log("ON REWARDED: CLOSE");

}
