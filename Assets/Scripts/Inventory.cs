using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] slots;
    public Sprite emptySlot;

    public void RemoveItem(int index)
    {
        slots[index].sprite = emptySlot;

        //for (int i = index, j = index + 1; j <= slots.Length; i++, j++)
        //{
        //    if (j == slots.Length)
        //    {
        //        slots[i].sprite = emptySlot;
        //    }
        //    else
        //    {
        //        slots[i].sprite = slots[j].sprite;
        //    }
        //}
    }
}
