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
    bool IsSelect = false;

    private void Awake()
    {
        Icon = this.GetComponent<Image>();
    }

    public void SetBtn(CharacterSelectUI CS, Character character)
    {

        CharacterSelect = CS;
        this.character = character;
        Icon.sprite = GameDB.Instance.GetCharacterIcon(character);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsSelect)
        {

            CharacterSelect.SetSelectList(this, this.character);
            //���� �Ǿ��ٴ� �̹��� �ѱ�
            //�̰Ŷ� �´� ���� ����Ʈ �����
            //�ؿ� UI�� �������� ������ ����ְ�

            IsSelect = !IsSelect;
        }
        else
        {
            //���� �Ǿ��ٴ� �̹��� ����
            //������ �ε����� ĳ���� �߰�
            //�ؿ� UI�� �������ѳ��� ������ �����ְ�
        }
        //    �߰��� ����Ʈ ��/CharacterSelect.MyCs.Length
        
    }
}
