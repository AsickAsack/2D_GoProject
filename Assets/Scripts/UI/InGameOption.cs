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

    public TMPro.TMP_Text TicektCount;

    public TMPro.TMP_Text OptionGoldText;
    public TMPro.TMP_Text OptionTicketText;

    private void Start()
    {
        InitOption();
    }

    public void InitOption()
    {
        SetBgmSlider();
        SetEffectSlider();
        TicektCount.text = $"x {StageManager.instance.stage[(int)StageManager.instance.CurStage.x - 1].subStage[(int)StageManager.instance.CurStage.y - 1].NeedTicket}";
        OptionGoldText.text = PlayerDB.Instance.Gold.ToString("N0");
        OptionTicketText.text = $"{PlayerDB.Instance.Ticket}/{PlayerDB.Instance.MaxTicket}";
    }

    public void SetPanel(bool check)
    {
        OptionPanel.SetActive(check);
        DontTouch_Panel.SetActive(check);
    }

    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
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
        Time.timeScale = 0;
    }

    //게임 재시작
    public void ReStartStage()
    {
        
        if(PlayerDB.Instance.Ticket < StageManager.instance.stage[(int)StageManager.instance.CurStage.x-1].subStage[(int)StageManager.instance.CurStage.y-1].NeedTicket)
        {
            PlayManager.Instance.ingameUI.SetPopup("티켓이 부족합니다!");
            return;
        }

        PlayerDB.Instance.Ticket -= StageManager.instance.stage[(int)StageManager.instance.CurStage.x-1].subStage[(int)StageManager.instance.CurStage.y-1].NeedTicket;
        StageManager.instance.InitStage(2);
    }

    public void NextStage()
    {
        StageManager.instance.InitStage(3);
    }
    //메인 화면 이동
    public void GoMain()
    {
        StageManager.instance.InitStage(1);


    }




}


