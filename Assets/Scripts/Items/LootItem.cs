using UnityEngine;

public class LootItem : MonoBehaviour
{
    [SerializeField]
    private ItemType type;

    public ItemType Type { get { return type; } }
}
