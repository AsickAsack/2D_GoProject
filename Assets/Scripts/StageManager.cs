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
public struct ToggleSetting
{
    public Vector2 Toggle_Pos;
    public float Angle;
    public ToggleObstacleSetting[] ToggleObstacles;
}

[System.Serializable]
public struct ToggleObstacleSetting
{
    public bool IsActive;
    public ObstacleName obstacle_name;
    public Vector2 obstacle_Pos;
    public float Angle;
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
    public ToggleSetting[] MyToggle;
}

[System.Serializable]
public struct SubStage
{
    public int NeedCharacter;
    public int NeedTicket;
    //public int Stars;
    public int RewardGold;
    //public bool IsActive;
    public StageSetting Object_Information;
}

[System.Serializable]
public class Stage
{
    public SubStage[] subStage;
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

    public List<Character> SelectCharacters = new List<Character>();
    public List<CharacterPlay> CurCharacters = new List<CharacterPlay>();
    public List<MonsterPlay> CurMonsters = new List<MonsterPlay>();
    public List<Obstacle> CurObstacle = new List<Obstacle>();
    public List<ObToggle> CurToggle = new List<ObToggle>();

    public Vector2 CurStage;
    public Stage[] stage;

    public UnityEngine.Events.UnityAction NextStageAction;
    public bool NextSet = false;

    /// <summary>
    ///  1 - ����ȭ�� , 2 - ���� �����, 3 - ������������
    /// </summary>
    /// <param name="index"></param>
    public void InitStage(int index)
    {
        CurCharacters.Clear();
        CurMonsters.Clear();
        CurObstacle.Clear();
        CurToggle.Clear();
        Time.timeScale = 1;

        switch (index)
        {
            case 1:
                SelectCharacters.Clear();
                SceneLoader.Instance.Loading_LoadScene(0);
                break;
            case 2:
                SceneLoader.Instance.Loading_LoadScene(2);
                break;
            case 3:
                if (SetNextStage())
                {
                    //���� ���������� �ִٸ�
                    SelectCharacters.Clear();
                    SceneLoader.Instance.Loading_LoadScene(0);
                    NextSet = true;
                    //NextStageAction();
                }
                else
                {
                    PlayManager.Instance.ingameUI.SetPopup("���� ���������� �����ϴ�!");
                }
                break;
        }
    }

    public bool SetNextStage()
    {

        if (stage[(int)CurStage.x - 1].subStage.Length <= CurStage.y)
        {
            if (stage.Length <= CurStage.x)
                return false;
            else
            {
                CurStage = new Vector2(CurStage.x + 1, 0);
                return true;
            }
        }
        else
        {
            CurStage.y++;
            return true;
        }
        
    }

    //���������� �ʿ��� ĳ���� ī��Ʈ ����
    public int GetNeedCharacterCount()
    {
        if (stage[(int)CurStage.x - 1].subStage[(int)CurStage.y - 1].NeedCharacter > 5)
            return 5;
        else
            return stage[(int)CurStage.x-1].subStage[(int)CurStage.y-1].NeedCharacter;
    }

    public void SetStage(int Stage,int SubStage)
    {
        Stage--;
        SubStage--;

        SetCharacter();
        SetMonster(Stage,SubStage);
        SetObstacle(Stage,SubStage);
        SetToggle(Stage, SubStage);

    }

    public void SetCharacter()
    {
        for (int i = 0; i < GetNeedCharacterCount();i++)
        {
            CharacterPlay obj = Instantiate(GameDB.Instance.GetCharacter(SelectCharacters[i].MyCharacter), Vector2.zero,Quaternion.identity).GetComponent<CharacterPlay>();

            CurCharacters.Add(obj);
            CurCharacters[i].character = SelectCharacters[i];
            CurCharacters[i].InGame_Sprite.sprite = GameDB.Instance.GetCharacterIcon(CurCharacters[i].character);
            CurCharacters[i].gameObject.SetActive(false);
        }
    }


    //��������DB�� �´� ���� ����
    public void SetMonster(int Stage,int SubStage)
    {
        for (int i = 0; i < stage[Stage].subStage[SubStage].Object_Information.MyMonster.Length; i++)
        {
            CurMonsters.Add(Instantiate(GameDB.Instance.GetMonster(stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].monster_name),
                stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].Monster_Pos, Quaternion.identity).GetComponent<MonsterPlay>());

            CurMonsters[^1].monster = new Monster(stage[Stage].subStage[SubStage].Object_Information.MyMonster[i].monster_name);
            CurMonsters[^1].Basic_init();

            //������ �����Ǿ���� (���� �ʱ�ȭ ��Ű�� �ɵ�?)
            //���⵵ �� ȹ�������� ������,,
        }
    }


    //��������DB�� �´� ��ֹ� ����
    public void SetObstacle(int Stage,int SubStage)
    {
        for (int i = 0; i < stage[Stage].subStage[SubStage].Object_Information.MyObstacle.Length; i++)
        {

                GameObject obj = Instantiate(GameDB.Instance.GetObstacle(stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].obstacle_name),
                stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].obstacle_Pos,
                Quaternion.Euler(0, 0, stage[Stage].subStage[SubStage].Object_Information.MyObstacle[i].Angle));

            //������ ���� �ٸ���..MonsterClass�� �ʿ��ұ�?
        }
    }

    public void SetToggle(int Stage, int SubStage)
    {
        for (int i = 0; i < stage[Stage].subStage[SubStage].Object_Information.MyToggle.Length; i++)
        {
            ObToggle mytoggle = Instantiate(GameDB.Instance.GetObstacle(ObstacleName.Toggle),
                stage[Stage].subStage[SubStage].Object_Information.MyToggle[i].Toggle_Pos,
                Quaternion.Euler(0, 0, stage[Stage].subStage[SubStage].Object_Information.MyToggle[i].Angle)).GetComponent<ObToggle>();

            CurToggle.Add(mytoggle);

            for (int j = 0; j < stage[Stage].subStage[SubStage].Object_Information.MyToggle[i].ToggleObstacles.Length; j++)
            {
                Obstacle obj = Instantiate(GameDB.Instance.GetObstacle(stage[Stage].subStage[SubStage].Object_Information.MyToggle[i].ToggleObstacles[j].obstacle_name),
                stage[Stage].subStage[SubStage].Object_Information.MyToggle[i].ToggleObstacles[j].obstacle_Pos,
                Quaternion.Euler(0, 0, stage[Stage].subStage[SubStage].Object_Information.MyToggle[i].ToggleObstacles[j].Angle)).GetComponent<Obstacle>();

                obj.gameObject.SetActive(stage[Stage].subStage[SubStage].Object_Information.MyToggle[i].ToggleObstacles[j].IsActive);

                mytoggle.SetConnect(obj);

                CurObstacle.Add(obj);
            }
            
        }
    }
}
