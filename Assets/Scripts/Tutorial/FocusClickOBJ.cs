using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FocusClickOBJ : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        TutorialManager.instance.TutorialClickAction[TutorialManager.instance.TutorialClick_index++]();
    }
}
