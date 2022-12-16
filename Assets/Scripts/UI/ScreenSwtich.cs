using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwtich : MonoBehaviour
{
    public Animator SwitchAnim;

    public void StartSwitch()
    {
        if(PlayerDB.Instance.playerdata.PlayFirst)
        {

            PopUpManager.Instance.OpenPopup(0, "튜토리얼 진행", "처음이시네요!\n 튜토리얼을 진행하겠습니다.", () =>
              {
                  Character TutorialCharacter1 = new Character((int)CharacterName.Strong);
                  Character TutorialCharacter2 = new Character((int)CharacterName.bulldozer);

                  StageManager.instance.SelectCharacters.Add(TutorialCharacter1);
                  StageManager.instance.SelectCharacters.Add(TutorialCharacter2);

                  SceneLoader.Instance.Loading_LoadScene(3);
              });
        }
        else
        SwitchAnim.SetTrigger("Switch");
        //StartCoroutine(SwtichImage(f));
    }

    /*
    IEnumerator SwtichImage(float f)
    {
        

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
    */
}
