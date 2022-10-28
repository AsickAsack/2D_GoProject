using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BgmAudio;
    public AudioSource EffectAudio;

    public AudioClip[] BgmClips;
    public AudioClip[] EffectClips;

    //bgm�ٲٱ� 
    public void ChangeBgm(int index, bool Loop = true)
    {
        BgmAudio.clip = BgmClips[index];
        BgmAudio.loop = Loop;
        BgmAudio.Play();
    }

    /// <summary>
    /// index - 0: ��� 1: �Ͻ����� 2: ����
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

    //����Ʈ ���
    public void PlayEffect(int index)
    {
        BgmAudio.PlayOneShot(BgmClips[index]);
    }

    //����Ʈ ���� ����
    public void ChangeEffectVolume(float v)
    {
        EffectAudio.volume = v / 100;
    }

    //bgm ���� ����
    public void ChangeBgmVolume(float v)
    {
        BgmAudio.volume = v / 100;
    }






}
