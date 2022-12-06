using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager Instance;

    public Canvas StageCanvas;
    //캐릭터를 담을 변수
    public Character[] TempSelect_Char;

    public CharacterSelectListUI[] MySelectList;
    public CharacterSelectUI[] MySelected_Char;
    public float basicSize;
    public float MinusSize;

    public int Count = 0;
    public TMPro.TMP_Text Select_CountTX;

    public int Pointer = 0;

    private void Awake()
    {
        Instance = this;
        TempSelect_Char = new Character[5];
    }

    public void ExitSelect()
    {
        for(int i=0;i<MySelectList.Length;i++)
        {
            if (MySelectList[i].gameObject.activeSelf)
                MySelectList[i].ResetBtn();
            else
                break;
        }

        for (int i = 0; i < MySelected_Char.Length; i++)
        {
            if (MySelected_Char[i].gameObject.activeSelf)
                MySelected_Char[i].ResetBtn();
            else
                break;
        }

    }


    //캐릭터 선택 UI열기
    public void OpenCharacterSelectUI(int x, int y)
    {

        if (StageManager.instance.stage.Length < x)
        {
                PopUpManager.Instance.OpenPopup(0, "안내", "아직 구현되지 않은 \n스테이지 입니다.", null);
                return;
        }
        else
        {
            if (StageManager.instance.stage[x - 1].subStage.Length < y)
            {
                PopUpManager.Instance.OpenPopup(0, "안내", "아직 구현되지 않은 \n스테이지 입니다.", null);
                return;
            }
        }

        if (PlayerDB.Instance.MyCharacters.Count < StageManager.instance.stage[x-1].subStage[y-1].NeedCharacter)
        {
            PopUpManager.Instance.OpenPopup(0, "알림", $"이 스테이지에 필요한\n 캐릭터의 수가 부족합니다.\n\n {x}-{y} 스테이지 \n필요 캐릭터 수 : {StageManager.instance.stage[x-1].subStage[y-1].NeedCharacter}", null);
            return;
        }

        StageCanvas.enabled = true;

        Set_CountText(0);
        AcitveCharacterBox();

        for (int i = 0; i < PlayerDB.Instance.MyCharacters.Count;i++)
        {
            //보유 캐릭터 수만큼 켜지고 아이콘 까지 세팅
            MySelectList[i].SetBtn(PlayerDB.Instance.MyCharacters[i]);
        }

        
    }

    //선택할때마다 카운트 텍스트 바꾸어주는 함수
    public void Set_CharacterIcon(Character myChar,int index)
    {

        //텍스트 표시해주고 아이콘 표시, 널이라면 지워줘야함
        if (myChar == null)
        {
            //정해진 인덱스 지우기 , 포인터 그 인덱스로 
            TempSelect_Char[index] = myChar;
            MySelected_Char[index].SetBtn(index,null);
            Count--;
        }
        else
        {
            Pointer = GetPointer();
            TempSelect_Char[Pointer] = myChar;
            MySelected_Char[Pointer].SetBtn(index,GameDB.Instance.GetCharacterIcon(myChar));
            Count++;
        }
        
        Set_CountText(this.Count);
    }

    public void Set_CountText(int Count)
    {
        this.Count = Count;
        Select_CountTX.text = this.Count + " / " + StageManager.instance.GetNeedCharacterCount();
    }

    public int GetPointer()
    {
        for(int i=0;i< MySelected_Char.Length;i++)
        {
            if(!MySelected_Char[i].IsSelected)
            {
                return i;
            }
        }

        return -1;
    }

    //필요한 캐릭터 수만큼 UI사이즈 설정하고 액티브 시키기
    public void AcitveCharacterBox()
    {
        float size = basicSize - (MinusSize * (StageManager.instance.GetNeedCharacterCount() - 1));

        for (int i = 0; i < StageManager.instance.GetNeedCharacterCount(); i++)
        {
            MySelected_Char[i].gameObject.SetActive(true);
            MySelected_Char[i].myRect.sizeDelta = new Vector2(size,size);
        }
    }


    //확인을 누르면 List에 Add후에 씬 이동
    public void Confirm_StageCharacter()
    {
        if(Count != StageManager.instance.GetNeedCharacterCount())
        {
            PopUpManager.Instance.OpenPopup(0, "안내", "캐릭터를 다 선택 해주세요.", null);
            return;
        }

        if(PlayerDB.Instance.myUserSkill == null)
        {
            PopUpManager.Instance.OpenPopup(0, "안내", "유저 스킬을 선택 해주세요.", null);
            return;

        }

        for(int i=0;i < TempSelect_Char.Length;i++)
        {
            StageManager.instance.SelectCharacters.Add(TempSelect_Char[i]);
        }

        SceneLoader.Instance.Loading_LoadScene(2);
    } 

}
