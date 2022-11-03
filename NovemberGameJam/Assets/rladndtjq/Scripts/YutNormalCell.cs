using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YutNormalCell : YutBasedCell
{
    [SerializeField] YutBasedCell nextCell;

    public override void MoveToward(PlayerPiece player, float animationTime)
    {
        if (player.playerType == 0)
        {
            playerOneArrived = false;
            if (player.moveCount <= 0)
                nextCell.playerOneArrived = true;
        }
        if (player.playerType == 1)
        {
            playerTwoArrived = false;
            if (player.moveCount <= 0)
                nextCell.playerTwoArrived = true;
        }

        player.previousCells.Add(player.currentCell);
        player.currentCell = nextCell;
        player.transform.DOMove(nextCell.transform.position, animationTime).SetEase(Ease.OutQuad);
    }
}
