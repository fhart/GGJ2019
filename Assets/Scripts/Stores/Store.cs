using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField]
    private StoreConfig config;

    private Dictionary<ItemType, int> ItemValueMap;

    private void Start()
    {
        ItemValueMap = new Dictionary<ItemType, int>();

        foreach (var itemValue in config.ItemValues)
        {
            ItemValueMap.Add(itemValue.type, itemValue.value);
        }
    }

    public int GetValueForItem(Item item)
    {
        if (ItemValueMap.ContainsKey(item.Type))
        {
            return ItemValueMap[item.Type];
        }

        throw new Exception("La Re Cagamos");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Me toco " + collision.gameObject.name);
    }
}
