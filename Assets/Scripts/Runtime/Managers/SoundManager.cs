using System.Collections;
using System.Collections.Generic;
using Runtime.Data.UnityObject;
using Runtime.Enums;
using Runtime.Extensions;
using Runtime.Managers;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [Header("Default Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Glissando Audio Source")]
    [SerializeField] private AudioSource glissandoAudioSource;

    [SerializeField] private CD_GameSound COLLECTION;

    private float glissandoDuration = 2f;
    private float glissandoPitchRange = 2f;
    private Coroutine glissandoCoroutine;

    public void PlaySound(GameSoundType soundType)
    {
        if (SettingsManager.Instance.isSoundActive)
        {
            foreach (var sound in COLLECTION.gameSoundData)
            {
                if (soundType == sound.gameSoundType)
                {
                    audioSource.PlayOneShot(sound.audioClip);
                    break;
                }
            }
        }
    }

    public void PlayGlissandoSound(GameSoundType soundType)
    {
        if (SettingsManager.Instance.isSoundActive)
        {
            foreach (var sound in COLLECTION.gameSoundData)
            {
                if (soundType == sound.gameSoundType && sound.hasExternalAudioSource && sound.hasGlissando)
                {
                    if (glissandoCoroutine != null)
                    {
                        StopCoroutine(glissandoCoroutine);
                    }
                    glissandoCoroutine = StartCoroutine(PlayGlissando(sound.audioClip));
                    break;
                }
            }
        }
    }

    private IEnumerator PlayGlissando(AudioClip clip)
    {
        float elapsedTime = 0f;
        float initialPitch = glissandoAudioSource.pitch;

        glissandoAudioSource.PlayOneShot(clip);

        while (elapsedTime < glissandoDuration)
        {
            float t = elapsedTime / glissandoDuration;
            glissandoAudioSource.pitch = Mathf.Lerp(initialPitch, initialPitch + glissandoPitchRange, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        glissandoAudioSource.pitch = initialPitch;
    }
}
