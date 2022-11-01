using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadCharacter : CharacterPlay
{
    public override void AcitveSkill()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(this.transform.position, 2.0f);

        GameObject obj = Instantiate(Effect, this.transform.position, Quaternion.identity);


        for (int i = 0; i < coll.Length; i++)
        {
            if ((coll[i].CompareTag("EnemyBall") || coll[i].transform.CompareTag("PlayerBall")) && coll[i].transform != this.transform)
                Destroy(coll[i].gameObject);
        }

        this.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 1.0f);
    }

    public override void PassiveSkill()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            AcitveSkill();

        }
    }

}
