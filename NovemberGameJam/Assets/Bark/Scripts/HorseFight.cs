using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HorseFight : MonoBehaviour
{
    [SerializeField] private GameObject Player1, Player2;

    private float curTime , player1Time, player2Time;

    private void Start()
    {
        PlayerMove();
    }
    private void Update()
    {
         curTime += Time.deltaTime;
        InputTouch();
        if(curTime >= 12.6f)
        {
            //���
            if(player1Time < player2Time)
            {
                //��1 ��!
            }
            else
            {
                //��2 ��!
            }
            print($"{player1Time}, {player2Time}");
        }
    }
    void PlayerMove()
    {
        Player1.transform.DOMoveX(-11, 12.6f);
        Player2.transform.DOMoveX(11, 12.6f);
    }

    void InputTouch()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            player1Time = 6.3f - curTime;
            Mathf.Abs(player1Time);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            player2Time = 6.3f - curTime;
            Mathf.Abs(player2Time);
        }
    }
}
