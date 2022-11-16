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
    public int NeedCharacter;
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

    public List<Character> SelectCharacters = new List<Character>();
    public List<CharacterPlay> CurCharacters = new List<CharacterPlay>();
    public List<MonsterPlay> CurMonsters = new List<MonsterPlay>();
    public List<Obstacle> CurObstacle = new List<Obstacle>();

    public Vector2 CurStage;
    public Stage[] stage;


    public void SetStage(int Stage,int SubStage)
    {
        Stage--;
        SubStage--;

        SetCharacter();
        SetMonster(Stage,SubStage);
        SetObstacle(Stage,SubStage);
    }

    public void SetCharacter()
    {
        for (int i = 0; i < SelectCharacters.Count;i++)
        {
            CharacterPlay obj = Instantiate(GameDB.Instance.GetCharacter(SelectCharacters[i].MyCharacter), Vector2.zero,Quaternion.identity).GetComponent<CharacterPlay>();

            
            CurCharacters.Add(obj);
            CurCharacters[i].character = SelectCharacters[i];
            CurCharacters[i].InGame_Sprite.sprite = GameDB.Instance.GetCharacterIcon(CurCharacters[i].character);
            CurCharacters[i].gameObject.SetActive(false);
        }
    }


    //스테이지DB에 맞는 몬스터 생성
    public void SetMonster(int Stage,int SubStage)
    {
        for (int i = 0; i < stage[Stage].subStage[SubStage].Object_Information.MyMonster.Length; i++)
        {
            CurMonsters.Add(Instantiate(GameDB.Instance.GetMonster(stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].monster_name),
                stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].Monster_Pos, Quaternion.identity).GetComponent<MonsterPlay>());

            CurMonsters[^1].monster = new Monster(stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].monster_name);
            CurMonsters[^1].Basic_init();

            //아이콘 수정되어야함 (몬스터 초기화 시키면 될듯?)
            //여기도 좀 획기적으로 수정을,,
        }
    }


    //스테이지DB에 맞는 장애물 생성
    public void SetObstacle(int Stage,int SubStage)
    {
        for (int i = 0; i < stage[Stage].subStage[SubStage].Object_Information.MyObstacle.Length; i++)
        {

                GameObject obj = Instantiate(GameDB.Instance.GetObstacle(stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].obstacle_name),
                stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].obstacle_Pos,
                Quaternion.Euler(0, 0, stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].Angle));

            //종류에 따라 다르게..MonsterClass가 필요할까?
        }

    }
}
