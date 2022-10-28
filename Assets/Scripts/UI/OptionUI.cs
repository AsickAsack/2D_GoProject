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

    public void SetBgmSlider()
    {
        GameManager.instance.SoundManager.ChangeBgmVolume(Bgm_Slider.value);
        BgmVolumeTx.text = (Bgm_Slider.value * 100).ToString();
    }


    
}
