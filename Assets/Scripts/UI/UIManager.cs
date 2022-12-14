using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public PopUpManager popupManager;
    public OptionUI optionUI;
    public ScreenSwtich screen_swtich;
    public CharacterUI characterUI;
    public MetaScreen metaScreen;

    public TMPro.TMP_Text Gold_Text;
    public TMPro.TMP_Text Ticekt_Text;

    private void Awake()
    {
        Instance = this;
        SetGold_Text();
        PlayerDB.Instance.Ticket = PlayerDB.Instance.Ticket;
        
    }

    //플레이 사운드
    public void PlayEffectOnce(int index)
    {
        SoundManager.Instance.PlayEffect(index);
    }

    //BGM 사운드,
    public void PlayBgm(int index)
    {
        SoundManager.Instance.SetBgm(index);
    }



    public void SetGold_Text()
    {
        Gold_Text.text = PlayerDB.Instance.Gold.ToString("N0") + " 골드";
    }

    public void SetTicket_Text(int MaxTicket)
    {
        Ticekt_Text.text = $"{PlayerDB.Instance.Ticket}/{MaxTicket}";
    }



    #region 게임 스타트 UI 기능

    public void QuitGame(int index)
    {
        popupManager.OpenPopup(index, "종료하기", "정말 종료 하시겠습니까?",null);

    }

    #endregion


    #region 캐릭터창

   public void OpenCharacterUI()
    {
        //이거 나중에 처음에 접속할때 변경하는걸로 -> json불러올때

        if (PlayerDB.Instance.playerdata.MyCharacters.Count == 0) return;
        else
        {
            characterUI.No_HaveCharOBJ.SetActive(false);
            characterUI.HaveCharOBJ.SetActive(true);
            characterUI.SetCharacterUI();
        }
        


    }


    #endregion



    #region 씬이동

    public void MoveScene(int index)
    {
        LoadingManager.Instance.Loading_LoadScene(index);
    }


    #endregion
}
