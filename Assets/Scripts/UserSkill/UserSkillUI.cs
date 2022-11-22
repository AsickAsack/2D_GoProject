using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserSkillUI : MonoBehaviour,IPointerClickHandler
{
    public int index;
    public UserSkill userSKill;
    public GameObject SelectObj;
    public TMPro.TMP_Text UserSkillTitle;
    public TMPro.TMP_Text UserSkillDes;

    private void Awake()
    {
        userSKill = GameDB.Instance.UserSkills[index];

        if(PlayerDB.Instance.myUserSkill != null)
        {
            //이미 선택된게 있다면 선택 해놔야함.
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(userSKill != null)
        {
            SelectObj.transform.position = this.transform.position;
            SelectObj.SetActive(true);
            userSKill.SetUserSkill();
            UserSkillTitle.text = userSKill.UserskillName;
            UserSkillDes.text = userSKill.UserSkillDes;
        }
    }
}
