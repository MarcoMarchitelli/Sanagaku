using UnityEditor;
using Deirin.AI;

[CustomEditor(typeof(Scanner))]
[CanEditMultipleObjects]
public class ScannerEditor : Editor
{
    SerializedProperty timeToScan, fovAngle, scanAreaLenght, obstacleLayer, OnTargetSpotted, 
        OnTargetLost, scanType, spotLightColor, detectedSpotlightColor, scanAreaRadius, canSeeThroughObstacles;

    bool showParameters = true, showEvents = true, showBehaviours = true;

    private void OnEnable()
    {
        timeToScan = serializedObject.FindProperty("timeToScan");
        fovAngle = serializedObject.FindProperty("fovAngle");
        scanAreaLenght = serializedObject.FindProperty("scanAreaLenght");
        obstacleLayer = serializedObject.FindProperty("obstacleLayer");
        OnTargetSpotted = serializedObject.FindProperty("OnTargetSpotted");
        OnTargetLost = serializedObject.FindProperty("OnTargetLost");
        scanType = serializedObject.FindProperty("scanType");
        spotLightColor = serializedObject.FindProperty("spotLightColor");
        detectedSpotlightColor = serializedObject.FindProperty("detectedSpotlightColor");
        scanAreaRadius = serializedObject.FindProperty("scanAreaRadius");
        canSeeThroughObstacles = serializedObject.FindProperty("canSeeThroughObstacles");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();

        showBehaviours = EditorGUILayout.Foldout(showBehaviours, "Behaviours", true, EditorStyles.foldout);
        if (showBehaviours)
        {
            EditorGUILayout.PropertyField(scanType);
            EditorGUILayout.PropertyField(canSeeThroughObstacles);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
        if (showParameters)
        {
            EditorGUILayout.PropertyField(timeToScan);                      

            if (scanType.enumValueIndex == (int)Scanner.ScanType.fieldOfView)
            {
                EditorGUILayout.PropertyField(fovAngle);
                EditorGUILayout.PropertyField(scanAreaLenght);
                EditorGUILayout.PropertyField(spotLightColor);
                EditorGUILayout.PropertyField(detectedSpotlightColor);
            }
            else if(scanType.enumValueIndex == (int)Scanner.ScanType.circularArea)
            {
                EditorGUILayout.PropertyField(scanAreaRadius);
            }
            if(!canSeeThroughObstacles.boolValue)
                EditorGUILayout.PropertyField(obstacleLayer);
        }

        EditorGUILayout.Space();

        showEvents = EditorGUILayout.Foldout(showEvents, "Events", true, EditorStyles.foldout);
        if (showEvents)
        {
            EditorGUILayout.PropertyField(OnTargetSpotted);
            EditorGUILayout.PropertyField(OnTargetLost);
        }

        serializedObject.ApplyModifiedProperties();
    }

}
