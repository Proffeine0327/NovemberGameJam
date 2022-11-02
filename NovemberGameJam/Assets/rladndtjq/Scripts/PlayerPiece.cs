using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public YutBasedCell currentCell;
    public YutBasedCell lastCell;
    public List<YutBasedCell> previousCells = new List<YutBasedCell>();
    public int playerType; //0 == playerOne, 1 == playerTwo;
    public int moveCount;
    public bool isTurn;
}
