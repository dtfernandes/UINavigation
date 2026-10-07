using UnityEditor;
using dtfernandes.UINavigation;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

[CustomEditor(typeof(Selectable), editorForChildClasses: true)]
public class SelectableEditor : Editor
{
    SerializedProperty _highlightMode;
    SerializedProperty _highlightSprite, _defaultSprite;
    SerializedProperty _highlightColor, _defaultColor;
    SerializedProperty _active, _interactable;

    private List<Component> _highlighers;

    List<SerializedProperty> _defaultProps;

    void OnEnable()
    {
        _highlightMode = serializedObject.FindProperty("_highlightMode");

        _highlightSprite = serializedObject.FindProperty("_highlightSprite");
        _defaultSprite = serializedObject.FindProperty("_defaultSprite");

        _highlightColor = serializedObject.FindProperty("_highlightColor");
        _defaultColor = serializedObject.FindProperty("_defaultColor");

        _active = serializedObject.FindProperty("_active");
        _interactable = serializedObject.FindProperty("_interactable");

        FillHighlighters();

        _defaultProps = new List<SerializedProperty> { };
        SerializedProperty prop = serializedObject.GetIterator();
        prop.NextVisible(true);
        while (true)
        {
            _defaultProps.Add(prop.Copy());

            bool doReturn = prop.NextVisible(false);

            if (!doReturn) break;
        }
        // foreach(SerializedProperty sp in serializedObject)
        // {
        //     Debug.Log("Test: " + sp.Name);
        // }
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

        EditorGUILayout.PropertyField(_defaultProps[0]);

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
        EditorGUILayout.Space();

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

                if (_highlighers.Count == 0)
                {
                    EditorGUILayout.HelpBox("This object has no IHighlighter components", MessageType.Info);
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

        EditorGUILayout.LabelField("State", headerStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_active);
        EditorGUILayout.PropertyField(_interactable);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);


        EditorGUILayout.LabelField("Child Specific", headerStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        for (int i = 1; i < _defaultProps.Count; i++)
        {
            SerializedProperty prop = _defaultProps[i];
            EditorGUILayout.PropertyField(prop);
        }

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }

}