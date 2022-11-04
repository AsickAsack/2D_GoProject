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
    public SubStage[] subStage;
}


public class StageManager : MonoBehaviour
{


    #region 싱글톤

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

    public List<Character> CurCharacters = new List<Character>();
    public List<MonsterPlay> CurMonsters = new List<MonsterPlay>();
    public List<Obstacle> CurObstacle = new List<Obstacle>();

    public Vector2 CurStage;
    public Stage[] stage;
    
    public void SetStage(int Stage,int SubStage)
    {
        Stage--;
        SubStage--;

        for (int i = 0; i < stage[Stage].subStage[SubStage].Object_Information.MyMonster.Length;i++)
        {
            CurMonsters.Add(Instantiate(GameDB.Instance.GetMonster(stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].monster_name),
                stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].Monster_Pos, Quaternion.identity).GetComponent<MonsterPlay>());

            CurMonsters[^1].monster = new Monster(stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].monster_name);
            CurMonsters[^1].Basic_init();

            //아이콘 수정되어야함 (몬스터 초기화 시키면 될듯?)
        }

        for (int i = 0; i < stage[Stage].subStage[SubStage].Object_Information.MyObstacle.Length; i++)
        {

            GameObject obj = Instantiate(GameDB.Instance.GetObstacle(stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].obstacle_name),
                stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].obstacle_Pos,
                Quaternion.Euler(0, 0, stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].Angle));

            
            //종류에 따라 다르게
        }
    }


}
