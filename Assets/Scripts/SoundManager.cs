using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _Instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<SoundManager>();
                if(_Instance == null)
                {
                    GameObject obj = Resources.Load("SoundManager") as GameObject;
                    _Instance = obj.GetComponent<SoundManager>();
                    DontDestroyOnLoad(obj);
                }
            }

            return _Instance;
        }

    }

    private float _BgmVolume;
    public float BgmVolume
    {
        get => _BgmVolume;
        set
        {
            _BgmVolume = value;
        } 

    }

    private float _EffectVolume;
    public float EffectVolume
    {
        get => _EffectVolume;
        set
        {
            _EffectVolume = value;
        }

    }

    private AudioSource _BgmAudio;
    public AudioSource BgmAudio
    {
        get
        {
            _BgmAudio = Camera.main.GetComponent<AudioSource>();
            return _BgmAudio;
        }
    }

    private AudioSource _EffectAudio;
    public AudioSource EffectAudio
    {
        get
        {
            _EffectAudio = Camera.main.transform.GetChild(0).GetComponent<AudioSource>();
            return _EffectAudio;
        }
    }

    public AudioClip[] BgmClips;
    public AudioClip[] EffectClips;

    //bgm바꾸기 
    public void SetBgm(int index, bool Loop = true)
    {
        BgmAudio.clip = BgmClips[index];
        BgmAudio.loop = Loop;
        BgmAudio.Play();
    }

    /// <summary>
    /// index - 0: 재생 1: 일시정지 2: 정지
    /// </summary>
    /// <param name="index"></param>
    public void SetBgmState(int index)
    {
        switch(index)
        {
            case 0:
                BgmAudio.Play();
                break;
            case 1:
                BgmAudio.Pause();
                break;
            case 2:
                BgmAudio.Stop();
                break;
        }
    }

    //이펙트 재생
    public void PlayEffect(int index)
    {
        EffectAudio.PlayOneShot(EffectClips[index]);
    }

    //이펙트 볼륨 수정
    public void ChangeEffectVolume(float v)
    {
        EffectAudio.volume = v;
        EffectVolume = v;
    }

    //bgm 볼륨 수정
    public void ChangeBgmVolume(float v)
    {
        BgmAudio.volume = v;
        BgmVolume = v;
    }

    public void SetVolumeNewScene()
    {
        ChangeBgmVolume(BgmVolume);
        ChangeEffectVolume(EffectVolume);
    }




}
