using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MonsterSetting
{
    public MonsterName monster_name;
    public Vector2 Monster_Pos;
}

[System.Serializable]
public struct ObstacleSetting
{
    public ObstacleName obstacle_name;
    public Vector2 obstacle_Pos;
    public float Angle;
}

[System.Serializable]
public struct StageSetting
{
    public MonsterSetting[] MyMonster;
    public ObstacleSetting[] MyObstacle;
}

[System.Serializable]
public struct SubStage
{
    public StageSetting Object_Information;
}

[System.Serializable]
public class Stage
{
    public SubStage[]subStage;
}


public class StageManager : MonoBehaviour
{

    #region �̱���

    private static StageManager _Instance = null;

    public static StageManager instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = FindObjectOfType<StageManager>();
                if(_Instance == null)
                {
                    GameObject obj = Instantiate(Resources.Load("StageManager") as GameObject);
                    DontDestroyOnLoad(obj);
                    _Instance = obj.GetComponent<StageManager>();
                }
            }
            return _Instance;
        }
    }

    #endregion


    public Vector2 CurStage;
    public Stage[] stage;

    public void SetStage(int Stage,int SubStage)
    {
        for (int i = 0; i < stage[Stage].subStage[SubStage].Object_Information.MyMonster.Length;i++)
        {
            GameObject obj = Instantiate(Resources.Load("Monster") as GameObject, stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].Monster_Pos, Quaternion.identity);
            //������ ���� ��ũ��Ʈ �ھƾ��� //������ �����Ǿ���� ( ���� �ʱ�ȭ ��Ű�� �ɵ�?)

        }

        for (int i = 0; i < stage[Stage].subStage[SubStage].Object_Information.MyObstacle.Length; i++)
        {
            //�ޱ� ����
            GameObject obj = Instantiate(Resources.Load("Obstacle") as GameObject, stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].obstacle_Pos, Quaternion.identity);

            //������ ���� �ٸ���
        }

        //Debug.Log()


    }

}
