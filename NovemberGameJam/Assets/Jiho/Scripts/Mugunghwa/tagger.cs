using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    GameObject image;
    [SerializeField]
    Text timer;
    float time = 4f;
    string dialogue = "무궁화 꽃이 피었습니다!";
    bool isturn = false;
    bool iscou = false;
    bool istext = false;
    bool isstart = false;
    public bool isnotend = true;
    public bool move = false;
    void Start()
    {
        StartCoroutine(mugunghwastart());
    }

    void Update()
    {
        if(isstart)
        {
            time -= Time.deltaTime;
            timer.text = ((int)time).ToString();
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
        isstart = true;
        yield return new WaitForSeconds(3f);
        move = true;
        isstart = false;
        image.SetActive(false);
        istext = true;
    }
    public void gameend()
    {
        Debug.Log("무궁화 끝!");
        //게임 종료시 호출되는 함수
    }
}
