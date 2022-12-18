using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StageTutorialManager : MonoBehaviour
{
    public Dictionary<MonsterName, string> MonsterDescription = new Dictionary<MonsterName, string>();
    public Dictionary<ObstacleName, string> ObstacleDescription = new Dictionary<ObstacleName, string>();

    public Queue<UnityAction> ExplainQueue = new Queue<UnityAction>();

    public GameObject ExplainCanvas;
    public TMPro.TMP_Text TitleText;
    public TMPro.TMP_Text ExPlainText;

    public GameObject ActiveObj;
    public GameObject MovePanel;
    public Vector3 CameraOrgPos;

    int x;
    int y;

    public void InitDic()
    {
        MonsterDescription = new Dictionary<MonsterName, string>()
        { 
            { MonsterName.Big , "��ġ�� Ŀ�� ������ ���� �������� �׸�ŭ �� �з�����."},
            { MonsterName.Bomb , "�� ���� ������ �Ͽ� ����� ������ �ֺ��� ������ ����Ų��."},
            { MonsterName.Boss, "�ٸ� ���� �浹�ϸ� ���� 2������ �����Ѵ�.\n�� �Ͽ� �� ���� �ߵ��ȴ�."},
            { MonsterName.Armor , "ù ��° �浹�� ������ �԰� �ִ� ������ �����ش�.\nù �浹 ���� ������ ���ư� �Ϲ� ���ְ� ���Եȴ�."},
            { MonsterName.Silence , "�ε����� ��� �Ʊ� ĳ������ ��ų�� ��ȿ�� ����� ���̴�."},

        };

        ObstacleDescription = new Dictionary<ObstacleName, string>()
        {
            { ObstacleName.Wall , "�ε����� ƨ�ܳ����� ���̴�."},
            { ObstacleName.BlackHole , "������ ���� ������ ���� ���Ƶ鿩������ ��Ȧ�̴�."},
            { ObstacleName.Toggle , "�ٸ� ���� �浹�ϸ� ���� Ư���� ����� Ű�ų� ����.\n�� �Ͽ� �� ���� �ߵ��ȴ�."},
            { ObstacleName.Booster , "���ǿ� ���� ������ �ش� �������� ����������."},
            { ObstacleName.Stop , "�� ���ǿ� ���� ���� �ӵ��� �ް����ϰ� �ȴ�."},
        };


    }

    public void FindAndPushQueue(int x, int y)
    {
        PlayManager.Instance.gameState = GameState.None;

        for (int i = 0; i < StageManager.instance.stage[x].subStage[y].NewTutorial.Tutorial_MonsterName.Length; i++)
        {
            for (int j = 0; j < StageManager.instance.CurMonsters.Count; j++)
            {
                if (StageManager.instance.CurMonsters[j].monster.monsterName == StageManager.instance.stage[x].subStage[y].NewTutorial.Tutorial_MonsterName[i])
                {
                    ExplainQueue.Enqueue(() =>
                    {
                        MonsterPlay TempMonster = StageManager.instance.CurMonsters[j];
                        SetStageTutorial(TempMonster.gameObject,TempMonster.monster.Name, MonsterDescription[TempMonster.monster.monsterName]);
                    });
                    break;
                }
            }
        }

        for (int i = 0; i < StageManager.instance.stage[x].subStage[y].NewTutorial.Tutorial_obstacle_name.Length; i++)
        {
            for (int j = 0; j < StageManager.instance.CurObstacle.Count; j++)
            {

                if (StageManager.instance.CurObstacle[j].MyObstacleName == StageManager.instance.stage[x].subStage[y].NewTutorial.Tutorial_obstacle_name[i])
                {
                    ExplainQueue.Enqueue(() =>
                    {
                        Obstacle TempObstacle = StageManager.instance.CurObstacle[j];
                        SetStageTutorial(TempObstacle.gameObject,TempObstacle.MyObstacleName.ToString(), ObstacleDescription[TempObstacle.MyObstacleName]);
                    });
                    break;
                }

            }
        }
        
        if(ExplainQueue.Count > 0)
        {
            MovePanel.SetActive(false);
            StartCoroutine(DelayExplain());
        }

    }

    IEnumerator DelayExplain()
    {
        yield return new WaitForSeconds(2.0f);
        ExplainQueue.Dequeue()();
    }

    public void SetStageTutorial(GameObject Obj,string Title, string Des)
    {
        TitleText.text = Title;
        ExPlainText.text = Des;
        Camera.main.transform.position = new Vector3(Obj.transform.position.x,Obj.transform.position.y,Camera.main.transform.position.z);
        ExplainCanvas.SetActive(true);

        if(!Obj.activeSelf)
        {
            ActiveObj = Obj;
            ActiveObj.SetActive(true);
        }
        
    }


    private void Awake()
    {
        CameraOrgPos = Camera.main.transform.position;

        x = (int)StageManager.instance.CurStage.x - 1;
        y = (int)StageManager.instance.CurStage.y - 1;


        if (PlayerDB.Instance.playerdata.MyStageData[x].StageStar[y] > 0 || !StageManager.instance.stage[x].subStage[y].NewTutorial.IsTutorial) 
            return;

            InitDic();
    }

    private void Start()
    {
        if(MonsterDescription.Count > 0)
            FindAndPushQueue(x,y);
    }
}
