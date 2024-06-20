using System.Collections;
using System.Collections.Generic;
using _2048Figure.Architecture.ServiceLocator;
using UnityEngine;

public class SoundManager : MonoBehaviour, IService
{
   private AudioSource _audioSource => GetComponent<AudioSource>();
   private bool _muted = false;
   public void PlaySound(AudioClip clip)
   {
      _audioSource.clip = clip;
      _audioSource.Play();
   }

   public void Mute()
   {
      _muted = !_muted;
      _audioSource.mute = _muted;
   }
}
