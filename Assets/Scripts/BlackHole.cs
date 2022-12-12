using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Obstacle
{
    public GameObject Pivot;
    Vector2 BlackHoleVector;
    public float ScaleSppeed;
    public float LerpSpeed;

    private void Awake()
    {
        BlackHoleVector = new Vector2(0.01f, 0.01f);
    }

    public override void Skill(Collision2D collision)
    {
       //
    }

    public override void Skill(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBall") || collision.CompareTag("EnemyBall"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0.01f, 0.01f);
            StartCoroutine(RotatePlayer(collision.gameObject));
        }
    }

    IEnumerator RotatePlayer(GameObject Player)
    {

        float time = 0.2f;
        Rigidbody2D temprigid = Player.GetComponent<Rigidbody2D>();
        while (time > 0.0f)
        {
            temprigid.velocity = BlackHoleVector;
            time -= Time.deltaTime;
            Player.transform.position = Vector2.Lerp(Player.transform.position, this.transform.position, LerpSpeed * Time.deltaTime);
            yield return null;
        }


        while(Player.transform.localScale.x > 0.0f)
        {
            temprigid.velocity = BlackHoleVector;
            Player.transform.Rotate(Vector3.forward * Time.deltaTime * 720.0f);

            Player.transform.localScale = Player.transform.localScale - ((Vector3)BlackHoleVector * Time.deltaTime * ScaleSppeed);

            yield return null;
        }

        Player.GetComponent<IDeathProcess>()?.Death();
    }
}
