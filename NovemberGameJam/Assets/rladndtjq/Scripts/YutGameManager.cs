using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YutGameManager : MonoBehaviour
{
    public static YutGameManager manager;

    public YutManager yutManager;
    public PlayerPiece[] players;
    public int currentTurnPlayer;
    public YutBasedCell startCell;
    public YutBasedCell lastCell;
    public bool isGettingResult;
    public float power;
    [Header("UI")]
    public Image powerBarImage;
    public TextMeshProUGUI throwResultText;

    private void Awake()
    {
        manager = this;
        powerBarImage.gameObject.SetActive(false);
        throwResultText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isGettingResult)
        {
            if (Input.GetMouseButtonDown(0))
            {
                powerBarImage.gameObject.SetActive(true);
            }

            if (Input.GetMouseButton(0) && power < 1)
            {
                power += Time.deltaTime / 2;
                powerBarImage.fillAmount = power;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isGettingResult = true;
                powerBarImage.gameObject.SetActive(false);
                StartCoroutine(ThrowingYuts());
                power = 0;
            }
        }
    }

    IEnumerator ThrowingYuts()
    {
        bool isBackDo = false;
        var throwResult = yutManager.ThrowYutsNGetResult(power);
        switch (throwResult)
        {
            case ThrowResult.Fail:
                throwResultText.text = "낙!";
                break;
            case ThrowResult.BackDo:
                if (players[currentTurnPlayer].currentCell != startCell)
                    isBackDo = true;
                throwResultText.text = "빽도!";
                break;
            case ThrowResult.Do:
                players[currentTurnPlayer].moveCount = 1;
                throwResultText.text = "도!";
                break;
            case ThrowResult.Gae:
                players[currentTurnPlayer].moveCount = 2;
                throwResultText.text = "개!";
                break;
            case ThrowResult.Girl:
                players[currentTurnPlayer].moveCount = 3;
                throwResultText.text = "걸!";
                break;
            case ThrowResult.Yut:
                players[currentTurnPlayer].moveCount = 4;
                throwResultText.text = "윷!";
                break;
            case ThrowResult.Mo:
                players[currentTurnPlayer].moveCount = 5;
                throwResultText.text = "모!";
                break;

        }

        yield return new WaitForSecondsRealtime(1.75f);
        throwResultText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        throwResultText.gameObject.SetActive(false);

        if (isBackDo)
        {
            players[currentTurnPlayer].currentCell.MoveBackward(players[currentTurnPlayer], 0.35f);
        }
        else
        {
            while (players[currentTurnPlayer].moveCount > 0)
            {
                players[currentTurnPlayer].moveCount--;
                players[currentTurnPlayer].currentCell.MoveToward(players[currentTurnPlayer], 0.35f);
                yield return new WaitForSeconds(0.36f);
                if (players[currentTurnPlayer].currentCell == lastCell)
                    break;
            }
        }

        if (!players[currentTurnPlayer].isContinuity)
        {
            if (throwResult != ThrowResult.Mo && throwResult != ThrowResult.Yut)
                currentTurnPlayer = currentTurnPlayer == 0 ? 1 : 0;
        }
        else
        {
            players[currentTurnPlayer].isContinuity = true;
            currentTurnPlayer = currentTurnPlayer == 0 ? 1 : 0;
        }
        yield return new WaitForSecondsRealtime(1.4f);
        isGettingResult = false;
        foreach(var yut in yutManager.yuts) yut.Reset();
    }
}
