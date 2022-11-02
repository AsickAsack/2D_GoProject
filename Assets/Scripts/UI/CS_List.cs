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

        //����Ʈ���� ����� -> �������� ��ư������ �ѹ��� �߰��ϴ°� ������
        IsSelect = false;
        CurChar = null;
        Icon.sprite = null;
    }
}
