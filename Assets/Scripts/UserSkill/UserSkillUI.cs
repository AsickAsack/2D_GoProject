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
    }

    private void Start()
    {
        if (PlayerDB.Instance.myUserSkill == userSKill)
        {
            SetUIRoutine();
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(userSKill != null)
        {
            SetUIRoutine();
        }
    }

    public void SetUIRoutine()
    {
        userSKill.SetUserSkill();
        UserSkillTitle.text = userSKill.UserskillName;
        UserSkillDes.text = userSKill.UserSkillDes;
        SelectObj.transform.position = this.transform.position;
        SelectObj.SetActive(true);
    }
}
