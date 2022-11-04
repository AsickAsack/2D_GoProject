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
                // 오른쪽 UI클릭해서 말 터치하기 -> 
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
