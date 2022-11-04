using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class outline : MonoBehaviour
{
    [SerializeField]
    Text lefttext;
    [SerializeField]
    Text righttext;
    [SerializeField]
    Image leftimage;
    [SerializeField]
    Image rightimage;
    [SerializeField]
    GameObject[] jegi;
    public bool isPlayGame = false;

    [Header("UI")]
    public Image fadePanel;
    public ExplainScript explain;
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI win;
    bool isExplainShowEnd;
    int TouchNum;
    void Start()
    {
        StartCoroutine(fadeIn());
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "leftjegi")
        {
            jegi[0].GetComponent<Collider2D>().enabled = false;
            jegi[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            jegi[1].GetComponent<Collider2D>().enabled = false;
            jegi[1].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            lefttext.text = "LOSE";
            lefttext.color = Color.red;
            righttext.text = "WIN";
            righttext.color = Color.green;
            leftimage.color = new Color(0, 0, 0, 0.5f);
            TouchNum = 1;
            StartCoroutine(EndGame());
        }
        if (collision.name == "rightjegi")
        {
            jegi[0].GetComponent<Collider2D>().enabled = false;
            jegi[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            jegi[1].GetComponent<Collider2D>().enabled = false;
            jegi[1].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            lefttext.text = "WIN";
            lefttext.color = Color.green;
            righttext.text = "LOSE";
            righttext.color = Color.red;
            rightimage.color = new Color(0, 0, 0, 0.5f);
            TouchNum = -1;
            StartCoroutine(EndGame());
        }
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
        countDown.text = "½ÃÀÛ!";
        yield return new WaitForSeconds(1);

        isPlayGame = true;
        countDown.gameObject.SetActive(false);
    }
    IEnumerator EndGame()
    {
        win.gameObject.SetActive(true);
        win.text = TouchNum >= 0 ? "±è ´ë°¨ ¿ì½Â!" : "Á¤ ´ë°¨ ¿ì½Â!";
        yield return new WaitForSeconds(1);
        for (float t = fadePanel.color.a; t < 1; t += Time.deltaTime / 2)
        {
            var fadePanelColor = fadePanel.color;
            fadePanelColor.a = t;
            fadePanel.color = fadePanelColor;
            yield return new WaitForEndOfFrame();
        }
        YutGameManager.manager.isPlayMiniGame = false;
    }
}
