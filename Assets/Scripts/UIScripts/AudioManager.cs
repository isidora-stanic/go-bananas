using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour //PersistentSingleton<AudioManager>
{
    [SerializeField] private AudioSource musicAudioSource, effectsAudioSource;

    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private Slider musicVolume;

    void Start() 
    {   
        if (PlayerPrefs.HasKey("masterVolume"))
            masterVolume.value = PlayerPrefs.GetFloat("masterVolume");
        if (PlayerPrefs.HasKey("sfxVolume"))
            sfxVolume.value = PlayerPrefs.GetFloat("sfxVolume");
        if (PlayerPrefs.HasKey("musicVolume"))
            musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void PlaySound(AudioClip audioClip)
    {
        effectsAudioSource.PlayOneShot(audioClip);
    }

    public void PlaySound(AudioClip audioClip, float volume)
    {
        effectsAudioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayRandomSound(AudioClip[] audioClips)
    {
        int randomClipIndex = Random.Range(0, audioClips.Length);
        AudioClip audioClip = audioClips[randomClipIndex];
        effectsAudioSource.PlayOneShot(audioClip);
    }

    public void PlayRandomSound(AudioClip[] audioClips, float volume)
    {
        int randomClipIndex = Random.Range(0, audioClips.Length);
        AudioClip audioClip = audioClips[randomClipIndex];
        effectsAudioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayMusic(AudioClip audioClip, float volume)
    {
        if (musicAudioSource.clip == audioClip) // ako se vec pusta ta ista muzika nemoj prekidati
            return;
        musicAudioSource.Stop();
        musicAudioSource.clip = audioClip;
        musicAudioSource.volume = volume;
        musicAudioSource.Play();
    }

    public void PauseMusic()
    {
        musicAudioSource.Pause();
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
    }

    public void ToggleEffects()
    {
        effectsAudioSource.mute = !effectsAudioSource.mute;
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = masterVolume.value;
        PlayerPrefs.SetFloat("masterVolume", masterVolume.value);
        PlayerPrefs.Save();
    }

    public void ChangeEffectsVolume()
    {
        effectsAudioSource.volume = sfxVolume.value;
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume.value);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume()
    {
        musicAudioSource.volume = musicVolume.value;
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        PlayerPrefs.Save();
    }

    public float CalculateVolumeForCollisionForce(float collisionForce, float forceThreshold)
    {
        float volume = 1;

        if (collisionForce <= forceThreshold)
        {
            volume = collisionForce / forceThreshold;
        }

        Debug.Log("volume: " + volume);

        return volume;
    }
}
