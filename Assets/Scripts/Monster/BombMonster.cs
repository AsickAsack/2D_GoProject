using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombMonster : MonsterPlay
{
    //������Ʈ Ǯ���� ������ ����Ʈ �ε���
    public int BombEffectIndex;
    //���� ����
    public float BombRange;
    //������ �ϴ� ��
    public int BombTurn;
    //�ڵ����� ������ �ּ� �ִ� ���� ��
    public Vector2 Bombturn_Range;

    public TMPro.TMP_Text TurnText;
    

    public override void Initialize()
    {
        BombTurn = Random.Range((int)Bombturn_Range.x, (int)Bombturn_Range.y + 1);
        TurnText.text = BombTurn.ToString();
    }


    public override void Skill()
    {
        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.MonsterFall, this.transform.position, Quaternion.identity);
        Collider2D[] MyCollider = Physics2D.OverlapCircleAll((Vector2)this.transform.position, BombRange);

        if(MyCollider.Length > 0)
        {
            for(int i=0;i<MyCollider.Length;i++)
            {
                if(MyCollider[i].CompareTag("PlayerBall")|| MyCollider[i].CompareTag("EnemyBall"))
                {
                    MyCollider[i].GetComponent<IDeathProcess>().Death();
                }
            }
        }

    }


    public override void GoForward(Vector2 Dir, float Power, Transform tr)
    {
        if(tr.CompareTag("PlayerBall")&& PlayManager.Instance.CurTurn == BombTurn)
            Skill();
        else
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
    }

    public override void CountProcess()
    {
        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.MonsterFall, this.transform.position, Quaternion.identity);
        PlayManager.Instance.EnemyCount--;
        this.gameObject.SetActive(false);
        PlayManager.Instance.CurMultiKill++;

    }


    public override void Death()
    {

        CountProcess();
    }


}
