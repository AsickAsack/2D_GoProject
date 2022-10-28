using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PopUpManager popupManager;
    

    #region 게임 스타트 UI 기능

    public void QuitGame(int index)
    {
        popupManager.OpenPopup(index, "Quit Game", "Quit Game?", () => Application.Quit());

    }

    #endregion
}
