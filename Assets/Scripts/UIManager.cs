using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform SliderPos;
    public Slider PowerSlider;


    private void Awake()
    {
        PowerSlider.transform.position = Camera.main.WorldToScreenPoint(SliderPos.position);
    }
}
