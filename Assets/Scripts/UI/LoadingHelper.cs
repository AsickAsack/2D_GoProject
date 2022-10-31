using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingHelper : MonoBehaviour
{
    public Slider Loading_Slider;
    public TMPro.TMP_Text Loading_Text;

    public void SetLoading(float f)
    {
        Loading_Slider.value = f+0.1f;
        Loading_Text.text = "Loading... "+ (Loading_Slider.value*100).ToString("N0") + '%';
    }

}
