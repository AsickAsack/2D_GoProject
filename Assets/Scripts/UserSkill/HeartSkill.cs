using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeartSkill : UserSkill
{
    public GameObject HeartObj;


    public override void Skill(Vector2 pos)
    {
        GameObject obj = Instantiate(HeartObj, pos, Quaternion.identity);
        SetSkillPoint();
        try
        {
            for (int i=0;i<PlayManager.Instance.OnBoardPlayer.Count;i++)
            {
                if (PlayManager.Instance.OnBoardPlayer[i].gameObject.activeSelf)
                    PlayManager.Instance.OnBoardPlayer[i].GetComponent<ConflictAndSKill>().MyRigid.AddForce((pos - (Vector2)StageManager.instance.CurCharacters[i].transform.position).normalized * 20.0f, ForceMode2D.Force);
            }

            for (int i = 0; i < StageManager.instance.CurMonsters.Count; i++)
            {
            
                if(StageManager.instance.CurMonsters[i].gameObject.activeSelf)
                    StageManager.instance.CurMonsters[i].GetComponent<MonsterPlay>().MyRigid.AddForce((pos - (Vector2)StageManager.instance.CurCharacters[i].transform.position).normalized * 20.0f, ForceMode2D.Force);
            }
        }
        catch(System.ArgumentOutOfRangeException e)
        {
            Debug.Log(e.ParamName);
            Debug.Log(StageManager.instance.CurMonsters.Count);
        }
    }


}
