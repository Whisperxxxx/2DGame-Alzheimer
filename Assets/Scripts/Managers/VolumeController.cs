using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public List<AudioSource> musicSources; // �洢����������ƵԴ
    public List<AudioSource> sfxSources; // �洢������Ч��ƵԴ
    public Slider musicVolumeSlider; // �������������Ļ���
    public Slider sfxVolumeSlider; // ������Ч�����Ļ���


    void Start()
    {
        // ��ʼ�������ֵ
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // ���ó�ʼ����
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
