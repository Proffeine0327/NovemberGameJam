using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    void Start()
    {

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
            gameend();
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
            gameend();
        }
    }

    public void gameend()
    {
        Debug.Log("제기 끝!");
        //게임 종료시 호출되는 함수
    }
}
