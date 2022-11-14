using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//���� ĳ���� ���� List
public class CharacterSelectListUI : MonoBehaviour, IPointerDownHandler
{
    public int MyIndex;
    public Character character;
    public Image Icon;
    public GameObject SelectObj;
    bool IsSelect = false;

    public void SetBtn(Character Mychar)
    {
        this.gameObject.SetActive(true);
        character = Mychar;
        Icon.sprite = GameDB.Instance.GetCharacterIcon(Mychar);
    }

    public void ResetBtn()
    {
        character = null;
        IsSelect = false;
        SelectObj.SetActive(IsSelect);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       

        //�̹� ���� �� ĳ���͸� ��������
        if (IsSelect)
        { 
            CharacterSelectManager.Instance.Set_CharacterIcon(null,MyIndex);
            //ĳ���� ���� UI ����
            SelectObj.gameObject.SetActive(!IsSelect);
        }
        else //���� �ȵ� ĳ���͸� ��������
        {
            if (CharacterSelectManager.Instance.Count == 5)
            {
                PopUpManager.Instance.OpenPopup(0, "�ȳ�", "ĳ���͸� ���� �����߽��ϴ�.", null);
                return;
            }

            MyIndex = CharacterSelectManager.Instance.GetPointer();
            CharacterSelectManager.Instance.Set_CharacterIcon(character,-1);
            //ĳ���� ���� UI ����
            SelectObj.gameObject.SetActive(!IsSelect);
        }

        //�� ��ư�� ���õǾ����� check
        IsSelect = !IsSelect;
    }

}
