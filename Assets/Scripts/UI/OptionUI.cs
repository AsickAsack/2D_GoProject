using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Slider Bgm_Slider;
    public Slider Effect_Slider;
    public TMPro.TMP_Text BgmVolumeTx;
    public TMPro.TMP_Text EffectVolumeTx;

    public Toggle CutScene;
    public Toggle Push;

    private void Start()
    {
        SetBgmSlider();
        SetEffectSlider();
    }

    public void ChangeCutScene()
    {
        GameManager.instance.CutScene = CutScene.isOn;
    }

    public void SetBgmSlider()
    {
        SoundManager.Instance.ChangeBgmVolume(Bgm_Slider.value);
        BgmVolumeTx.text = (Bgm_Slider.value * 100).ToString("N0");
    }

    public void SetEffectSlider()
    {
        SoundManager.Instance.ChangeEffectVolume(Effect_Slider.value);
        EffectVolumeTx.text = (Effect_Slider.value * 100).ToString("N0");
    }



}
