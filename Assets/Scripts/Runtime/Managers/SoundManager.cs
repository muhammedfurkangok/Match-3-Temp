using Runtime.Extensions;
using Runtime.Managers;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
  [SerializeField] private AudioSource audioSource;

   private void Awake()
   {
       base.Awake();
       audioSource = GetComponent<AudioSource>();
   }
   
   public void PlaySound(AudioClip clip)
   {
       if (clip != null && SettingsManager.Instance.isSoundActive)  audioSource.PlayOneShot(clip);
   }
   
}
