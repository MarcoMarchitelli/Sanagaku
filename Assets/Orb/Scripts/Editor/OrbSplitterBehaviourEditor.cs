using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Sangaku.EditorCustom
{
    //[CustomEditor(typeof(OrbSplitterBehaviour))]
    [CanEditMultipleObjects]
    public class OrbSplitterBehaviourEditor : Editor
    {
        SerializedProperty orbsNumber, angleOffset, minOrbsNumber, maxOrbsNumber, minAngle, maxAngle;

        bool randomize = true;

        private void OnEnable()
        {
            orbsNumber = serializedObject.FindProperty("orbsNumber");
            angleOffset = serializedObject.FindProperty("angleOffset");
            minOrbsNumber = serializedObject.FindProperty("minOrbsNumber");
            maxOrbsNumber = serializedObject.FindProperty("maxOrbsNumber");
            minAngle = serializedObject.FindProperty("minAngle");
            maxAngle = serializedObject.FindProperty("maxAngle");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.Space();
        }
    }
}