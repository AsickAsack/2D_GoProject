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
            { MonsterName.Big , "덩치가 커서 맞히기 쉬운 말이지만 그만큼 덜 밀려난다."},
            { MonsterName.Bomb , "적 위에 쓰여진 턴에 충격을 받을시 주변에 폭발을 일으킨다."},
            { MonsterName.Boss, "다른 말과 충돌하면 졸개 2마리를 생성한다.\n한 턴에 한 번만 발동된다."},
            { MonsterName.Armor , "첫 번째 충돌은 유닛이 입고 있는 투구가 막아준다.\n첫 충돌 이후 투구가 날아가 일반 유닛과 같게된다."},
            { MonsterName.Silence , "부딪히는 모든 아군 캐릭터의 스킬을 무효로 만드는 적이다."},

        };

        ObstacleDescription = new Dictionary<ObstacleName, string>()
        {
            { ObstacleName.Wall , "부딪히면 튕겨나오는 벽이다."},
            { ObstacleName.BlackHole , "가까이 오는 유닛을 전부 딸아들여버리는 블랙홀이다."},
            { ObstacleName.Toggle , "다른 말과 충돌하면 맵의 특정한 기믹을 키거나 끈다.\n한 턴에 한 번만 발동된다."},
            { ObstacleName.Booster , "발판에 닿은 유닛을 해당 방향으로 날려보낸다."},
            { ObstacleName.Stop , "이 발판에 닿은 말은 속도가 급감속하게 된다."},
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
