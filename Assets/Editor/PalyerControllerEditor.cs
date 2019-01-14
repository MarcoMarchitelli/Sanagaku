using UnityEditor;

[CustomEditor(typeof(PlayerController))]
[CanEditMultipleObjects]
public class PalyerControllerEditor : Editor
{
    //references
    SerializedProperty walkSmoke, equippedGun, gunPoint;

    //behaviours
    SerializedProperty InputDirection, parry, automaticParry, dash;

    //parameters
    SerializedProperty health, moveSpeed, shootInput, dashInput, parryInput, MaskToIgnore, aimLayer, parryRadius, parryTime, parryCooldown, dashDistance, dashSpeed, dashCooldown;

    //events
    SerializedProperty OnShoot, OnParryStart, OnParryEnd, OnDashStart, OnDashEnd;

    bool showReferences = true, showBehaviours = true, showParameters = true, showEvents = true;

    private void OnEnable()
    {
        walkSmoke = serializedObject.FindProperty("walkSmoke");
        equippedGun = serializedObject.FindProperty("equippedGun");
        gunPoint = serializedObject.FindProperty("gunPoint");

        InputDirection = serializedObject.FindProperty("InputDirection");
        parry = serializedObject.FindProperty("parry");
        automaticParry = serializedObject.FindProperty("automaticParry");
        dash = serializedObject.FindProperty("dash");

        health = serializedObject.FindProperty("health");
        moveSpeed = serializedObject.FindProperty("moveSpeed");
        shootInput = serializedObject.FindProperty("shootInput");
        parryInput = serializedObject.FindProperty("parryInput");
        dashInput = serializedObject.FindProperty("dashInput");
        MaskToIgnore = serializedObject.FindProperty("MaskToIgnore");
        aimLayer = serializedObject.FindProperty("aimLayer");
        parryRadius = serializedObject.FindProperty("parryRadius");
        parryTime = serializedObject.FindProperty("parryTime");
        parryCooldown = serializedObject.FindProperty("parryCooldown");
        dashDistance = serializedObject.FindProperty("dashDistance");
        dashSpeed = serializedObject.FindProperty("dashSpeed");
        dashCooldown = serializedObject.FindProperty("dashCooldown");

        OnShoot = serializedObject.FindProperty("OnShoot");
        OnParryStart = serializedObject.FindProperty("OnParryStart");
        OnParryEnd = serializedObject.FindProperty("OnParryEnd");
        OnDashStart = serializedObject.FindProperty("OnDashStart");
        OnDashEnd = serializedObject.FindProperty("OnDashEnd");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorStyles.foldout.fontStyle = UnityEngine.FontStyle.Bold;

        EditorGUILayout.Space();

        showReferences = EditorGUILayout.Foldout(showReferences, "References", true, EditorStyles.foldout);
        if (showReferences)
        {
            EditorGUILayout.PropertyField(gunPoint);
            EditorGUILayout.PropertyField(equippedGun);
            EditorGUILayout.PropertyField(walkSmoke);
        }

        EditorGUILayout.Space();

        showBehaviours = EditorGUILayout.Foldout(showBehaviours, "Behaviours", true, EditorStyles.foldout);
        if (showBehaviours)
        {
            EditorGUILayout.PropertyField(InputDirection);
            EditorGUILayout.PropertyField(parry);
            if(parry.boolValue)
                EditorGUILayout.PropertyField(automaticParry);
            EditorGUILayout.PropertyField(dash);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
        if (showParameters)
        {
            EditorGUILayout.PropertyField(health);
            EditorGUILayout.PropertyField(moveSpeed);
            EditorGUILayout.PropertyField(MaskToIgnore);
            EditorGUILayout.PropertyField(aimLayer);
            EditorGUILayout.PropertyField(shootInput);
            if (parry.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Parry", EditorStyles.boldLabel);
                if(!automaticParry.boolValue)
                    EditorGUILayout.PropertyField(parryInput);
                EditorGUILayout.PropertyField(parryRadius);
                EditorGUILayout.PropertyField(parryTime);
                EditorGUILayout.PropertyField(parryCooldown);
            }
            if (dash.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Dash", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(dashInput);
                EditorGUILayout.PropertyField(dashDistance);
                EditorGUILayout.PropertyField(dashSpeed);
                EditorGUILayout.PropertyField(dashCooldown);
            }
        }

        EditorGUILayout.Space();

        showEvents = EditorGUILayout.Foldout(showEvents, "Events", true, EditorStyles.foldout);
        if (showEvents)
        {
            EditorGUILayout.PropertyField(OnShoot);
            if (parry.boolValue)
            {
                EditorGUILayout.PropertyField(OnParryStart);
                EditorGUILayout.PropertyField(OnParryEnd);
            }
            if (dash.boolValue)
            {
                EditorGUILayout.PropertyField(OnDashStart);
                EditorGUILayout.PropertyField(OnDashEnd);
            }
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();

    }

}
