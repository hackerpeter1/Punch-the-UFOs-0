using UnityEditor;
using UnityEngine;
using System.Collections;

// Custom Editor using SerializedProperties.
// Automatic handling of multi-object editing, undo, and prefab overrides.
[CustomEditor(typeof(FirstSceneControl))]
[CanEditMultipleObjects]
public class MyPlayerEditor : Editor
{
    /*   
    public Vector3 size;  
    public Color color;  
    public float speed;  
    public Vector3 direction; 
    */
    SerializedProperty roundProp;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        roundProp = serializedObject.FindProperty("round");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        EditorGUILayout.IntSlider(roundProp, 1, 3, new GUIContent("Upper Limit of Difficulty"));

        // Only show the armor progress bar if all the objects have the same armor value:
        if (!roundProp.hasMultipleDifferentValues)
            ProgressBar(roundProp.intValue / 3f, "Upper Limit of Difficulty");

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }

    // Custom GUILayout progress bar.
    void ProgressBar(float value, string label)
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}