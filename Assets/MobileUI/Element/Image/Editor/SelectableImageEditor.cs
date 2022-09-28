#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;


namespace MobileUI
{
    [CustomEditor(typeof(SelectableImage)), CanEditMultipleObjects]
    public class SelectableImageEditor : ImageEditor
    {
        private SerializedProperty _selectedColorProp;
        private SerializedProperty _deselectedColorProp;
        private SerializedProperty _normalColorProp;
        private SerializedProperty _pressedColorProp;
        private SerializedProperty _disabledColorProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            _selectedColorProp = serializedObject.FindProperty("_selectedColor");
            _deselectedColorProp = serializedObject.FindProperty("_deselectedColor");
            _normalColorProp = serializedObject.FindProperty("_normalColor");
            _pressedColorProp = serializedObject.FindProperty("_pressedColor");
            _disabledColorProp = serializedObject.FindProperty("_disabledColor");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_selectedColorProp);
            EditorGUILayout.PropertyField(_deselectedColorProp);
            EditorGUILayout.PropertyField(_normalColorProp);
            EditorGUILayout.PropertyField(_pressedColorProp);
            EditorGUILayout.PropertyField(_disabledColorProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif