using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;

namespace MobileUI
{
    [CustomEditor(typeof(InteractableImage)), CanEditMultipleObjects]
    public class InteractableImageEditor : ImageEditor
    {
        private SerializedProperty _baseColorProp;
        private SerializedProperty _normalColorProp;
        private SerializedProperty _pressedColorProp;
        private SerializedProperty _disabledColorProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            _baseColorProp = serializedObject.FindProperty("_baseColor");
            _normalColorProp = serializedObject.FindProperty("_normalColor");
            _pressedColorProp = serializedObject.FindProperty("_pressedColor");
            _disabledColorProp = serializedObject.FindProperty("_disabledColor");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_baseColorProp);
            EditorGUILayout.PropertyField(_normalColorProp);
            EditorGUILayout.PropertyField(_pressedColorProp);
            EditorGUILayout.PropertyField(_disabledColorProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}