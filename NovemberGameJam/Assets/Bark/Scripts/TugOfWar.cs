using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TugOfWar : MonoBehaviour
{
    public static TugOfWar instance;

    int ropePoint;

    public GameObject rope;

    public GameObject playerOnePrefeb;
    public GameObject playerTwoPrefeb;

    public SpriteRenderer playerOneStunImage;
    public SpriteRenderer playerTwoStunImage;

    public Transform[] playerOneTransforms;
    public Transform[] playerTwoTransforms;

    public List<TOWButtonScript> playerOneButtons = new List<TOWButtonScript>();
    public List<TOWButtonScript> playerTwoButtons = new List<TOWButtonScript>();

    [Header("UI")]
    public Image fadePanel;
    public ExplainScript explain;
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI win;

    bool isExplainShowEnd;
    bool isPlayGame;
    bool isGameEnd;

    float playerOneStunTime;
    float playerTwoStunTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        countDown.gameObject.SetActive(false);
        fadePanel.gameObject.SetActive(true);
        playerOneStunImage.gameObject.SetActive(false);
        playerTwoStunImage.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
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
        for (int i = 0; i < 5; i++)
        {
            var one = Instantiate(playerOnePrefeb, playerOneTransforms[i].position, Quaternion.identity);
            playerOneButtons.Add(one.GetComponent<TOWButtonScript>());
            playerOneButtons[i].currentTransformIndex = i;

            var two = Instantiate(playerTwoPrefeb, playerTwoTransforms[i].position, Quaternion.identity);
            playerTwoButtons.Add(two.GetComponent<TOWButtonScript>());
            playerTwoButtons[i].currentTransformIndex = i;
        }
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
        win.text = ropePoint >= 0 ? "정 대감 우승!" : "김 대감 우승!";
        yield return new WaitForSeconds(1);
        for (float t = fadePanel.color.a; t > 0; t -= Time.deltaTime / 2)
            {
                var fadePanelColor = fadePanel.color;
                fadePanelColor.a = t;
                fadePanel.color = fadePanelColor;
                yield return new WaitForEndOfFrame();
            }
        YutGameManager.manager.isPlayMiniGame = false;
    }

    private void Update()
    {
        if (isPlayGame)
        {
            rope.transform.position = new Vector3((float)ropePoint / 3.33f, 0,0);

            if(Mathf.Abs(ropePoint) >= 15)
            {
                isGameEnd = true;
                isPlayGame = false;
                StartCoroutine(EndGame());
            }

            if (playerOneStunTime > 0)
            {
                playerOneStunTime -= Time.deltaTime;
                playerOneStunImage.gameObject.SetActive(true);
            }
            else
            {
                playerOneStunImage.gameObject.SetActive(false);
            }

            int index = -1;

            if (Input.GetKeyDown(KeyCode.W))
                index = 0;
            if (Input.GetKeyDown(KeyCode.S))
                index = 1;
            if (Input.GetKeyDown(KeyCode.A))
                index = 2;
            if (Input.GetKeyDown(KeyCode.D))
                index = 3;

            if (index != -1 && playerOneStunTime <= 0)
            {
                if (playerOneButtons[0].buttonType == index)
                {
                    var temp = playerOneButtons[0];
                    playerOneButtons.RemoveAt(0);
                    Destroy(temp.gameObject);

                    foreach (var btn in playerOneButtons) btn.currentTransformIndex--;
                    var one = Instantiate(playerOnePrefeb, playerOneTransforms[4].position, Quaternion.identity);
                    playerOneButtons.Add(one.GetComponent<TOWButtonScript>());
                    playerOneButtons[4].currentTransformIndex = 4;
                    ropePoint--;
                }
                else
                {
                    playerOneStunTime = 1;
                }
            }

            if (playerTwoStunTime > 0)
            {
                playerTwoStunTime -= Time.deltaTime;
                playerTwoStunImage.gameObject.SetActive(true);
            }
            else
            {
                playerTwoStunImage.gameObject.SetActive(false);
            }

            index = -1;

            if (Input.GetKeyDown(KeyCode.UpArrow))
                index = 0;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                index = 1;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                index = 2;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                index = 3;

            if (index != -1 && playerTwoStunTime <= 0)
            {
                if (playerTwoButtons[0].buttonType == index)
                {
                    var temp = playerTwoButtons[0];
                    playerTwoButtons.RemoveAt(0);
                    Destroy(temp.gameObject);

                    foreach (var btn in playerTwoButtons) btn.currentTransformIndex--;
                    var one = Instantiate(playerTwoPrefeb, playerTwoTransforms[4].position, Quaternion.identity);
                    playerTwoButtons.Add(one.GetComponent<TOWButtonScript>());
                    playerTwoButtons[4].currentTransformIndex = 4;
                    ropePoint++;
                }
                else
                {
                    playerTwoStunTime = 1;
                }
            }
        }
    }
}
