using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YutMiddleCell : YutBasedCell
{
    [SerializeField] YutBasedCell toThirdCell;
    [SerializeField] YutBasedCell toFinalCell;

    [SerializeField] YutBasedCell fromFirstCell;
    [SerializeField] YutBasedCell fromSecondCell;

    public override void MoveToward(PlayerPiece player)
    {
        if (player.playerType == 0)
        {
            if (playerOneArrived == true)
            {
                player.previousCells.Add(player.currentCell);
                player.currentCell = toFinalCell;
                player.transform.DOMove(toFinalCell.transform.position, 0.35f).SetEase(Ease.OutQuad);

                if (player.moveCount <= 0)
                    toFinalCell.playerOneArrived = true;

                playerOneArrived = false;
            }
            else
            {
                if (player.previousCells[player.previousCells.Count - 1] == fromFirstCell)
                {
                    player.previousCells.Add(player.currentCell);
                    player.currentCell = toThirdCell;
                    player.transform.DOMove(toThirdCell.transform.position, 0.35f).SetEase(Ease.OutQuad);

                    if (player.moveCount <= 0)
                        toThirdCell.playerOneArrived = true;

                    playerOneArrived = false;
                }
                if (player.previousCells[player.previousCells.Count - 1] == fromSecondCell)
                {
                    player.previousCells.Add(player.currentCell);
                    player.currentCell = toFinalCell;
                    player.transform.DOMove(toFinalCell.transform.position, 0.35f).SetEase(Ease.OutQuad);


                    if (player.moveCount <= 0)
                        toFinalCell.playerOneArrived = true;

                    playerOneArrived = false;
                }
            }
        }
        if (player.playerType == 1)
        {
            if (playerTwoArrived == true)
            {
                player.currentCell = toFinalCell;
                player.transform.DOMove(toFinalCell.transform.position, 0.35f).SetEase(Ease.OutQuad);

                if (player.moveCount <= 0)
                    toFinalCell.playerTwoArrived = true;

                playerTwoArrived = false;
            }
            else
            {
                if (player.previousCells[player.previousCells.Count - 1] == fromFirstCell)
                {
                    player.currentCell = toThirdCell;
                    player.transform.DOMove(toThirdCell.transform.position, 0.35f).SetEase(Ease.OutQuad);

                    if (player.moveCount <= 0)
                        toFinalCell.playerTwoArrived = true;

                    playerTwoArrived = false;
                }
                if (player.previousCells[player.previousCells.Count - 1] == fromSecondCell)
                {
                    player.currentCell = toFinalCell;
                    player.transform.DOMove(toFinalCell.transform.position, 0.35f).SetEase(Ease.OutQuad);

                    if (player.moveCount <= 0)
                        toFinalCell.playerTwoArrived = true;

                    playerTwoArrived = false;
                }
            }
        }
    }
}
