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


    public void SetAndOpenPopup(string title,string detail)
    {
        this.gameObject.SetActive(true);
        PopupTitle.text = title;
        Detail.text = detail;
    }


    public void SetBtnAction(int index,UnityAction PopupAction)
    {
        if (Buttons.Length == 0) return;

        Buttons[index].onClick.RemoveAllListeners();
        Buttons[index].onClick.AddListener(PopupAction);
    }
    

}
