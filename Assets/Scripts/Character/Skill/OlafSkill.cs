using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlafSkill : CharacterSkill
{
    public GameObject IceObj;
    public int CurHitCount;
    public int IceCount;
    public float IceRange;

    //������ ���̽� ī��Ʈ�� dB���� �޾ƿ;����� ����

    private void OnEnable()
    {
        CurHitCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBall") || collision.gameObject.CompareTag("PlayerBall"))
        {
            CurHitCount++;
            //���� ���� ���̶��..
            if (!characterplay.OnBoard && CurHitCount == IceCount)
            {
                IsSKill = true;


                if (collision.gameObject.GetComponent<ICompareSkill>().GetSkillPriority(this))
                {
                    PlayManager.Instance.objectPool.GetActiveEffects(13,this.transform.position);
                    Skill(collision.transform);
                }

                IsSKill = false;
            }
            ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);

        }
    }

    public void Skill(Transform tr)
    {
        Collider2D[] myColl;

        myColl = Physics2D.OverlapCircleAll(this.transform.position, IceRange);

        for(int i=0;i<myColl.Length;i++)
        {
            if((myColl[i].transform.CompareTag("PlayerBall")|| myColl[i].transform.CompareTag("EnemyBall")) && myColl[i].transform != this.transform)
            {
                Debug.Log(myColl[i].transform);
                Ice ice = Instantiate(IceObj).GetComponent<Ice>();
                ice.SetIce(myColl[i].gameObject);   
            }
        }

        
        
    }
        
}
