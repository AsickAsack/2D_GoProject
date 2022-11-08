using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameIcon : MonoBehaviour, IPointerClickHandler
{

    public int Myindex;
    private Image Icon;

    //Ŭ��������
    public void OnPointerClick(PointerEventData eventData)
    {
        //���� �ڵ� ���ڰ� ���� �ؾߵ� 
        //Ready�������϶� ������ ��ȯ , �ƹ��͵� ������ ��ȯ

        PlayManager.Instance.ChangeCurPlayer(Myindex);
        
    }

    private void Awake()
    {
        Icon = this.transform.GetChild(0).GetComponent<Image>();
    }


    private void Start()
    {
        Icon.sprite = GameDB.Instance.GetCharacterIcon(StageManager.instance.SelectCharacters[Myindex]);
    }
}
