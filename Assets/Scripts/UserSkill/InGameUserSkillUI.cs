using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUserSkillUI : MonoBehaviour
{
    public UserSkill CurUserSkill;

    public Image UserSKill_icon;
    public TMPro.TMP_Text UserSkillText;




    private void Awake()
    {
        CurUserSkill = PlayerDB.Instance.myUserSkill;
        Init_InGameSkill();
    }


    public void Init_InGameSkill()
    {
        UserSKill_icon.sprite = GameDB.Instance.GetUserSkillicon(CurUserSkill.myIcon);
        UserSkillText.text = $"'권능'\n<color=red>{CurUserSkill.SkillPoint} 포인트</color>";
    }

    public void UserSkill_InfoPopup()
    {
        if(!PlayerDB.Instance.playerdata.PlayFirst)
            PlayManager.Instance.ingameUI.SetInfoPopup(true, CurUserSkill);
        else
            TutorialPlaymanager.Instance.ingameUI.SetInfoPopup(true, CurUserSkill);

    }



}


