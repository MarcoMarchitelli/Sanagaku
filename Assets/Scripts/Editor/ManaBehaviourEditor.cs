using UnityEngine;
using UnityEditor;
using Sangaku;

[CustomEditor(typeof(ManaBehaviour))]
[CanEditMultipleObjects]
public class ManaBehaviourEditor : Editor
{
    SerializedProperty maxMana, startMana, startAtMax, regeneration, canExceedMax, amountPerSecond,
        OnManaChanged;

    bool showBehaviours = true, showParameters = true, showEvents = true;

    private void OnEnable()
    {
        maxMana = serializedObject.FindProperty("maxMana");
        startMana = serializedObject.FindProperty("startMana");
        startAtMax = serializedObject.FindProperty("startAtMax");
        regeneration = serializedObject.FindProperty("regeneration");
        canExceedMax = serializedObject.FindProperty("canExceedMax");
        amountPerSecond = serializedObject.FindProperty("amountPerSecond");
        OnManaChanged = serializedObject.FindProperty("OnManaChanged");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorStyles.foldout.fontStyle = FontStyle.Bold;

        EditorGUILayout.Space();

        showBehaviours = EditorGUILayout.Foldout(showBehaviours, "Behaviours", true, EditorStyles.foldout);
        if (showBehaviours)
        {
            EditorGUILayout.PropertyField(startAtMax);
            EditorGUILayout.PropertyField(canExceedMax);
            EditorGUILayout.PropertyField(regeneration);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
        if (showParameters)
        {
            EditorGUILayout.PropertyField(maxMana);
            if(!startAtMax.boolValue)
                EditorGUILayout.PropertyField(startMana);
            if(regeneration.boolValue)
                EditorGUILayout.PropertyField(amountPerSecond);
        }

        EditorGUILayout.Space();

        showEvents = EditorGUILayout.Foldout(showEvents, "Events", true, EditorStyles.foldout);
        if (showEvents)
        {
            EditorGUILayout.PropertyField(OnManaChanged);
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }

}
