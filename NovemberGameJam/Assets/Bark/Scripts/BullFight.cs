using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullFight : MonoBehaviour
{
    [SerializeField] private GameObject Cow1, Cow2;
    private bool isGameStart = true;

    private int TouchNum;

    private void Update()
    {
        Cow1.transform.DOMove(new Vector2(-0.5f + (TouchNum / 4f), 0), 0.1f);
        Cow2.transform.DOMove(new Vector2(0.5f + (TouchNum / 4f), 0), 0.1f);
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
        }
        else if (TouchNum <= -42)
        {
            isGameStart = false;
        }
    }
}
