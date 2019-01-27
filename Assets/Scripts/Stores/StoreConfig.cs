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

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Game Elements/Store Config")]
    public static void CreateMyAsset()
    {
        var asset = ScriptableObject.CreateInstance<StoreConfig>();

        ProjectWindowUtil.CreateAsset(asset, "Assets/StoreConfigs/StoreConfig.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif
}
