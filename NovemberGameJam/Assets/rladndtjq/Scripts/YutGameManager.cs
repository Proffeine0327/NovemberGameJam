using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YutGameManager : MonoBehaviour
{
    public static YutGameManager manager;

    public YutManager yutManager;
    public PlayerPiece[] players;
    public int currentTurnPlayer;
    public YutBasedCell startCell;
    public YutBasedCell lastCell;
    public bool isGettingResult;

    private void Awake()
    {
        manager = this;
    }

    void Update()
    {
        if (!isGettingResult)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGettingResult = true;
                StartCoroutine(ThrowingYuts());
            }
        }
    }

    IEnumerator ThrowingYuts()
    {
        bool isBackDo = false;
        switch (yutManager.ThrowYutsNGetResult())
        {
            case ThrowResult.BackDo:
                if (players[currentTurnPlayer].currentCell != startCell)
                    isBackDo = true;
                break;
            case ThrowResult.Do:
                players[currentTurnPlayer].moveCount = 1;
                break;
            case ThrowResult.Gae:
                players[currentTurnPlayer].moveCount = 2;
                break;
            case ThrowResult.Girl:
                players[currentTurnPlayer].moveCount = 3;
                break;
            case ThrowResult.Yut:
                players[currentTurnPlayer].moveCount = 4;
                break;
            case ThrowResult.Mo:
                players[currentTurnPlayer].moveCount = 5;
                break;

        }

        yield return new WaitForSecondsRealtime(1f);

        if (isBackDo)
        {
            players[currentTurnPlayer].currentCell.MoveBackward(players[currentTurnPlayer]);
        }
        else
        {
            while (players[currentTurnPlayer].moveCount > 0)
            {
                players[currentTurnPlayer].moveCount--;
                players[currentTurnPlayer].currentCell.MoveToward(players[currentTurnPlayer]);
                yield return new WaitForSeconds(0.36f);
                if (players[currentTurnPlayer].currentCell == lastCell)
                    break;
            }
        }

        currentTurnPlayer = currentTurnPlayer == 0 ? 1 : 0;
        yield return new WaitForSecondsRealtime(1.4f);
        isGettingResult = false;
    }
}
