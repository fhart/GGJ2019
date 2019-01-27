using System;
using System.Collections.Generic;
using UnityEngine;

public class Store : MapObject
{
    [SerializeField]
    private StoreConfig config;
    [SerializeField]
    private StoreManager doorPopUp;

    private Dictionary<ItemType, int> ItemValueMap;

    private void Start()
    {
        ItemValueMap = new Dictionary<ItemType, int>();

        foreach (var itemValue in config.ItemValues)
        {
            ItemValueMap.Add(itemValue.type, itemValue.value);
        }
    }

    public int GetValueForItem(ItemType type)
    {
        if (ItemValueMap.ContainsKey(type))
        {
            return ItemValueMap[type];
        }

        throw new Exception("La Re Cagamos");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        doorPopUp.Activate(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        doorPopUp.Hide();
    }
}
