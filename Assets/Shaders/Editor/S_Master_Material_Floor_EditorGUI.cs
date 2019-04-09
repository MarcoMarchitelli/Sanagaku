using UnityEngine;
using UnityEditor;

public class S_Master_Material_Floor_EditorGUI : ShaderGUI
{
    // link spiegone : https://catlikecoding.com/unity/tutorials/rendering/part-9/

    MaterialEditor materialEditor;
    MaterialProperty[] properties;

    public override void OnGUI(MaterialEditor _materialEditor, MaterialProperty[] _properties)
    {
        materialEditor = _materialEditor;
        properties = _properties;
        DoMain();
    }

    void DoMain()
    {
        GUILayout.Label("Main Maps", EditorStyles.boldLabel);

        MaterialProperty mainTex = FindProperty("_MainTex", properties);
        GUIContent albedoLabel = new GUIContent(mainTex.displayName);
        materialEditor.TexturePropertySingleLine(albedoLabel, mainTex);
    }
}
