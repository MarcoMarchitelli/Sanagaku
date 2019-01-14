using UnityEngine;
using UnityEditor;
using Deirin.AI;

[CustomEditor(typeof(Shooter))]
[CanEditMultipleObjects]
public class ShooterEditor : Editor
{
    SerializedProperty projectile, projectileSpawnPoint, hasTargets, projectilesIgnoreObstacles, searchForTargetsOnAwake, startsShootingOnAwake, fireRate,
        projectileSpeed, projectileLifeTime, turnRateAnglesPerSecond, obstacleLayer, OnProjectileShoot;

    bool showReferences, showBehaviours, showParameters, showEvents;

    private void OnEnable()
    {
        projectile = serializedObject.FindProperty("projectile");
        projectileSpawnPoint = serializedObject.FindProperty("projectileSpawnPoint");
        hasTargets = serializedObject.FindProperty("hasTargets");
        projectilesIgnoreObstacles = serializedObject.FindProperty("projectilesIgnoreObstacles");
        searchForTargetsOnAwake = serializedObject.FindProperty("searchForTargetsOnAwake");
        startsShootingOnAwake = serializedObject.FindProperty("startsShootingOnAwake");
        fireRate = serializedObject.FindProperty("fireRate");
        projectileSpeed = serializedObject.FindProperty("projectileSpeed");
        projectileLifeTime = serializedObject.FindProperty("projectileLifeTime");
        turnRateAnglesPerSecond = serializedObject.FindProperty("turnRateAnglesPerSecond");
        obstacleLayer = serializedObject.FindProperty("obstacleLayer");
        OnProjectileShoot = serializedObject.FindProperty("OnProjectileShoot");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();

        showReferences = EditorGUILayout.Foldout(showReferences, "References", true, EditorStyles.foldout);
        if (showReferences)
        {
            EditorGUILayout.PropertyField(projectile);
            EditorGUILayout.PropertyField(projectileSpawnPoint);
        }

        EditorGUILayout.Space();

        showBehaviours = EditorGUILayout.Foldout(showBehaviours, "showBehaviours", true, EditorStyles.foldout);
        if (showBehaviours)
        {
            EditorGUILayout.PropertyField(hasTargets);
            EditorGUILayout.PropertyField(projectilesIgnoreObstacles);
            EditorGUILayout.PropertyField(searchForTargetsOnAwake);
            EditorGUILayout.PropertyField(startsShootingOnAwake);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "showParameters", true, EditorStyles.foldout);
        if (showParameters)
        {
            EditorGUILayout.PropertyField(fireRate);
            EditorGUILayout.PropertyField(projectileSpeed);
            EditorGUILayout.PropertyField(projectileLifeTime);
            if(hasTargets.boolValue)
                EditorGUILayout.PropertyField(turnRateAnglesPerSecond);
            if(!projectilesIgnoreObstacles.boolValue)
                EditorGUILayout.PropertyField(obstacleLayer);
        }

        EditorGUILayout.Space();

        showEvents = EditorGUILayout.Foldout(showEvents, "showEvents", true, EditorStyles.foldout);
        if (showEvents)
        {
            EditorGUILayout.PropertyField(OnProjectileShoot);
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }

}
