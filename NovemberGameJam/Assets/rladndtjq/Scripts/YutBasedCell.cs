using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class YutBasedCell : MonoBehaviour
{
    public bool playerOneArrived;
    public bool playerTwoArrived;

    public abstract void MoveToward(PlayerPiece player);
    public void MoveBackward(PlayerPiece player)
    {
        player.currentCell = player.previousCells[player.previousCells.Count - 1];
        player.transform.DOMove(player.previousCells[player.previousCells.Count - 1].transform.position, 0.35f).SetEase(Ease.OutCubic);
        player.previousCells.RemoveAt(player.previousCells.Count - 1);
    }
}
