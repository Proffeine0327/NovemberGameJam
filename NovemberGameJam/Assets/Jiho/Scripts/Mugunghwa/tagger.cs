using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class tagger : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    TextMesh[] tm;
    [SerializeField]
    GameObject Mugunghwamanager;
    string dialogue = "무궁화 꽃이 피었습니다!";
    bool isturn = false;
    bool iscou = false;
    bool istext = false;
    bool isstart = false;
    public bool isnotend = true;
    public bool move = false;

    [Header("UI")]
    public Image fadePanel;
    public ExplainScript explain;
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI win;
    bool isExplainShowEnd;
    public int TouchNum;
    void Start()
    {
        StartCoroutine(fadeIn());
    }

    void Update()
    {
        if (isstart)
        {
            StartCoroutine(mugunghwastart());
        }
        if (istext == true && isnotend)
        {
            istext = false;
            StartCoroutine(printtext());
        }
        if (isturn == true)
        {
            if (Input.GetKey(KeyCode.S) && isnotend)
            {
                isnotend = false;
                tm[0].color = Color.red;
                tm[0].text = "Lose!";
                tm[1].color = Color.green;
                tm[1].text = "Win!";
                Mugunghwamanager.GetComponent<player>().enabled = false;
                TouchNum = -1;
                gameend();
            }
            if (Input.GetKey(KeyCode.L) && isnotend)
            {
                isnotend = false;
                tm[0].color = Color.green;
                tm[0].text = "Win!";
                tm[1].color = Color.red;
                tm[1].text = "Lose!";
                Mugunghwamanager.GetComponent<player>().enabled = false;
                TouchNum = 1;
                gameend();
            }
        }
        if (iscou == true)
        {
            iscou = false;
            StartCoroutine(turn());
        }
    }

    IEnumerator printtext()
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            WaitForSeconds wait = new WaitForSeconds(Random.Range(0.01f, 0.5f));
            text.text += dialogue[i];
            yield return wait;
        }
        isturn = true;
        iscou = true;
    }

    IEnumerator turn()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[1];
        yield return new WaitForSeconds(1.5f);
        text.text = "";
        isturn = false;
        if (isnotend)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        istext = true;
    }

    IEnumerator mugunghwastart()
    {
        isstart = false;
        yield return new WaitForSeconds(3f);
        move = true;
        isstart = false;
        istext = true;
    }
    public void gameend()
    {
        StartCoroutine(EndGame());
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

        isstart = true;//
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
        YutGameManager.manager.isPlayMiniGame = false;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("YutPlay"));

        if (TouchNum < 0)
            YutGameManager.manager.players[0].coinAmount += 10; //김대감 1번쨰 플레이어
        else
            YutGameManager.manager.players[1].coinAmount += 10; //정대감 2번째 플레이어

        YutGameManager.manager.isPlayMiniGame = false;
    }
}
