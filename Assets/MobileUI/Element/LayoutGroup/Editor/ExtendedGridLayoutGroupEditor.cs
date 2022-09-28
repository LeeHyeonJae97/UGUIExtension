using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(ExtendedGridLayoutGroup))]
public class ExtendedGridLayoutGroupEditor : GridLayoutGroupEditor
{
    private SerializedProperty _runtimeMode;

    protected override void OnEnable()
    {
        base.OnEnable();

        _runtimeMode = serializedObject.FindProperty("_runtimeMode");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_runtimeMode);

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}
