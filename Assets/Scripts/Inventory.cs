using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] slots;

    public void RemoveItem(int index)
    {
        for (int i = index, j = index + 1; j <= slots.Length; i++, j++)
        {
            if (j == slots.Length)
            {
                slots[i].sprite = null;
            }
            else
            {
                slots[i].sprite = slots[j].sprite;
            }
        }
    }
}
