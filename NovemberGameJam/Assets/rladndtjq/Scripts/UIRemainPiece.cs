using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRemainPiece : MonoBehaviour
{
    public Image[] pieces;
    public int playerIndex;

    void Update()
    {
        if(YutGameManager.manager.pieceAmount - YutGameManager.manager.players[playerIndex].cycleCount + 1 < pieces.Length)
        {
            pieces[YutGameManager.manager.pieceAmount - YutGameManager.manager.players[playerIndex].cycleCount + 1].gameObject.SetActive(false);
        }
    }
}
