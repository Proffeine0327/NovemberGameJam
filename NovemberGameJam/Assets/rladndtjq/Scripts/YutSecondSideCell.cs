using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YutSecondSideCell : YutBasedCell
{
    [SerializeField] YutBasedCell wareCell; //넘어가기
    [SerializeField] YutBasedCell arriveCell; //도착

    public override void MoveToward(PlayerPiece player)
    {
        if (player.playerType == 0)
        {
            if (playerOneArrived == true)
            {
                player.currentCell = arriveCell;
                player.transform.DOMove(arriveCell.transform.position, 0.35f).SetEase(Ease.OutQuad);
                if (player.moveCount <= 0)
                    arriveCell.playerOneArrived = true;
                player.previousCells.Add(player.currentCell);
                player.transform.DOMove(arriveCell.transform.position, 0.35f).SetEase(Ease.OutQuad);
            }
            else
            {
                player.currentCell = wareCell;
                player.transform.DOMove(wareCell.transform.position, 0.35f).SetEase(Ease.OutQuad);
                if (player.moveCount <= 0)
                    wareCell.playerOneArrived = true;
                player.previousCells.Add(player.currentCell);
                player.transform.DOMove(wareCell.transform.position, 0.35f).SetEase(Ease.OutQuad);
            }
            playerOneArrived = false;
        }
        if (player.playerType == 1)
        {
            if (playerTwoArrived == true)
            {
                player.currentCell = arriveCell;
                player.transform.DOMove(arriveCell.transform.position, 0.35f).SetEase(Ease.OutQuad);
                if (player.moveCount <= 0)
                    arriveCell.playerOneArrived = true;
                player.previousCells.Add(player.currentCell);
                player.transform.DOMove(arriveCell.transform.position, 0.35f).SetEase(Ease.OutQuad);
            }
            else
            {
                player.currentCell = wareCell;
                player.transform.DOMove(wareCell.transform.position, 0.35f).SetEase(Ease.OutQuad);
                if (player.moveCount <= 0)
                    wareCell.playerOneArrived = true;
                player.previousCells.Add(player.currentCell);
                player.transform.DOMove(wareCell.transform.position, 0.35f).SetEase(Ease.OutQuad);
            }
            playerTwoArrived = false;
        }
    }
}