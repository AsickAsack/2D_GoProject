using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingHelper : MonoBehaviour
{
    public Slider Loading_Slider;
    public TMPro.TMP_Text Loading_Text;
    public Transform LoadingCircle;
    public float RotateSpeed;

    public Image BackGround;
    public Sprite[] BackGroundImages;
    
    public TMPro.TMP_Text Loading_ImageText;
    [TextArea]
    public string[] Loading_ImageString;

    private void Awake()
    {
        int temp = Random.Range(0, BackGroundImages.Length);
        BackGround.sprite = BackGroundImages[temp];
        Loading_ImageText.text = Loading_ImageString[temp];
    }

    private void Update()
    {
        LoadingCircle.Rotate(-Vector3.forward * Time.deltaTime * RotateSpeed);
    }

    public void SetLoading(float f)
    {
        Loading_Slider.value = f+0.1f;
        Loading_Text.text = "Loading... "+ (Loading_Slider.value*100).ToString("N0") + '%';
    }

}
