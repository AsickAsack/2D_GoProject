using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Ready,Shot,End

}

public class GamePlayLogic : MonoBehaviour
{

    GameState gameState = GameState.Ready;

    public void ChangeState(GameState s)
    {
        if (s == gameState) return;

        gameState = s;

        switch(s)
        {
            case GameState.Ready:
                // ������ UIŬ���ؼ� �� ��ġ�ϱ� -> 
                break;

            case GameState.Shot:
                
                break;

            case GameState.End:
                break;
        }
    }

    public void GameLoop()
    {
        switch (gameState)
        {
            case GameState.Ready:
                break;

            case GameState.Shot:
                break;

            case GameState.End:
                break;
        }
    }

}
