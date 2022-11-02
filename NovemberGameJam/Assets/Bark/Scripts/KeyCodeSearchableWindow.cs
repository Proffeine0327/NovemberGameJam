using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class KeyCodeSearchableWindow : EditorWindow
{
    public static void Open(Action<KeyCode> action)
    {
        var window = EditorWindow.CreateInstance<KeyCodeSearchableWindow>();
        var mousePos = EditorGUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        window.action = action;
        window.elements = Enum.GetNames(typeof(KeyCode));
        window.ShowAsDropDown(new Rect(mousePos, Vector2.zero), new Vector2(200, 600));
    }

    string search;

    Action<KeyCode> action;
    string[] elements;

    private void OnGUI()
    {
        search = EditorGUILayout.TextField(search, EditorStyles.toolbarSearchField);

        var showElement = elements;
        if(!string.IsNullOrEmpty(search))
        {
            var searchElement =
                from ele in showElement
                where ele.Contains(search)
                select ele;

            showElement = searchElement.ToArray();
        }

        for(int i = 0; i < showElement.Length; i++)
        {
            if (GUILayout.Button(showElement[i]))
            {
                action.Invoke((KeyCode)Enum.Parse(typeof(KeyCode), showElement[i]));
                this.Close();
            }
        }
    }
}
