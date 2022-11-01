using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   
    public PopUpManager popupManager;
    public OptionUI optionUI;
    public ScreenSwtich screen_swtich;
    public CharacterUI characterUI;


    #region ���� ��ŸƮ UI ���

    public void QuitGame(int index)
    {
        popupManager.OpenPopup(index, "�����ϱ�", "���� ���� �Ͻðڽ��ϱ�?",()=> Debug.Log("��"));

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
