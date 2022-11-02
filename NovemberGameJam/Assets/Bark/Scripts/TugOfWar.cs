using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Reflection;

public class TugOfWar : MonoBehaviour
{
    [SerializeField] GameObject Line, TouchParticle, Tutorial, GameOverParticle, ResultWindow;
    [SerializeField] SpriteRenderer[] Player1Arrows, Player2Arrows;
    [SerializeField] Transform LeftPos, RightPos;
    [SerializeField] List<KeyCode> KeyboardList = new List<KeyCode>();
    [SerializeField] List<KeyCode> KeyboardList2 = new List<KeyCode>();
    [SerializeField] List<KeyCode> RandomKeyboardList = new List<KeyCode>();
    [SerializeField] List<KeyCode> RandomKeyboardList2 = new List<KeyCode>();
    [SerializeField] Text StartSecond, Result1, Result2;
    [SerializeField] Text[] Player1Text, Player2Text;
    private int TouchNum;
    private bool isGameStart = false, isGameOver1 = false, isGameOver2 = false;

    private Coroutine IngCoroutine;
    private void Start()
    {
        StartListDraw();
        StartCoroutine(StartTutorial());
    }
    private void Update()
    {
        PressKeyPrint();
        Line.transform.position = new Vector2(0.45f + (TouchNum / 3.8f), -2.59f);
        GameEnd();
        InputTouch();
    }

    void InputTouch()
    {
        if (isGameStart)
        {
            for (int i = 0; i < KeyboardList.Count; i++)
            {
                if (Input.GetKeyDown(KeyboardList[i]) && !isGameOver1)
                {
                    if (KeyboardList[i] == RandomKeyboardList[0])
                    {
                        if (IngCoroutine != null)
                        {
                            StopCoroutine(IngCoroutine);
                        }
                        IngCoroutine = StartCoroutine(Grading(true, Player1Arrows));
                        TouchProduction(LeftPos);
                        TouchNum--;
                        RandomKeyboardList.Remove(RandomKeyboardList[0]);
                        RandomKeyboardList.Add(KeyboardList[Random.Range(0, 4)]);
                    }
                    else
                    {
                        if (IngCoroutine != null)
                        {
                            StopCoroutine(IngCoroutine);
                        }
                        IngCoroutine = StartCoroutine(Grading(false, Player1Arrows));
                        StartCoroutine(GameOverDelay(true));
                        //기절
                    }
                }
            }
            for (int i = 0; i < KeyboardList2.Count; i++)
            {
                if (Input.GetKeyDown(KeyboardList2[i]) && !isGameOver2)
                {
                    if (KeyboardList2[i] == RandomKeyboardList2[0])
                    {
                        if (IngCoroutine != null)
                        {
                            StopCoroutine(IngCoroutine);
                        }
                        IngCoroutine = StartCoroutine(Grading(true, Player2Arrows));
                        TouchProduction(RightPos);
                        TouchNum++;
                        RandomKeyboardList2.Remove(RandomKeyboardList2[0]);
                        RandomKeyboardList2.Add(KeyboardList2[Random.Range(0, 4)]);
                    }
                    else
                    {
                        if (IngCoroutine != null)
                        {
                            StopCoroutine(IngCoroutine);
                        }
                        IngCoroutine = StartCoroutine(Grading(false, Player2Arrows));
                        StartCoroutine(GameOverDelay(false));
                        //기절
                    }
                }
            }
        }
    }

    void StartListDraw()
    {
        for (int i = 0; i < RandomKeyboardList.Count; i++)
        {
            RandomKeyboardList[i] = KeyboardList[Random.Range(0, 4)];
            RandomKeyboardList2[i] = KeyboardList2[Random.Range(0, 4)];
        }
    }
    void TouchProduction(Transform trans)
    {
        GameObject gameObject = Instantiate(TouchParticle, trans);
        Destroy(gameObject, 0.6f);
    }
    void GameEnd()
    {
        if (TouchNum >= 15)
        {
            isGameStart = false;
            ResultWindow.SetActive(true);
            ResultWindow.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.7f);
            Result1.text = "Player2";
            Result2.text = "Player1";
        }
        else if (TouchNum <= -15)
        {
            isGameStart = false;
            ResultWindow.SetActive(true);
            ResultWindow.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.7f);
            Result1.text = "Player1";
            Result2.text = "Player2";
        }
    }
    void PressKeyPrint()
    {
        for (int i = 0; i < Player1Text.Length; i++)
        {
            Player1Text[i].text = RandomKeyboardList[i].ToString();
            if (RandomKeyboardList2[i].ToString() == "LeftArrow")
            {
                Player2Text[i].text = "←";
            }
            else if (RandomKeyboardList2[i].ToString() == "UpArrow")
            {
                Player2Text[i].text = "↑";
            }
            else if (RandomKeyboardList2[i].ToString() == "DownArrow")
            {
                Player2Text[i].text = "↓";
            }
            else if (RandomKeyboardList2[i].ToString() == "RightArrow")
            {
                Player2Text[i].text = "→";
            }
        }
    }
    IEnumerator StartTutorial()
    {
        for (int i = 0; i < 5; i++)
        {
            StartSecond.text = $"{5 - i}";
            yield return new WaitForSeconds(1);
        }
        Tutorial.SetActive(false);
        StartSecond.gameObject.SetActive(false);
        isGameStart = true;
    }
    IEnumerator GameOverDelay(bool thisOver)
    {
        if (thisOver)
        {
            isGameOver1 = true;
            GameObject gameObject = Instantiate(GameOverParticle, LeftPos);
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
            isGameOver1 = false;
        }
        else
        {
            isGameOver2 = true;
            GameObject gameObject = Instantiate(GameOverParticle, RightPos);
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
            isGameOver2 = false;
        }
    }
    IEnumerator Grading(bool grad, SpriteRenderer[] spriteRenderers)
    {
        if (grad)
        {
            for (int i = 0; i < Player1Arrows.Length; i++)
            {
                spriteRenderers[i].color = new Color(0, 1, 0, 1);
            }
            yield return new WaitForSeconds(0.6f);
            for (int i = 0; i < Player1Arrows.Length; i++)
            {
                spriteRenderers[i].color = new Color(1, 1, 0, 1);
            }
        }
        else
        {
            for (int i = 0; i < Player1Arrows.Length; i++)
            {
                spriteRenderers[i].color = new Color(1, 0, 0, 1);
            }
            yield return new WaitForSeconds(2);
            for (int i = 0; i < Player1Arrows.Length; i++)
            {
                spriteRenderers[i].color = new Color(1, 1, 0, 1);
            }
            IngCoroutine = null;
        }
    }
    public void GoHome()
    {
        //SceneManager.LoadScene("Title");
    }
    public void ReTry()
    {
        SceneManager.LoadScene("TugOfWar");
    }
}

[CustomEditor(typeof(TugOfWar))]
public class TugOfWarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var _keycodes = serializedObject.FindProperty("KeyboardList");
        var _keycodes2 = serializedObject.FindProperty("KeyboardList2");

        EditorGUILayout.BeginVertical(GUI.skin.box);
        for (int i = 0; i < _keycodes.arraySize; i++)
        {
            int index = i;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel($"KeyCode{index}");
            if (GUILayout.Button("open"))
            {
                KeyCodeSearchableWindow.Open((x) =>
                {
                    _keycodes.GetArrayElementAtIndex(index).enumValueFlag = (int)x;
                    serializedObject.ApplyModifiedProperties();
                });
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        for (int i = 0; i < _keycodes2.arraySize; i++)
        {
            int index = i;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel($"KeyCode{index}");
            if (GUILayout.Button("open"))
            {
                KeyCodeSearchableWindow.Open((x) =>
                {
                    _keycodes2.GetArrayElementAtIndex(index).enumValueFlag = (int)x;
                    serializedObject.ApplyModifiedProperties();
                });
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        base.OnInspectorGUI();
    }
}
