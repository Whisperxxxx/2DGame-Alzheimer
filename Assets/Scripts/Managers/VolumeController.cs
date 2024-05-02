using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public List<AudioSource> musicSources; // 存储所有音乐音频源
    public List<AudioSource> sfxSources; // 存储所有音效音频源
    public Slider musicVolumeSlider; // 控制音乐音量的滑块
    public Slider sfxVolumeSlider; // 控制音效音量的滑块


    void Start()
    {
        // 初始化滑块的值
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // 设置初始音量
        SetMusicVolume(musicVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);

    }

    public void SetMusicVolume(float volume)
    {
        foreach (AudioSource source in musicSources)
        {
            source.volume = volume;
        }

    }

    public void SetSFXVolume(float volume)
    {
        foreach (AudioSource source in sfxSources)
        {
            source.volume = volume;
        }
    }
}
