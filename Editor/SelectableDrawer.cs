using UnityEditor;
using dtfernandes.UINavigation;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Selectable), editorForChildClasses: true)]
public class SelectableDrawer : Editor
{
    SerializedProperty _highlightMode;
    SerializedProperty _highlightSprite, _defaultSprite;
    SerializedProperty _highlightColor, _defaultColor;

    private List<Component> _highlighers;

    void OnEnable()
    {
        _highlightMode = serializedObject.FindProperty("_highlightMode");

        _highlightSprite = serializedObject.FindProperty("_highlightSprite");
        _defaultSprite = serializedObject.FindProperty("_defaultSprite");

        _highlightColor = serializedObject.FindProperty("_highlightColor");
        _defaultColor = serializedObject.FindProperty("_defaultColor");

        FillHighlighters();
    }

    private void FillHighlighters()
    {

        _highlighers = new List<Component> { };
        IHighlighter[] hs = (target as Selectable).GetComponents<IHighlighter>();

        foreach (IHighlighter h in hs)
        {
            if (h is Component)
            {
                _highlighers.Add(h as Component);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        int prevMode = _highlightMode.enumValueIndex;

        // Top line
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Title
        GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 13,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        EditorGUILayout.LabelField("Highlights", headerStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_highlightMode);

        switch (_highlightMode.enumValueIndex)
        {
            // Color
            case 0:
                EditorGUILayout.PropertyField(_defaultColor);
                EditorGUILayout.PropertyField(_highlightColor);
                break;
            // Image
            case 1:
                EditorGUILayout.PropertyField(_defaultSprite);
                EditorGUILayout.PropertyField(_highlightSprite);
                break;
            // Script
            case 2:

                if (prevMode != 2)
                {
                    FillHighlighters();
                }

                if (_highlighers == null)
                {
                    EditorGUILayout.HelpBox("it seems there's been a problem", MessageType.Info);
                    break;
                }

                EditorGUI.BeginDisabledGroup(true);
                for (int i = 0; i < _highlighers.Count; i++)
                {
                    // Makes the field read-only
                    EditorGUILayout.ObjectField(
                        $"{_highlighers[i].GetType().Name}",
                        _highlighers[i],
                        typeof(Component),
                        allowSceneObjects: true);

                }
                EditorGUI.EndDisabledGroup();
                break;

        }
        // Bottom line
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        serializedObject.ApplyModifiedProperties();
    }

}