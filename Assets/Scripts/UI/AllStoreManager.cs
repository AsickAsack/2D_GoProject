using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllStoreManager : MonoBehaviour
{
    public GameObject[] Menu;


    public void GoMorphingScene()
    {
        Character TutorialCharacter1 = new Character((int)CharacterName.Strong);
        StageManager.instance.SelectCharacters.Add(TutorialCharacter1);
        StageManager.instance.IsMorphing = true;

        SceneLoader.Instance.Loading_LoadScene(2);
    }

    public void GiveTicket()
    {
        if (PlayerDB.Instance.Ticket == PlayerDB.Instance.MaxTicket)
        {
            PopUpManager.Instance.OpenPopup(0, "�˸�", "�̹� Ƽ���� �ִ�ġ �Դϴ�.",null);
            return;
        }
        else if(PlayerDB.Instance.Ticket+10 > PlayerDB.Instance.MaxTicket)
        {
            PopUpManager.Instance.OpenPopup(0, "Ƽ�� ȹ��", $"Ƽ�� { PlayerDB.Instance.MaxTicket - PlayerDB.Instance.Ticket} ���� ȹ�� �߽��ϴ�.\n���� Ƽ�� : {PlayerDB.Instance.MaxTicket} ��");
            PlayerDB.Instance.Ticket = 30;
            
            PlayerDB.Instance.SaveData();
        }
        else
        {
            PlayerDB.Instance.Ticket += 10;
            PopUpManager.Instance.OpenPopup(0, "Ƽ�� ȹ��", $"Ƽ�� 10���� ȹ�� �߽��ϴ�.\n���� Ƽ�� : {PlayerDB.Instance.Ticket} ��");
            PlayerDB.Instance.SaveData();
        }

    }


    public void ClickMenu(int index)
    {
        for(int i = 0; i < Menu.Length; i++)
        {
            Menu[i].SetActive(i == index);
        }
    }

}
