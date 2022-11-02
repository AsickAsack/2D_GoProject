using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charcter_DetailUI : MonoBehaviour
{
    public Image Icon;
    public Image BigImage;

    public TMPro.TMP_Text Name_Text;
    public TMPro.TMP_Text Active_Text;
    public TMPro.TMP_Text Passive_Text;
    public TMPro.TMP_Text Story_Text;
    
    //상세 페이지 데이터들 세팅
    public void SetDetail(Character character)
    {
        Icon.sprite = GameDB.Instance.GetCharacterIcon(character);
        BigImage.sprite = GameDB.Instance.GetCharacterImage(character);

        Name_Text.text = character.Name;
        Active_Text.text = character.Active_Des;
        Passive_Text.text = character.Passive_Des;
        Story_Text.text = character.Story;

        this.GetComponent<Canvas>().enabled = true;
    }
}
