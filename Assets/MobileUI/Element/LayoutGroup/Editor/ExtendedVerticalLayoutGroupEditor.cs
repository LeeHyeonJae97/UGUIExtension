using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ExtendedVerticalLayoutGroup))]
public class ExtendedVerticalLayoutGroupEditor : HorizontalOrVerticalLayoutGroupEditor
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
