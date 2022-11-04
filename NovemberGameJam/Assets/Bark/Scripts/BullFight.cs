using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BullFight : MonoBehaviour
{
    [Header("인게임")]
    [SerializeField] private GameObject Cow1, Cow2;
    private bool isPlayGame;

    private int TouchNum;

    [Header("UI")]
    public Image fadePanel;
    public ExplainScript explain;
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI win;

    bool isExplainShowEnd;

    bool isEndGameOnce = false;

    private void Start()
    {
        StartCoroutine(fadeIn());
    }
    private void Update()
    {
        Cow1.transform.DOMove(new Vector2(-4f + (TouchNum / 4f), 0), 0.1f);
        Cow2.transform.DOMove(new Vector2(4f + (TouchNum / 4f), 0), 0.1f);
        GameEnd();
        InputTouch();
    }
    IEnumerator fadeIn()
    {
        countDown.gameObject.SetActive(false);
        fadePanel.gameObject.SetActive(true);
        win.gameObject.SetActive(false);
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
        countDown.gameObject.SetActive(true);
        countDown.text = "3";
        yield return new WaitForSeconds(1);
        countDown.text = "2";
        yield return new WaitForSeconds(1);
        countDown.text = "1";
        yield return new WaitForSeconds(1);
        countDown.text = "시작!";
        yield return new WaitForSeconds(1);

        isPlayGame = true;
        countDown.gameObject.SetActive(false);
    }
    IEnumerator EndGame()
    {
        win.gameObject.SetActive(true);
        win.text = TouchNum >= 0 ? "김 대감 우승!" : "정 대감 우승!";
        yield return new WaitForSeconds(1);
        for (float t = fadePanel.color.a; t < 1; t += Time.deltaTime / 2)
        {
            var fadePanelColor = fadePanel.color;
            fadePanelColor.a = t;
            fadePanel.color = fadePanelColor;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("YutPlay"));

        if (TouchNum > 0)
            YutGameManager.manager.players[0].coinAmount += 10; //김대감 1번쨰 플레이어
        else
            YutGameManager.manager.players[1].coinAmount += 10; //정대감 2번째 플레이어

        YutGameManager.manager.isPlayMiniGame = false;
    }
    void InputTouch()
    {
        if (isPlayGame)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
            {
                TouchNum++;
            }
            if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
            {
                TouchNum--;
            }
        }
    }
    void GameEnd()
    {
        if (TouchNum >= 42)
        {
            isPlayGame = false;
            if (!isEndGameOnce)
            {

                isEndGameOnce = true;
                StartCoroutine(EndGame());
            }
        }
        else if (TouchNum <= -42)
        {
            isPlayGame = false;
            if (!isEndGameOnce)
            {
                isEndGameOnce = true;
                StartCoroutine(EndGame());
            }
        }
    }
}
