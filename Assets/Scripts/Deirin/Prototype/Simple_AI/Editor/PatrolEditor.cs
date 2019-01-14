using UnityEditor;
using Deirin.AI;

[CustomEditor(typeof(Patrol))]
[CanEditMultipleObjects]
public class PatrolEditor : Editor
{
    SerializedProperty path, speed, waitTime, rotationAnglePerSecond, rotatesToWaypoint, OnMovementStart, OnMovementEnd, OnWaypointReached, OnPathFinished, startOnAwake;

    bool showReferences, showBehaviours, showParameters, showEvents;

    private void OnEnable()
    {
        path = serializedObject.FindProperty("path");
        speed = serializedObject.FindProperty("speed");
        waitTime = serializedObject.FindProperty("waitTime");
        rotationAnglePerSecond = serializedObject.FindProperty("rotationAnglePerSecond");
        rotatesToWaypoint = serializedObject.FindProperty("rotatesToWaypoint");
        OnMovementStart = serializedObject.FindProperty("OnMovementStart");
        OnMovementEnd = serializedObject.FindProperty("OnMovementEnd");
        OnWaypointReached = serializedObject.FindProperty("OnWaypointReached");
        OnPathFinished = serializedObject.FindProperty("OnPathFinished");
        startOnAwake = serializedObject.FindProperty("startOnAwake");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();

        showReferences = EditorGUILayout.Foldout(showReferences, "References", true, EditorStyles.foldout);
        if (showReferences)
        {
            EditorGUILayout.PropertyField(path);    
        }

        EditorGUILayout.Space();

        showBehaviours = EditorGUILayout.Foldout(showBehaviours, "Behaviours", true, EditorStyles.foldout);
        if (showBehaviours)
        {
            EditorGUILayout.PropertyField(rotatesToWaypoint);
            EditorGUILayout.PropertyField(startOnAwake);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
        if (showParameters)
        {
            EditorGUILayout.PropertyField(speed);

            if (rotatesToWaypoint.boolValue)
                EditorGUILayout.PropertyField(rotationAnglePerSecond);
            else
                EditorGUILayout.PropertyField(waitTime);
        }

        EditorGUILayout.Space();

        showEvents = EditorGUILayout.Foldout(showEvents, "Events", true, EditorStyles.foldout);
        if (showEvents)
        {
            EditorGUILayout.PropertyField(OnMovementStart);
            EditorGUILayout.PropertyField(OnMovementEnd);
            EditorGUILayout.PropertyField(OnWaypointReached);
            EditorGUILayout.PropertyField(OnPathFinished);
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }

}
