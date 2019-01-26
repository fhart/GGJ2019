using System;
using UnityEditor;
using UnityEngine;

public class StoreConfig : ScriptableObject
{
    public ItemValue[] ItemValues; 

    [Serializable]
    public class ItemValue
    {
        public ItemType type;
        public int value;
    }

    [MenuItem("Assets/Create/Store Config")]
    public static void CreateMyAsset()
    {
        var asset = ScriptableObject.CreateInstance<StoreConfig>();

        AssetDatabase.CreateAsset(asset, "Assets/StoreConfig.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
