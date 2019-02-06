using UnityEditor;

[CustomEditor(typeof(TestBullet))]
[CanEditMultipleObjects]
public class TestBulletEditor : Editor
{
    //references
    SerializedProperty HitSmoke, Trail, DeathTimer;

    //behaviours
    SerializedProperty deathBehaviour, speedOverLifeTime;

    //parameters
    SerializedProperty collisionLayer, speedOverLifeTimeCurve, moveSpeed, lifeTime, bouncesToDie, damage, deathTime;

    //events
    SerializedProperty OnLifeEnd;

    bool showReferences = true, showBehaviours = true, showParameters = true, showEvents = true;

    private void OnEnable()
    {
        HitSmoke = serializedObject.FindProperty("HitSmoke");
        Trail = serializedObject.FindProperty("Trail");
        DeathTimer = serializedObject.FindProperty("DeathTimer");

        deathBehaviour = serializedObject.FindProperty("deathBehaviour");
        speedOverLifeTime = serializedObject.FindProperty("speedOverLifeTime");

        collisionLayer = serializedObject.FindProperty("collisionLayer");
        moveSpeed = serializedObject.FindProperty("moveSpeed");
        speedOverLifeTimeCurve = serializedObject.FindProperty("speedOverLifeTimeCurve");
        lifeTime = serializedObject.FindProperty("lifeTime");
        bouncesToDie = serializedObject.FindProperty("bouncesToDie");
        damage = serializedObject.FindProperty("damage");
        deathTime = serializedObject.FindProperty("deathTime");

        OnLifeEnd = serializedObject.FindProperty("OnLifeEnd");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorStyles.foldout.fontStyle = UnityEngine.FontStyle.Bold;

        EditorGUILayout.Space();

        showReferences = EditorGUILayout.Foldout(showReferences, "References", true, EditorStyles.foldout);
        if (showReferences)
        {
            EditorGUILayout.PropertyField(HitSmoke);
            EditorGUILayout.PropertyField(Trail);
            EditorGUILayout.PropertyField(DeathTimer);
        }

        EditorGUILayout.Space();

        showBehaviours = EditorGUILayout.Foldout(showBehaviours, "Behaviours", true, EditorStyles.foldout);
        if (showBehaviours)
        {
            EditorGUILayout.PropertyField(deathBehaviour);
            if(deathBehaviour.enumValueIndex == (int)TestBullet.DeathBehaviour.byTime)
                EditorGUILayout.PropertyField(speedOverLifeTime);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
        if (showParameters)
        {

            EditorGUILayout.PropertyField(moveSpeed);
            EditorGUILayout.PropertyField(damage);
            EditorGUILayout.PropertyField(collisionLayer);
            if (deathBehaviour.enumValueIndex == (int)TestBullet.DeathBehaviour.byTime)
            {
                EditorGUILayout.PropertyField(lifeTime);
                EditorGUILayout.PropertyField(deathTime);
                if (speedOverLifeTime.boolValue)
                {
                    EditorGUILayout.PropertyField(speedOverLifeTimeCurve);
                    //speedOverLifeTimeCurve.animationCurveValue.keys[0].time = 0;
                    //speedOverLifeTimeCurve.animationCurveValue.keys[0].value = moveSpeed.floatValue;
                    //speedOverLifeTimeCurve.animationCurveValue.keys[1].time = lifeTime.floatValue;
                    //speedOverLifeTimeCurve.animationCurveValue.keys[1].value = 0;
                }
            }
            if (deathBehaviour.enumValueIndex == (int)TestBullet.DeathBehaviour.byBounces)
                EditorGUILayout.PropertyField(bouncesToDie);
        }

        EditorGUILayout.Space();

        showEvents = EditorGUILayout.Foldout(showEvents, "Events", true, EditorStyles.foldout);
        if (showEvents)
        {
            EditorGUILayout.PropertyField(OnLifeEnd);
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();

    }

}
