using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CS_List : MonoBehaviour, IPointerDownHandler
{
    public CharacterSelect_Btn Curbtn;
    public Character CurChar;
    public Image Icon;
    public bool IsSelect = false;

    private void Awake()
    {
        Icon = this.GetComponent<Image>();
    }

    public void SetBtn(CharacterSelect_Btn Curbtn, Character character)
    {
        IsSelect = true;
        this.Curbtn = Curbtn;
        this.CurChar = character;
        Icon.sprite = GameDB.Instance.GetCharacterIcon(character);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CurChar == null) return;

        //리스트에서 지우기 -> 마지막에 버튼누르면 한번에 추가하는게 좋을듯
        IsSelect = false;
        CurChar = null;
        Icon.sprite = null;
    }
}
