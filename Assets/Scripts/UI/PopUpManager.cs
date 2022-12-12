using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;


    public PopUp[] MyPopups;
    public GameObject Dont_TouchPanel;

    private void Awake()
    {
        Instance = this;
    }


    /// <summary>
    /// index = 0: Èò»ö ÆË¾÷ , 1: °ËÀº»ö ÆË¾÷
    /// </summary>
    public void OpenPopup(int index,string title,string detail,params UnityAction[] PopupAction)
    {
        Dont_TouchPanel.SetActive(true);
        MyPopups[index].SetAndOpenPopup(title, detail);

        if (PopupAction == null)
        {
            MyPopups[index].RemoveAction(0);
            return;
        }

        for(int i=0;i<PopupAction.Length;i++)
        {
            MyPopups[index].SetBtnAction(i, PopupAction[i]);
        }
    }

    public void OpenDesPopup(bool check, string Name, string Des)
    {
        Dont_TouchPanel.SetActive(true);
        MyPopups[2].SetAndOpenPopup(Name, Des);

    }
    



}

