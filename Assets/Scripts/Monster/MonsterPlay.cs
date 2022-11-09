using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MonsterPlay : MonoBehaviour, Death
{

    private Rigidbody2D _myRigid;
    protected Rigidbody2D myRigid
    {
        get 
        {
            _myRigid = this.GetComponent<Rigidbody2D>();
            return _myRigid;
        }
    }

    public Monster monster;

    public abstract void Initialize();
    public abstract void Skill();


    private void Start()
    {
        Basic_init();
    }

    //��� ���͵��� �ʿ��� �ʱ�ȭ(����,ũ��)
    public void Basic_init()
    {
        myRigid.mass = monster.Mass;
        this.transform.localScale = new Vector2(monster.Size, monster.Size);
        Initialize();
    }

    public void Death()
    {
        PlayManager.Instance.EnemyCount--;
        Debug.Log(PlayManager.Instance.EnemyCount);
    }

    private void OnDestroy()
    {
        Death();
    }
}
