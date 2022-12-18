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

    //�÷��� ����
    public void PlayEffectOnce(int index)
    {
        SoundManager.Instance.PlayEffect(index);
    }

    //BGM ����,
    public void PlayBgm(int index)
    {
        SoundManager.Instance.SetBgm(index);
    }



    public void SetGold_Text()
    {
        Gold_Text.text = PlayerDB.Instance.Gold.ToString("N0") + " ���";
    }

    public void SetTicket_Text(int MaxTicket)
    {
        Ticekt_Text.text = $"{PlayerDB.Instance.Ticket}/{MaxTicket}";
    }



    #region ���� ��ŸƮ UI ���

    public void QuitGame(int index)
    {
        popupManager.OpenPopup(index, "�����ϱ�", "���� ���� �Ͻðڽ��ϱ�?",null);

    }

    #endregion


    #region ĳ����â

   public void OpenCharacterUI()
    {
        //�̰� ���߿� ó���� �����Ҷ� �����ϴ°ɷ� -> json�ҷ��ö�

        if (PlayerDB.Instance.playerdata.MyCharacters.Count == 0) return;
        else
        {
            characterUI.No_HaveCharOBJ.SetActive(false);
            characterUI.HaveCharOBJ.SetActive(true);
            characterUI.SetCharacterUI();
        }
        


    }


    #endregion



    #region ���̵�

    public void MoveScene(int index)
    {
        LoadingManager.Instance.Loading_LoadScene(index);
    }


    #endregion
}
