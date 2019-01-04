using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestEnemy))]
public class TestEnemyEditor : Editor {

    SerializedProperty deathParticles, deathBehaviour, BouncesNeededToDie, health, OnDeath;

    bool showReferences, showBehaviours, showParameters, showEvents;

    private void OnEnable()
    {
        deathParticles = serializedObject.FindProperty("deathParticles");
        deathBehaviour = serializedObject.FindProperty("deathBehaviour");
        BouncesNeededToDie = serializedObject.FindProperty("BouncesNeededToDie");
        health = serializedObject.FindProperty("health");
        OnDeath = serializedObject.FindProperty("OnDeath");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();

        showReferences = EditorGUILayout.Foldout(showReferences, "References", true, EditorStyles.foldout);
        if (showReferences)
        {
            EditorGUILayout.PropertyField(deathParticles);
        }

        EditorGUILayout.Space();

        showBehaviours = EditorGUILayout.Foldout(showBehaviours, "Behaviours", true, EditorStyles.foldout);
        if (showBehaviours)
        {
            EditorGUILayout.PropertyField(deathBehaviour);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
        if (showParameters)
        {
            if(deathBehaviour.enumValueIndex == (int)TestEnemy.DeathBeahviour.diesFromBounces)
                EditorGUILayout.PropertyField(BouncesNeededToDie);
            else if (deathBehaviour.enumValueIndex == (int)TestEnemy.DeathBeahviour.diesFromDamage)
                EditorGUILayout.PropertyField(health);
        }

        EditorGUILayout.Space();

        showEvents = EditorGUILayout.Foldout(showEvents, "Events", true, EditorStyles.foldout);
        if (showEvents)
        {
            EditorGUILayout.PropertyField(OnDeath);
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }

}
