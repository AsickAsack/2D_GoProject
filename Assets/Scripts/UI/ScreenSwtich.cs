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
        while (ScreenSwitch_Imgae.fillAmount < 1.0f)
        {
            ScreenSwitch_Imgae.fillAmount += Time.deltaTime * f;

            yield return null;
        }

        TitleObj.SetActive(false);

        //yield return new WaitForSeconds(0.2f);

        while (ScreenSwitch_Imgae.fillAmount > 0.0f)
        {
            ScreenSwitch_Imgae.fillAmount -= Time.deltaTime * f;

            yield return null;
        }


    }
}
