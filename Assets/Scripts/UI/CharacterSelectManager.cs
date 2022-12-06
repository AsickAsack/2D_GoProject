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


    //ĳ���� ���� UI����
    public void OpenCharacterSelectUI(int x, int y)
    {

        if (StageManager.instance.stage.Length < x)
        {
                PopUpManager.Instance.OpenPopup(0, "�ȳ�", "���� �������� ���� \n�������� �Դϴ�.", null);
                return;
        }
        else
        {
            if (StageManager.instance.stage[x - 1].subStage.Length < y)
            {
                PopUpManager.Instance.OpenPopup(0, "�ȳ�", "���� �������� ���� \n�������� �Դϴ�.", null);
                return;
            }
        }

        if (PlayerDB.Instance.MyCharacters.Count < StageManager.instance.stage[x-1].subStage[y-1].NeedCharacter)
        {
            PopUpManager.Instance.OpenPopup(0, "�˸�", $"�� ���������� �ʿ���\n ĳ������ ���� �����մϴ�.\n\n {x}-{y} �������� \n�ʿ� ĳ���� �� : {StageManager.instance.stage[x-1].subStage[y-1].NeedCharacter}", null);
            return;
        }

        StageCanvas.enabled = true;

        Set_CountText(0);
        AcitveCharacterBox();

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

    //�ʿ��� ĳ���� ����ŭ UI������ �����ϰ� ��Ƽ�� ��Ű��
    public void AcitveCharacterBox()
    {
        float size = basicSize - (MinusSize * (StageManager.instance.GetNeedCharacterCount() - 1));

        for (int i = 0; i < StageManager.instance.GetNeedCharacterCount(); i++)
        {
            MySelected_Char[i].gameObject.SetActive(true);
            MySelected_Char[i].myRect.sizeDelta = new Vector2(size,size);
        }
    }


    //Ȯ���� ������ List�� Add�Ŀ� �� �̵�
    public void Confirm_StageCharacter()
    {
        if(Count != StageManager.instance.GetNeedCharacterCount())
        {
            PopUpManager.Instance.OpenPopup(0, "�ȳ�", "ĳ���͸� �� ���� ���ּ���.", null);
            return;
        }

        if(PlayerDB.Instance.myUserSkill == null)
        {
            PopUpManager.Instance.OpenPopup(0, "�ȳ�", "���� ��ų�� ���� ���ּ���.", null);
            return;

        }

        for(int i=0;i < TempSelect_Char.Length;i++)
        {
            StageManager.instance.SelectCharacters.Add(TempSelect_Char[i]);
        }

        SceneLoader.Instance.Loading_LoadScene(2);
    } 

}
