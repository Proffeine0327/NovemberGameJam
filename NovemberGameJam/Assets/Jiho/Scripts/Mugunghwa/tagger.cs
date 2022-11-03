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
    string dialogue = "¹«±ÃÈ­ ²ÉÀÌ ÇÇ¾ú½À´Ï´Ù!";
    bool isturn = false;
    bool iscou = false;
    bool istext = false;
    void Start()
    {
        StartCoroutine(adf());
    }

    void Update()
    {
        if (istext == true)
        {
            istext = false;
            StartCoroutine(printtext());
        }
        if (isturn == true)
        {
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("¿ÞÂÊ ¾Æ¿ô");
            }
            if (Input.GetKey(KeyCode.L))
            {
                Debug.Log("¿À¸¥ÂÊ ¾Æ¿ô");
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
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        istext = true;
    }

    IEnumerator adf()
    {
        yield return new WaitForSeconds(3f);
        istext = true;
    }
}
