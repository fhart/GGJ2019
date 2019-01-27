using UnityEditor;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    private ItemType type;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int score;

    public ItemType Type { get { return type; } }

    public Sprite Sprite { get { return sprite; } }

    public int Score { get { return score; } }

    [MenuItem("Assets/Create/Game Elements/Item")]
    public static void CreateMyAsset()
    {
        var asset = ScriptableObject.CreateInstance<Item>();

        ProjectWindowUtil.CreateAsset(asset, "Assets/Items/Item.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
