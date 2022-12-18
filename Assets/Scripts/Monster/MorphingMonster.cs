using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class MorphingMonster : MonsterPlay
{
    public MorphingCanvas RigCanvas;
    public Vector3 CameraOrgPos;
    
    public override void Initialize()
    {
        /*
        MorphingAnim = Instantiate(MorphingAnim, this.transform.position, Quaternion.identity);
        MorphingAnim.gameObject.SetActive(false);
        */
        CameraOrgPos = new Vector3(0, 1, -10);

        //RigCanvas = Instantiate(RigCanvas, this.transform.position, Quaternion.identity);
        RigCanvas.gameObject.SetActive(false);

        RigCanvas.MorphingGraphic.AnimationState.Complete += EndEvent;
    }


    public void EndEvent(Spine.TrackEntry entry)
    {
        RigCanvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public override void ConflictPlayer(Collision2D collision)
    {
        //서있는 흰돌 맞았을때도 생각
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {

            SoundManager.Instance.PlayEffect(9);

            RigCanvas.gameObject.SetActive(true);
            Camera.main.GetComponent<Animator>().SetTrigger("Shake");
            Time.timeScale = 0;


            ICompareSkill CK = collision.transform.GetComponent<ICompareSkill>();

            CK.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, MyRigid.velocity.magnitude, this.transform);

        }
    }

  

}
