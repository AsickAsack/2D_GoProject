using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwtich : MonoBehaviour
{
    public GameObject TitleObj;
    public UnityEngine.UI.Image ScreenSwitch_Imgae;

    public void StartSwitch(float f)
    {
        StartCoroutine(SwtichImage(f));
    }

    IEnumerator SwtichImage(float f)
    {
        Debug.Log(ScreenSwitch_Imgae.color.a);
        while (ScreenSwitch_Imgae.color.a < 1.0f)
        {
            float tempf = ScreenSwitch_Imgae.color.a + Time.deltaTime;
            Color temp = new Color(ScreenSwitch_Imgae.color.r, ScreenSwitch_Imgae.color.g, ScreenSwitch_Imgae.color.b, tempf);
            ScreenSwitch_Imgae.color = temp;

            yield return null;
        }

        TitleObj.SetActive(false);

        //yield return new WaitForSeconds(0.2f);

        while (ScreenSwitch_Imgae.color.a > 0.0f)
        {
            float tempf = ScreenSwitch_Imgae.color.a - Time.deltaTime;
            Color temp = new Color(ScreenSwitch_Imgae.color.r, ScreenSwitch_Imgae.color.g, ScreenSwitch_Imgae.color.b, tempf);
            ScreenSwitch_Imgae.color = temp;

            yield return null;
        }


    }
}
