using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSkill : MonoBehaviour
{
    public string UserskillName;
    [TextArea]
    public string UserSkillDes;
    public int SkillPoint;

    public void SetUserSkill()
    {
        PlayerDB.Instance.myUserSkill = this;
    }


}
