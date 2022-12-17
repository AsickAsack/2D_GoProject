using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FocusClickOBJ : MonoBehaviour, IPointerClickHandler
{
    public bool IsIgnore = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!IsIgnore)
            TutorialManager.instance.TutorialClickAction[TutorialManager.instance.TutorialClick_index++]();
    }
}
