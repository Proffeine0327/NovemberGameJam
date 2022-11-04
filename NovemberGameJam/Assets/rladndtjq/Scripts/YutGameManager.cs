using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class YutGameManager : MonoBehaviour
{
    public static YutGameManager manager;

    public YutManager yutManager;
    public PlayerPiece[] players;
    public int currentTurnPlayerIndex;
    public YutBasedCell startCell;
    public YutBasedCell lastCell;
    public bool isGettingResult;
    public float power;
    public bool isEnd = false;
    public bool isPlayGame = false;
    public bool isPlayMiniGame = false;
    public int pieceAmount;
    public int miniGameTurnCount;
    public GameObject disable;
    public string[] miniGameSceneNames;
    [Header("UI")]
    public Image powerBarImage;
    public TextMeshProUGUI throwResultText;
    public Image fadePanel;
    public ExplainScript explain;
    public Image miniGameList;
    public RectTransform miniGameListArrow;
    public RectTransform[] arrowPositions;
    public TextMeshProUGUI finalEndWinner;
    public GameObject pressanykey;

    bool isExplainShowEnd;
    int currentMiniGameTurnCount;
    SpriteRenderer[] playerSRs;

    private void Awake()
    {
        manager = this;
        DontDestroyOnLoad(manager);

        powerBarImage.gameObject.SetActive(false);
        throwResultText.gameObject.SetActive(false);
        fadePanel.gameObject.SetActive(true);

        var playersr =
            from player in players
            select player.GetComponent<SpriteRenderer>();

        playerSRs = playersr.ToArray();

        foreach (var player in players) player.mark.SetActive(false);

        StartCoroutine(fadeIn());
    }

    IEnumerator fadeIn()
    {
        var fadePanelColor = fadePanel.color;
        fadePanelColor.a = 1;
        fadePanel.color = fadePanelColor;

        for (float i = 1; i > 0.8f; i -= Time.deltaTime / 3)
        {
            fadePanelColor.a = i;
            fadePanel.color = fadePanelColor;
            yield return new WaitForEndOfFrame();
        }

        explain.showExplain(new Vector2(0, 1200), new Vector2(0, 0), () =>
        {
            isExplainShowEnd = true;
        });

        while (!isExplainShowEnd) yield return new WaitForEndOfFrame();

        yield return new WaitForSecondsRealtime(1f);

        for (float t = fadePanelColor.a; t > 0; t -= Time.deltaTime / 2)
        {
            fadePanelColor.a = t;
            fadePanel.color = fadePanelColor;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        isPlayGame = true;
    }

    void Update()
    {
        if (isPlayGame && !isEnd)
        {
            if (!isGettingResult)
            {
                players[currentTurnPlayerIndex].mark.SetActive(true);

                var color = playerSRs[currentTurnPlayerIndex == 0 ? 1 : 0].color;
                color.a = 0.6f;
                playerSRs[currentTurnPlayerIndex == 0 ? 1 : 0].color = color;

                if (Input.GetMouseButton(0) && power < 1)
                {
                    powerBarImage.gameObject.SetActive(true);
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
                if (players[currentTurnPlayerIndex].currentCell != startCell)
                    isBackDo = true;
                throwResultText.text = "빽도!";
                break;
            case ThrowResult.Do:
                players[currentTurnPlayerIndex].moveCount = 1;
                throwResultText.text = "도!";
                break;
            case ThrowResult.Gae:
                players[currentTurnPlayerIndex].moveCount = 2;
                throwResultText.text = "개!";
                break;
            case ThrowResult.Girl:
                players[currentTurnPlayerIndex].moveCount = 3;
                throwResultText.text = "걸!";
                break;
            case ThrowResult.Yut:
                players[currentTurnPlayerIndex].moveCount = 4;
                throwResultText.text = "윷!";
                break;
            case ThrowResult.Mo:
                players[currentTurnPlayerIndex].moveCount = 5;
                throwResultText.text = "모!";
                break;

        }

        yield return new WaitForSecondsRealtime(1.75f);
        throwResultText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        throwResultText.gameObject.SetActive(false);

        if (isBackDo)
        {
            players[currentTurnPlayerIndex].currentCell.MoveBackward(players[currentTurnPlayerIndex], 0.35f);
            if (players[currentTurnPlayerIndex].currentCell.isCoinCell)
                players[currentTurnPlayerIndex].coinAmount += 5;

            if (players[currentTurnPlayerIndex].currentCell != startCell)
                if (players[currentTurnPlayerIndex].currentCell == players[currentTurnPlayerIndex == 0 ? 1 : 0].currentCell)
                {
                    if (players[currentTurnPlayerIndex == 0 ? 1 : 0].coinAmount > 5)
                    {
                        players[currentTurnPlayerIndex == 0 ? 1 : 0].coinAmount -= 5;
                        players[currentTurnPlayerIndex].coinAmount += 5;
                    }
                }
        }
        else
        {
            bool isMove = players[currentTurnPlayerIndex].moveCount > 0 ? true : false;
            while (players[currentTurnPlayerIndex].moveCount > 0)
            {
                players[currentTurnPlayerIndex].moveCount--;
                players[currentTurnPlayerIndex].currentCell.MoveToward(players[currentTurnPlayerIndex], 0.35f);
                yield return new WaitForSeconds(0.36f);
                if (players[currentTurnPlayerIndex].currentCell == lastCell)
                    break;
            }

            if (isMove)
            {
                if (players[currentTurnPlayerIndex].currentCell.isCoinCell)
                    players[currentTurnPlayerIndex].coinAmount += 5;

                if (players[currentTurnPlayerIndex].currentCell == players[currentTurnPlayerIndex == 0 ? 1 : 0].currentCell)
                {
                    if (players[currentTurnPlayerIndex == 0 ? 1 : 0].coinAmount > 5)
                    {
                        players[currentTurnPlayerIndex == 0 ? 1 : 0].coinAmount -= 5;
                        players[currentTurnPlayerIndex].coinAmount += 5;
                    }
                }
            }
        }

        if (players[currentTurnPlayerIndex].currentCell == lastCell)
        {
            if (players[currentTurnPlayerIndex].cycleCount < pieceAmount)
            {
                yield return new WaitForSeconds(0.3f);
                players[currentTurnPlayerIndex].previousCells.Clear();
                players[currentTurnPlayerIndex].currentCell = startCell;
                players[currentTurnPlayerIndex].moveCount = 0;
                players[currentTurnPlayerIndex].transform.DOMove(startCell.transform.position, 0.5f);
                yield return new WaitForSeconds(0.5f);
                players[currentTurnPlayerIndex].cycleCount++;
            }
            else
            {
                isEnd = true;
                finalEndWinner.gameObject.SetActive(true);
                pressanykey.SetActive(true);
                fadePanel.gameObject.SetActive(true);

                var color = fadePanel.color;
                color.a = 1;
                fadePanel.color = color;
                
                players[currentTurnPlayerIndex].coinAmount += 25;
                finalEndWinner.text = players[0].coinAmount > players[1].coinAmount? "김 대감 승리!" : "정 대감 승리!";
                while(true)
                {
                    if(Input.anyKeyDown)
                        break;
                    yield return new WaitForEndOfFrame();
                }
                SceneManager.LoadScene("Title");
            }
        }

        if (!players[currentTurnPlayerIndex].isContinuity)
        {
            if (throwResult != ThrowResult.Mo && throwResult != ThrowResult.Yut)
            {
                currentTurnPlayerIndex = currentTurnPlayerIndex == 0 ? 1 : 0;
            }
            else
            {
                players[currentTurnPlayerIndex].isContinuity = true;
            }
        }
        else
        {
            players[currentTurnPlayerIndex].isContinuity = false;
            currentTurnPlayerIndex = currentTurnPlayerIndex == 0 ? 1 : 0;
        }
        yield return new WaitForSecondsRealtime(1.4f);
        for (int i = 0; i < players.Length; i++)
        {
            var color = playerSRs[i].color;
            color.a = 1;
            playerSRs[i].color = color;
        }
        isGettingResult = false;

        if (currentMiniGameTurnCount < miniGameTurnCount)
        {
            currentMiniGameTurnCount++;
        }
        else
        {
            isPlayGame = false;
            var rand = Random.Range(0, miniGameSceneNames.Length);
            var randSceneName = miniGameSceneNames[rand];

            miniGameList.gameObject.SetActive(true);
            miniGameListArrow.gameObject.SetActive(false);
            var fadeColor = fadePanel.color;
            fadeColor.a = 0.7f;
            fadePanel.color = fadeColor;

            miniGameListArrow.gameObject.SetActive(true);
            for(int i = 0; i < 21 + rand; i++)
            {
                miniGameListArrow.anchoredPosition = arrowPositions[i % 4].anchoredPosition;

                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2f);
            miniGameList.gameObject.SetActive(false);
            
            for (float i = fadeColor.a; i < 1; i += Time.deltaTime / 2)
            {
                var fadePanelColor = fadePanel.color;
                fadePanelColor.a = i;
                fadePanel.color = fadePanelColor;
                yield return new WaitForEndOfFrame();
            }
            disable.SetActive(false);
            SceneManager.LoadSceneAsync(randSceneName, LoadSceneMode.Additive);

            isPlayMiniGame = true;
            do
            {
                yield return new WaitForEndOfFrame();
            } while (isPlayMiniGame);

            SceneManager.UnloadSceneAsync(randSceneName);
            disable.SetActive(true);
            for (float t = 1; t > 0; t -= Time.deltaTime / 2)
            {
                var fadePanelColor = fadePanel.color;
                fadePanelColor.a = t;
                fadePanel.color = fadePanelColor;
                yield return new WaitForEndOfFrame();
            }
            isPlayGame = true;
            currentMiniGameTurnCount = 0;
        }

        foreach (var yut in yutManager.yuts) yut.Reset();
        foreach (var player in players) player.mark.SetActive(false);
    }
}
