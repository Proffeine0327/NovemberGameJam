using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TugOfWar : MonoBehaviour
{
    [SerializeField] GameObject Line, TouchParticle, Tutorial, GameOverParticle;
    [SerializeField] Transform LeftPos, RightPos;
    [SerializeField] List<KeyCode> KeyboardList = new List<KeyCode>();
    [SerializeField] List<KeyCode> KeyboardList2 = new List<KeyCode>();
    [SerializeField] List<KeyCode> RandomKeyboardList = new List<KeyCode>();
    [SerializeField] List<KeyCode> RandomKeyboardList2 = new List<KeyCode>();
    [SerializeField] Text StartSecond;
    [SerializeField] Text[] Player1Text, Player2Text;
    private int TouchNum;
    private bool isGameStart = false, isGameOver1 = false, isGameOver2 = false;

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
                        TouchProduction(LeftPos);
                        TouchNum--;
                        RandomKeyboardList.Remove(RandomKeyboardList[0]);
                        RandomKeyboardList.Add(KeyboardList[Random.Range(0, 4)]);
                    }
                    else
                    {
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
                        TouchProduction(RightPos);
                        TouchNum++;
                        RandomKeyboardList2.Remove(RandomKeyboardList2[0]);
                        RandomKeyboardList2.Add(KeyboardList2[Random.Range(0, 4)]);
                    }
                    else
                    {
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
        }
        else if (TouchNum <= -15)
        {
            isGameStart = false;
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
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            StartSecond.text = $"{3 - i}";
        }
        yield return new WaitForSeconds(1);
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
            Destroy (gameObject);
            isGameOver2 = false;
        }
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
