using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YutSecondSideCell : YutBasedCell
{
    [SerializeField] YutBasedCell wareCell; //넘어가기
    [SerializeField] YutBasedCell arriveCell; //도착

    public override void MoveToward(PlayerPiece player, float animationTime)
    {
        if (player.playerType == 0)
        {
            if (playerOneArrived == true)
            {
                player.previousCells.Add(player.currentCell);
                player.currentCell = arriveCell;
                player.transform.DOMove(arriveCell.transform.position, animationTime).SetEase(Ease.OutQuad);
                if (player.moveCount <= 0)
                    arriveCell.playerOneArrived = true;
                player.transform.DOMove(arriveCell.transform.position, animationTime).SetEase(Ease.OutQuad);
            }
            else
            {
                player.previousCells.Add(player.currentCell);
                player.currentCell = wareCell;
                player.transform.DOMove(wareCell.transform.position, animationTime).SetEase(Ease.OutQuad);
                if (player.moveCount <= 0)
                    wareCell.playerOneArrived = true;
                player.transform.DOMove(wareCell.transform.position, animationTime).SetEase(Ease.OutQuad);
            }
            playerOneArrived = false;
        }
        if (player.playerType == 1)
        {
            if (playerTwoArrived == true)
            {
                player.previousCells.Add(player.currentCell);
                player.currentCell = arriveCell;
                player.transform.DOMove(arriveCell.transform.position, animationTime).SetEase(Ease.OutQuad);
                if (player.moveCount <= 0)
                    arriveCell.playerOneArrived = true;
                player.transform.DOMove(arriveCell.transform.position, animationTime).SetEase(Ease.OutQuad);
            }
            else
            {
                player.previousCells.Add(player.currentCell);
                player.currentCell = wareCell;
                player.transform.DOMove(wareCell.transform.position, animationTime).SetEase(Ease.OutQuad);
                if (player.moveCount <= 0)
                    wareCell.playerOneArrived = true;
                player.transform.DOMove(wareCell.transform.position, animationTime).SetEase(Ease.OutQuad);
            }
            playerTwoArrived = false;
        }
    }
}