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
                  Character myCharacter = new Character((int)CharacterName.Strong);

                  StageManager.instance.SelectCharacters.Add(myCharacter);
                  
                  /*
                  CharacterPlay obj = Instantiate(GameDB.Instance.GetCharacter(myCharacter.MyCharacter), Vector2.zero, Quaternion.identity).GetComponent<CharacterPlay>();

                  StageManager.instance.CurCharacters.Add(obj);
                  StageManager.instance.CurCharacters[0].character = myCharacter;
                  StageManager.instance.CurCharacters[0].InGame_Sprite.sprite = GameDB.Instance.GetCharacterIcon(myCharacter);
                  StageManager.instance.CurCharacters[0].gameObject.SetActive(false);
                  */
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
