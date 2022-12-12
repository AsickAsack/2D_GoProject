using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCharacterUI : MonoBehaviour
{
    public int CharacterPrice;

    [Header("[보유 이전 UI들]")]

    public Character myChar;
    public Image CharacterIcon;
    public TMPro.TMP_Text IndexText;
    public TMPro.TMP_Text CharNameText;
    public TMPro.TMP_Text CharacterPriceText;

    public Button MyInfoButton;

    [Header("[보유 이후 UI들]")]

    public GameObject GetAlreadyPanel;
    public TMPro.TMP_Text GetDate;

    public void SetCharacterUI(int index)
    {
        myChar = new Character(index);
        CharacterIcon.sprite = GameDB.Instance.GetCharacterIcon(myChar);
        //IndexText.text = (index - (int)CharacterName.Strong + 1).ToString();
        CharNameText.text = myChar.Name;
        CharacterPriceText.text = CharacterPrice.ToString("N0");
        MyInfoButton.onClick.AddListener(()=> {
            PopUpManager.Instance.OpenDesPopup(true, myChar.Name, GameDB.Instance.ChangeFigure(myChar, myChar.Skill_Des));
        });
        this.gameObject.SetActive(true);
        Check_AlreadyHave();
    }

    

    public void Alreadyhave(Character character)
    {
        GetAlreadyPanel.SetActive(true);
        GetDate.text = "획득일자\n\n"+ character.GetTime.ToString("yy-MM-dd HH:mm");
    }

    public void Check_AlreadyHave()
    {
        Character temp = PlayerDB.Instance.MyCharacters.Find(x => x.MyCharacter == myChar.MyCharacter);

        if (temp != null)
        {
            Alreadyhave(temp);
        }    
    }

    public void BuyCharacter()
    {
        if(PlayerDB.Instance.Gold < CharacterPrice)
        {
            PopUpManager.Instance.OpenPopup(0, "알림", "돈이 부족합니다!", null);
        }
        else
        {
            PopUpManager.Instance.OpenPopup(0, "알림", $"{myChar.Name}를(을)\n 정말 구매 하시겠습니까?", confirmBuy);
        }
    }

    public void confirmBuy()
    {
        PlayerDB.Instance.Gold -= CharacterPrice;
        PlayerDB.Instance.MyCharacters.Add(new Character(myChar));
        Alreadyhave(PlayerDB.Instance.MyCharacters[^1]);
        PopUpManager.Instance.OpenPopup(0, "알림", $"{myChar.Name}를(을) 획득했습니다!", null);
    }

    

}
