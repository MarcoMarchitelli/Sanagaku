using UnityEditor;

[CustomEditor(typeof(PlayerController))]
[CanEditMultipleObjects]
public class PalyerControllerEditor : Editor
{
    //references
    SerializedProperty walkSmoke, equippedGun, gunPoint;

    //behaviours
    SerializedProperty InputDirection, parry, dash;

    //parameters
    SerializedProperty shootInput, dashInput, parryInput, MaskToIgnore, aimLayer, parryRadious, parryTime, parryCooldown, dashDistance, dashTime, dashCooldown;

    //events
    SerializedProperty OnShoot;

    bool showReferences = true, showBehaviours = true, showParameters = true, showEvents = true;

    private void OnEnable()
    {
        walkSmoke = serializedObject.FindProperty("walkSmoke");
        equippedGun = serializedObject.FindProperty("equippedGun");
        gunPoint = serializedObject.FindProperty("gunPoint");

        InputDirection = serializedObject.FindProperty("InputDirection");
        parry = serializedObject.FindProperty("parry");
        dash = serializedObject.FindProperty("dash");

        shootInput = serializedObject.FindProperty("shootInput");
        parryInput = serializedObject.FindProperty("parryInput");
        dashInput = serializedObject.FindProperty("dashInput");
        MaskToIgnore = serializedObject.FindProperty("MaskToIgnore");
        aimLayer = serializedObject.FindProperty("aimLayer");
        parryRadious = serializedObject.FindProperty("parryRadious");
        parryTime = serializedObject.FindProperty("parryTime");
        parryCooldown = serializedObject.FindProperty("parryCooldown");
        dashDistance = serializedObject.FindProperty("dashDistance");
        dashTime = serializedObject.FindProperty("dashTime");
        dashCooldown = serializedObject.FindProperty("dashCooldown");

        OnShoot = serializedObject.FindProperty("OnShoot");
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
            EditorGUILayout.PropertyField(dash);
        }

        EditorGUILayout.Space();

        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", true, EditorStyles.foldout);
        if (showParameters)
        {
            EditorGUILayout.PropertyField(MaskToIgnore);
            EditorGUILayout.PropertyField(aimLayer);
            EditorGUILayout.PropertyField(shootInput);
            if (parry.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Parry", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(parryInput);
                EditorGUILayout.PropertyField(parryRadious);
                EditorGUILayout.PropertyField(parryTime);
                EditorGUILayout.PropertyField(parryCooldown);
            }
            if (dash.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Dash", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(dashInput);
                EditorGUILayout.PropertyField(dashDistance);
                EditorGUILayout.PropertyField(dashTime);
                EditorGUILayout.PropertyField(dashCooldown);
            }
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
