using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
public class TugOfWar : MonoBehaviour
{
    [SerializeField] GameObject Line, TouchParticle, Tutorial;
    [SerializeField] Transform LeftPos, RightPos;
    [SerializeField] Text text;
    private int TouchNum;
    private bool isGameStart = false;

    private void Start()
    {
        StartCoroutine(StartTutorial());
    }
    private void Update()
    {
        Line.transform.position = new Vector2(0 + (TouchNum / 2f), 0);
        GameOver();
        InputTouch();
    }

    void InputTouch()
    {
        if (isGameStart)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
            {
                TouchProduction(LeftPos);
                TouchNum--;
            }
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.K))
            {
                TouchProduction(RightPos);
                TouchNum++;
            }
        }
    }
    void TouchProduction(Transform trans)
    {
        GameObject gameObject = Instantiate(TouchParticle, trans);
        Destroy(gameObject, 0.3f);
    }
    void GameOver()
    {
        if (TouchNum >= 15)
        {
            isGameStart = false;
        }
        else if (TouchNum <= -15)
        {
            isGameStart = false;
        }
    }
    IEnumerator StartTutorial()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            text.text = $"{3 - i}";
        }
        yield return new WaitForSeconds(1);
        Tutorial.SetActive(false);
        text.gameObject.SetActive(false);
        isGameStart = true;
    }
}
