using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects,ExecuteInEditMode ,RequireComponent(typeof(MeshRenderer))]
public class SortingLayerInTextMesh : MonoBehaviour
{
    [SerializeField] int sortingLayer;

    private void Update() 
    {
        GetComponent<MeshRenderer>().sortingOrder = sortingLayer;
    }
}

[ExecuteInEditMode, CustomEditor(typeof(SortingLayerInTextMesh))]
public class SortingLayerInTextMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Order in Layer");
        serializedObject.FindProperty("sortingLayer").intValue = EditorGUILayout.IntField(serializedObject.FindProperty("sortingLayer").intValue);
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}

