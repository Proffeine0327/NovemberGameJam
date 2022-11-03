using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BullFight : MonoBehaviour
{
    [SerializeField] private GameObject Cow1, Cow2, ResultWindow;
    [SerializeField] private Text Result1, Result2;
    private bool isGameStart = true;

    private int TouchNum;

    private void Update()
    {
        Cow1.transform.DOMove(new Vector2(-4f + (TouchNum / 4f), 0), 0.1f);
        Cow2.transform.DOMove(new Vector2(4f + (TouchNum / 4f), 0), 0.1f);
        GameEnd();
        InputTouch();
    }
    void InputTouch()
    {
        if (isGameStart)
        {
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
            {
                TouchNum++;
            }
            if(Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
            {
                TouchNum--;
            }
        }
    }
    void GameEnd()
    {
        if (TouchNum >= 42)
        {
            isGameStart = false;
            ResultWindow.SetActive(true);
            ResultWindow.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.7f);
            Result1.text = "Player1";
            Result2.text = "Player2";
        }
        else if (TouchNum <= -42)
        {
            isGameStart = false;
            ResultWindow.SetActive(true);
            ResultWindow.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.7f);
            Result1.text = "Player2";
            Result2.text = "Player1";
        }
    }
}
