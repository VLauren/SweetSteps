using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Effects : ScriptableObject
{
    static Effects instance;
    public static Effects Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<Effects>("Effects");
            }
            return instance;
        }
    }

    public List<GameObject> EffectPrefabs;

    public static void SpawnEffect(int _index, Vector3 _position, Quaternion _rotation = new Quaternion())
    {
        Instantiate(Instance.EffectPrefabs[_index], _position, _rotation);
    }

#if UNITY_EDITOR

    [MenuItem("PathPuzzle/Effects config", priority = 23)]
    public static void SelectParameters()
    {
        Selection.activeObject = Resources.Load("Effects");
    }

    [MenuItem("Assets/Create/PazzPuzzle/Effects")]
    public static void CreateEffectsConfig()
    {
        Effects newValues = ScriptableObject.CreateInstance<Effects>();

        // Si no existe la carpeta la creo
        if (!AssetDatabase.IsValidFolder("Assets/Data/Resources"))
            AssetDatabase.CreateFolder("Assets/Data", "Resources");

        AssetDatabase.CreateAsset(newValues, "Assets/Data/Resources/Effects.asset");
        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newValues;
    }

#endif

}
