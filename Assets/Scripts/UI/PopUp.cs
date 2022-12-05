using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PopUp : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text PopupTitle;
    [SerializeField]
    private TMPro.TMP_Text Detail;
    [SerializeField]
    Button[] Buttons;

    UnityAction HaveAction;


    public void SetAndOpenPopup(string title,string detail)
    {
        this.gameObject.SetActive(true);
        PopupTitle.text = title;
        Detail.text = detail;
    }


    public void SetBtnAction(int index,UnityAction PopupAction)
    {
        if (Buttons.Length == 0) return;

        if(HaveAction != null)
            Buttons[index].onClick.RemoveListener(HaveAction);

        HaveAction = PopupAction;
        Buttons[index].onClick.AddListener(PopupAction);
    }

    public void RemoveAction(int index)
    {
        if (Buttons.Length == 0) return;

        if (HaveAction != null)
            Buttons[index].onClick.RemoveListener(HaveAction);
    }
    

}
