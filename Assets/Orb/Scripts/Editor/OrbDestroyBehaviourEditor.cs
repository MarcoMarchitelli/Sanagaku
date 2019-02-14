using UnityEngine;
using UnityEditor;
using Sangaku;

[CustomEditor(typeof(OrbDestroyBehaviour))]
[CanEditMultipleObjects]
public class OrbDestroyBehaviourEditor : Editor
{
    SerializedProperty usesObjectPooler, poolTag, isIPoolable, OnDestruction;

    bool showBehaviours = true, showParameters = true, showEvents = true;

    private void OnEnable()
    {
        usesObjectPooler = serializedObject.FindProperty("usesObjectPooler");
        poolTag = serializedObject.FindProperty("poolTag");
        isIPoolable = serializedObject.FindProperty("isIPoolable");
        OnDestruction = serializedObject.FindProperty("OnDestruction");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorStyles.foldout.fontStyle = FontStyle.Bold;

        EditorGUILayout.Space();

        showBehaviours = EditorGUILayout.Foldout(showBehaviours, "Behaviours", true, EditorStyles.foldout);
        if (showBehaviours)
        {
            EditorGUILayout.PropertyField(usesObjectPooler);
        }

        EditorGUILayout.Space();

        if (usesObjectPooler.boolValue)
        {
            showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
            if (showParameters)
            {
                EditorGUILayout.PropertyField(poolTag);
                EditorGUILayout.PropertyField(isIPoolable);
            }
        }

        EditorGUILayout.Space();

        showEvents = EditorGUILayout.Foldout(showEvents, "Events", true, EditorStyles.foldout);
        if (showEvents)
        {
            EditorGUILayout.PropertyField(OnDestruction);
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }
}