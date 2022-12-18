using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeartSkill : UserSkill
{
    public GameObject HeartObj;
    public float Power;
    public float DelayTime;

    private void Awake()
    {
        myIcon = UserSkillicon.Heart;
    }

    public override void Skill(Vector2 pos)
    {
        SoundManager.Instance.PlayEffect(7);
        GameObject obj = Instantiate(HeartObj,pos,Quaternion.identity);
        PlayManager.Instance.UserSkillObj = obj.GetComponent<UserSkill>();
        SetSkillPoint();

        try
        {
            for (int i = 0; i < PlayManager.Instance.OnBoardPlayer.Count; i++)
            {
                if (PlayManager.Instance.OnBoardPlayer[i].gameObject.activeSelf)
                    PlayManager.Instance.OnBoardPlayer[i].GetComponent<CharacterSkill>().MyRigid.AddForce((pos - (Vector2)PlayManager.Instance.OnBoardPlayer[i].transform.position).normalized * Power, ForceMode2D.Force);
            }

            for (int i = 0; i < StageManager.instance.CurMonsters.Count; i++)
            {

                if (StageManager.instance.CurMonsters[i].gameObject.activeSelf)
                    StageManager.instance.CurMonsters[i].GetComponent<MonsterPlay>().MyRigid.AddForce((pos - (Vector2)StageManager.instance.CurMonsters[i].transform.position).normalized * Power, ForceMode2D.Force);
            }
        }
        catch (System.ArgumentException e)
        {
            Debug.Log(e.ParamName);
        }
    }





   

}
