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

    public int Count = 0;
    public TMPro.TMP_Text Select_CountTX;

    public int Pointer = 0;

    private void Awake()
    {
        Instance = this;
        TempSelect_Char = new Character[3];
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
            MySelected_Char[i].ResetBtn();
        }

    }


    //캐릭터 선택 UI열기
    public void OpenCharacterSelectUI()
    {

        if (StageManager.instance.stage.Length < (int)StageManager.instance.CurStage.x)
        {
                PopUpManager.Instance.OpenPopup(0, "안내", "아직 구현되지 않은 \n스테이지 입니다.", null);
                return;
        }
        else
        {
            if (StageManager.instance.stage[((int)StageManager.instance.CurStage.x) - 1].subStage.Length < (int)StageManager.instance.CurStage.y)
            {
                PopUpManager.Instance.OpenPopup(0, "안내", "아직 구현되지 않은 \n스테이지 입니다.", null);
                return;
            }
        }

        StageCanvas.enabled = true;

            Set_CountText(0);
        
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
        Select_CountTX.text = this.Count + " / 3";
    }

    public int GetPointer()
    {
        for(int i=0;i< MySelected_Char.Length;i++)
        {
            if(!MySelected_Char[i].IsSelected)
            {
                Debug.Log(i);
                return i;
            }
        }

        return -1;
    }


    //확인을 누르면 List에 Add후에 씬 이동
    public void Confirm_StageCharacter()
    {
        if(Count != 3)
        {
            PopUpManager.Instance.OpenPopup(0, "안내", "캐릭터를 다 선택 해주세요.", null);
            return;
        }

        for(int i=0;i < TempSelect_Char.Length;i++)
        {
            StageManager.instance.SelectCharacters.Add(TempSelect_Char[i]);
        }

        SceneLoader.Instance.Loading_LoadScene(2);
    } 

}
