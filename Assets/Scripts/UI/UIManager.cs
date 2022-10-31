using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PopUpManager popupManager;
    public OptionUI optionUI;
    

    #region ���� ��ŸƮ UI ���

    public void QuitGame(int index)
    {
        popupManager.OpenPopup(index, "Quit Game", "Really Quit Game?",()=> Debug.Log("��"));

    }

    #endregion




    #region ���̵�

    public void MoveScene(int index)
    {
        LoadingManager.Instance.Loading_LoadScene(index);
    }


    #endregion
}
