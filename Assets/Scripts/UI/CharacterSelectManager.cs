using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager Instance;

    public Canvas StageCanvas;
    //ĳ���͸� ���� ����
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


    //ĳ���� ���� UI����
    public void OpenCharacterSelectUI()
    {

        if (StageManager.instance.stage.Length < (int)StageManager.instance.CurStage.x)
        {
                PopUpManager.Instance.OpenPopup(0, "�ȳ�", "���� �������� ���� \n�������� �Դϴ�.", null);
                return;
        }
        else
        {
            if (StageManager.instance.stage[((int)StageManager.instance.CurStage.x) - 1].subStage.Length < (int)StageManager.instance.CurStage.y)
            {
                PopUpManager.Instance.OpenPopup(0, "�ȳ�", "���� �������� ���� \n�������� �Դϴ�.", null);
                return;
            }
        }

        StageCanvas.enabled = true;

            Set_CountText(0);
        
        for (int i = 0; i < PlayerDB.Instance.MyCharacters.Count;i++)
        {
            //���� ĳ���� ����ŭ ������ ������ ���� ����
            MySelectList[i].SetBtn(PlayerDB.Instance.MyCharacters[i]);
        }
    }

    //�����Ҷ����� ī��Ʈ �ؽ�Ʈ �ٲپ��ִ� �Լ�
    public void Set_CharacterIcon(Character myChar,int index)
    {

        

        //�ؽ�Ʈ ǥ�����ְ� ������ ǥ��, ���̶�� ���������
        if (myChar == null)
        {
            //������ �ε��� ����� , ������ �� �ε����� 
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


    //Ȯ���� ������ List�� Add�Ŀ� �� �̵�
    public void Confirm_StageCharacter()
    {
        if(Count != 3)
        {
            PopUpManager.Instance.OpenPopup(0, "�ȳ�", "ĳ���͸� �� ���� ���ּ���.", null);
            return;
        }

        for(int i=0;i < TempSelect_Char.Length;i++)
        {
            StageManager.instance.SelectCharacters.Add(TempSelect_Char[i]);
        }

        SceneLoader.Instance.Loading_LoadScene(2);
    } 

}
