using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private EndGameView _endGameView;
    [SerializeField] private bool _isEnable = true;
    
    /*private void OnEnable()
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
    }*/

    public void ChangeMusic()
    {
        _isEnable = !_isEnable;
        ToggleMusic(_isEnable);
    }
    
    private void ToggleMusic(bool enable)
    {
        if (enable)
        {
            mixer.audioMixer.SetFloat("MasterVolume", 0);
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
        }
        
        Debug.Log(enable);
    }
}
