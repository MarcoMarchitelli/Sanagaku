using UnityEditor;

[CustomEditor(typeof(TestEnemy))]
public class TestEnemyEditor : Editor {

    SerializedProperty deathParticles, deathBehaviour, BouncesNeededToDie, health, OnDeath, dealsDamageOnContact, damage;

    bool showReferences = true, showBehaviours = true, showParameters = true, showEvents = true;

    private void OnEnable()
    {
        deathParticles = serializedObject.FindProperty("deathParticles");
        deathBehaviour = serializedObject.FindProperty("deathBehaviour");
        BouncesNeededToDie = serializedObject.FindProperty("BouncesNeededToDie");
        health = serializedObject.FindProperty("health");
        OnDeath = serializedObject.FindProperty("OnDeath");
        dealsDamageOnContact = serializedObject.FindProperty("dealsDamageOnContact");
        damage = serializedObject.FindProperty("damage");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorStyles.foldout.fontStyle = UnityEngine.FontStyle.Bold;

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
            EditorGUILayout.PropertyField(dealsDamageOnContact);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
        if (showParameters)
        {
            if(deathBehaviour.enumValueIndex == (int)TestEnemy.DeathBeahviour.diesFromBounces)
                EditorGUILayout.PropertyField(BouncesNeededToDie);
            else if (deathBehaviour.enumValueIndex == (int)TestEnemy.DeathBeahviour.diesFromDamage)
                EditorGUILayout.PropertyField(health);
            if(dealsDamageOnContact.boolValue)
                EditorGUILayout.PropertyField(damage);
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
