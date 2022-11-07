using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpManager : MonoBehaviour
{
    public PopUp[] MyPopups;
    public GameObject Dont_TouchPanel;

    /// <summary>
    /// index = 0: Èò»ö ÆË¾÷ , 1: °ËÀº»ö ÆË¾÷
    /// </summary>
    public void OpenPopup(int index,string title,string detail,params UnityAction[] PopupAction)
    {
        Dont_TouchPanel.SetActive(true);
        MyPopups[index].SetAndOpenPopup(title, detail);

        if (PopupAction == null) return;

        for(int i=0;i<PopupAction.Length;i++)
        {
            MyPopups[index].SetBtnAction(i, PopupAction[i]);
        }
    }
    



}

