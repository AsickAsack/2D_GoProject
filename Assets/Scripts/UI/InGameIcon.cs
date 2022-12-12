using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameIcon : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{

    public int Myindex;
    private Image Icon;
    private Coroutine CharacterUICo;

    private void Awake()
    {
        Icon = this.transform.GetChild(0).GetComponent<Image>();
    }


    private void Start()
    {
        Icon.sprite = GameDB.Instance.GetCharacterIcon(StageManager.instance.SelectCharacters[Myindex]);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //���� �ڵ� ���ڰ� ���� �ؾߵ� 
        //Ready�������϶� ������ ��ȯ , �ƹ��͵� ������ ��ȯ

        PlayManager.Instance.ChangeCurPlayer(Myindex, this.gameObject);
       
        if (CharacterUICo == null)
            CharacterUICo = StartCoroutine(UIRoutine());
        else
        {
            StopCoroutine(CharacterUICo);
            CharacterUICo = StartCoroutine(UIRoutine());
        }
    }

    IEnumerator UIRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        //ui ����
        PlayManager.Instance.ingameUI.SetInfoPopup(true,StageManager.instance.CurCharacters[Myindex].character);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CharacterUICo != null)
            StopCoroutine(CharacterUICo);

        //UI����;
        PlayManager.Instance.ingameUI.SetInfoPopup(false);
    }
}
