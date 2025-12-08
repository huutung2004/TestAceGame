using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    [SerializeField] private MusicData _musicData;
    [SerializeField] private AudioSource sfxPrefab;
    [SerializeField] private AudioSource musicSource;
    private ObjectPool<AudioSource> audioPool;
    private List<AudioSource> activeSFX = new List<AudioSource>();
    private float currentSFXVolume = 1f;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioPool = new ObjectPool<AudioSource>(sfxPrefab, 10, transform);
    }
    public void Start()
    {
        AudioClip _backGroundClip = _musicData.GetClip("chill");
        if (_backGroundClip != null)
        {
            musicSource.clip = _backGroundClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    public void PlayMusic(string name)
    {
        AudioClip _audioClip = _musicData.GetClip(name);
        var source = audioPool.Get();
        source.clip = _audioClip;
        source.volume = currentSFXVolume;
        source.Play();
        activeSFX.Add(source);
        StartCoroutine(ReturnAfterPlay(source));

    }
    private IEnumerator ReturnAfterPlay(AudioSource src)
    {
        yield return new WaitForSeconds(src.clip.length);
        audioPool.ReturnToPool(src);
    }
    public void ChangeVolumeSFX(float volume)
    {
        currentSFXVolume = volume;

        foreach (var src in activeSFX)
        {
            if (src != null)
                src.volume = volume;
        }
    }
    public void ChangeVolumeMusic(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }
    public float GetCurrentVolumeMusic()
    {
        return musicSource.volume;
    }
    public float GetCurrentVolumeSFX()
    {
        return currentSFXVolume;
    }

}