using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameOption : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject DontTouch_Panel;

    public Slider Bgm_Slider;
    public Slider Effect_Slider;

    public TMPro.TMP_Text BgmVolumeTx;
    public TMPro.TMP_Text EffectVolumeTx;

    private void Start()
    {
        SetBgmSlider();
        SetEffectSlider();
    }
    public void SetPanel(bool check)
    {
        OptionPanel.SetActive(check);
        DontTouch_Panel.SetActive(check);
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
    

    public void Resume_Game()
    {
        SetPanel(false);
        Time.timeScale = 1;
    }

    public void Pause_Game()
    {
        SetPanel(true);
        Time.timeScale = 1;
    }

    //게임 재시작
    public void ReStartStage()
    {
        //티켓 있는지 확인해야함
        StageManager.instance.InitStage(true);
    }

    public void NextStage()
    {
        StageManager.instance.InitStage(true);
    }
    //메인 화면 이동
    public void GoMain()
    {
        StageManager.instance.InitStage(false);


    }




}


