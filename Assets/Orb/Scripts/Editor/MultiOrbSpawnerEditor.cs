using UnityEditor;

namespace Sangaku.EditorCustom
{
    [CustomEditor(typeof(MultiOrbSpawner))]
    [CanEditMultipleObjects]
    public class MultiOrbSpawnerEditor : Editor
    {
        SerializedProperty poolTag, randomize, orbsNumber, angleOffset, minOrbsNumber, maxOrbsNumber, minAngle, maxAngle, forwardOffset;

        private void OnEnable()
        {
            poolTag = serializedObject.FindProperty("poolTag");
            randomize = serializedObject.FindProperty("randomize");

            orbsNumber = serializedObject.FindProperty("orbsNumber");
            angleOffset = serializedObject.FindProperty("angleOffset");

            minOrbsNumber = serializedObject.FindProperty("minOrbsNumber");
            maxOrbsNumber = serializedObject.FindProperty("maxOrbsNumber");
            minAngle = serializedObject.FindProperty("minAngle");
            maxAngle = serializedObject.FindProperty("maxAngle");

            forwardOffset = serializedObject.FindProperty("forwardOffset");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(poolTag);
            EditorGUILayout.PropertyField(randomize);

            EditorGUILayout.Space();

            if (randomize.boolValue)
            {
                EditorGUILayout.PropertyField(minOrbsNumber);
                EditorGUILayout.PropertyField(maxOrbsNumber);
                EditorGUILayout.PropertyField(minAngle);
                EditorGUILayout.PropertyField(maxAngle);
            }
            else
            {
                EditorGUILayout.PropertyField(orbsNumber);
                EditorGUILayout.PropertyField(angleOffset);
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(forwardOffset);

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }
    }
}