using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumMusicSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ChangeVolume);
    }
    private void Start()
    {
        slider.value = MusicManager.Instance.GetCurrentVolumeMusic();
    }
    private void ChangeVolume(float value)
    {
        MusicManager.Instance.ChangeVolumeMusic(value);
    }
}
