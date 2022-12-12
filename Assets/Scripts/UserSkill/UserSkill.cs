using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSkill : MonoBehaviour
{
    public string UserskillName;
    [TextArea]
    public string UserSkillDes;
    public int SkillPoint;

    public UserSkillicon myIcon;

    /*
    public void SetUserSkill()
    {
        PlayerDB.Instance.myUserSkill = this;
    }
    */
    public virtual void Skill(Vector2 pos)
    {

    }

    public void SetSkillPoint()
    {
        PlayManager.Instance.UserSkillPoint -= this.SkillPoint;
    }

    public void End_Skill()
    {
        StartCoroutine(DelaySkill());
    }

    IEnumerator DelaySkill()
    {
        yield return new WaitForSeconds(1.5f);

        this.gameObject.SetActive(false);

    }
}
