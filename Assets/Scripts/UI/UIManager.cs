using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PopUpManager popupManager;
    

    #region ���� ��ŸƮ UI ���

    public void QuitGame(int index)
    {
        popupManager.OpenPopup(index, "Quit Game", "Quit Game?", () => Application.Quit());

    }

    #endregion
}
