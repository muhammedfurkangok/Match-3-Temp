using Runtime.Data.UnityObject;
using Runtime.Enums;
using Runtime.Extensions;
using Runtime.Managers;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private CD_GameSound COLLECTION;
  
   public void PlaySound(GameSoundType soundType)
   {
       if (SettingsManager.Instance.isSoundActive)
       {
           foreach (var sound in COLLECTION.gameSoundData)
           {
               if (soundType == sound.gameSoundType)
               {
                   audioSource.clip = sound.audioClip;
                   audioSource.Play();
                   break; 
               }
           }
       }
   }
}
