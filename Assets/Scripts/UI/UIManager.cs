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

    public TMPro.TMP_Text Gold_Text;

    private void Awake()
    {
        Instance = this;
        PlayerDB.Instance.Gold += 50000;
        SetGold_Text();
    }


    public void SetGold_Text()
    {
        Gold_Text.text = PlayerDB.Instance.Gold.ToString("N0") + " ���";
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
        characterUI.SetCharacterUI();
    }


    #endregion



    #region ���̵�

    public void MoveScene(int index)
    {
        LoadingManager.Instance.Loading_LoadScene(index);
    }


    #endregion
}
