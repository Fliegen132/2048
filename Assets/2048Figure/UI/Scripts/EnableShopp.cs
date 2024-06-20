using System;
using UnityEngine;

public class EnableShopp : MonoBehaviour
{
    
    public GameObject buyIcon;
    [SerializeField]private float _currentTime;
    private float _maxTime = 120;
    public bool wasBuy;
    private void Update()
    {
        if(wasBuy == false)
            _currentTime += Time.deltaTime;
        if (_currentTime >= _maxTime)
        {
            _currentTime = 0;
            buyIcon.GetComponent<Animator>().Play("Open");
        }
    }
  
}
