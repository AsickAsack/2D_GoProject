using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialoguePanel : MonoBehaviour,IPointerClickHandler
{
    public UnityAction DialogueAction;

    public void OnPointerClick(PointerEventData eventData)
    {
        DialogueAction();
    }
}
