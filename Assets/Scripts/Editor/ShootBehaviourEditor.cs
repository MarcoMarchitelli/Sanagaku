using UnityEngine;
using UnityEditor;
using Sangaku;

[CustomEditor(typeof(ShootBehaviour))]
[CanEditMultipleObjects]
public class ShootBehaviourEditor : Editor
{
    SerializedProperty projectilePrefab, shootPoint, hasTargets, searchForTargetsOnAwake, startsShootingOnAwake, secondsBetweenShots,
        turnRateAnglesPerSecond, OnShoot, usesObjectPooler, projectilePoolTag;

    bool showReferences = true, showBehaviours = true, showParameters = true, showEvents = true;

    private void OnEnable()
    {
        projectilePrefab = serializedObject.FindProperty("projectilePrefab");
        shootPoint = serializedObject.FindProperty("shootPoint");
        hasTargets = serializedObject.FindProperty("hasTargets");
        searchForTargetsOnAwake = serializedObject.FindProperty("searchForTargetsOnAwake");
        startsShootingOnAwake = serializedObject.FindProperty("startsShootingOnAwake");
        secondsBetweenShots = serializedObject.FindProperty("secondsBetweenShots");
        turnRateAnglesPerSecond = serializedObject.FindProperty("turnRateAnglesPerSecond");
        OnShoot = serializedObject.FindProperty("OnShoot");
        usesObjectPooler = serializedObject.FindProperty("usesObjectPooler");
        projectilePoolTag = serializedObject.FindProperty("projectilePoolTag");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorStyles.foldout.fontStyle = FontStyle.Bold;

        EditorGUILayout.Space();
        
        showReferences = EditorGUILayout.Foldout(showReferences, "References", true, EditorStyles.foldout);
        if (showReferences)
        {
            if (!usesObjectPooler.boolValue)
                EditorGUILayout.PropertyField(projectilePrefab);
            EditorGUILayout.PropertyField(shootPoint);
        }

        EditorGUILayout.Space();

        showBehaviours = EditorGUILayout.Foldout(showBehaviours, "Behaviours", true, EditorStyles.foldout);
        if (showBehaviours)
        {
            EditorGUILayout.PropertyField(usesObjectPooler);
            EditorGUILayout.PropertyField(hasTargets);
            EditorGUILayout.PropertyField(searchForTargetsOnAwake);
            EditorGUILayout.PropertyField(startsShootingOnAwake);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
        if (showParameters)
        {
            if (usesObjectPooler.boolValue)
                EditorGUILayout.PropertyField(projectilePoolTag);
            EditorGUILayout.PropertyField(secondsBetweenShots);
            if (hasTargets.boolValue)
                EditorGUILayout.PropertyField(turnRateAnglesPerSecond);
        }

        EditorGUILayout.Space();

        showEvents = EditorGUILayout.Foldout(showEvents, "Events", true, EditorStyles.foldout);
        if (showEvents)
        {
            EditorGUILayout.PropertyField(OnShoot);
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }

}