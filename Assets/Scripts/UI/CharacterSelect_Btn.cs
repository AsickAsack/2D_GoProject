using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelect_Btn : MonoBehaviour, IPointerDownHandler
{
    private CharacterSelectUI CharacterSelect;
    public Character character;
    public Image Icon;
    public GameObject SelectObj;
    bool IsSelect = false;

    public void SetBtn(Sprite CharSprite)
    {
        this.gameObject.SetActive(true);
        Icon.sprite = CharSprite;
    }


    public void OnPointerDown(PointerEventData eventData)
    {

        //�̹� ������ ĳ���Ͱ� 3������� ���Ͻ�Ŵ, �˾� �����.

        //�ӽ÷� ������ Ŭ������ �ʿ���
        // ������ ����

        //ĳ���� ���� UI ����
        SelectObj.gameObject.SetActive(!IsSelect);

        //�̹� ���� �� ĳ���͸� ��������
        if (IsSelect)
        {
            //ĳ���� ���ü� �÷���(�ؽ�Ʈ ����)
           //�������� ���� �� �ں��� ������
        }
        else //���� �ȵ� ĳ���͸� ��������
        {
            //�ӽ� ������ �־��ְ� �ؿ� ĳ���� UI �տ������� ä����
        }

        //�� ��ư�� ���õǾ����� check
        IsSelect = !IsSelect;
    }

}
