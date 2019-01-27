using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{
    public Item[] items;
    public Image[] dropSpot;
    public Image[] selectedDropSpot;

    public Text readyButtonText;
    public Button readyButton;

    public Hero hero;
    public HUB hub;

    private List<Item> selectedItems;

    public void SelectItem(int index)
    {
        if (selectedItems.Any(item => item.Type == items[index].Type))
        {
            selectedItems.Remove(items[index]);
            selectedDropSpot[index].gameObject.SetActive(false);
        }
        else if (selectedItems.Count < 5)
        {
            selectedItems.Add(items[index]);
            selectedDropSpot[index].gameObject.SetActive(true);
        }

        if (selectedItems.Count == 5)
        {
            readyButton.interactable = true;
            readyButtonText.text = "Ready!!!";
        }
        else
        {
            readyButtonText.text = string.Format("{0}/5", selectedItems.Count);
            readyButton.interactable = false;
        }
    }

    public void StartGame()
    {
        hero.items = selectedItems.ToArray();
        hero.SetInventory();

        hub.gameObject.SetActive(true);
        hero.EnableMovement();

        gameObject.SetActive(false);
    }

    private void Start()
    {
        selectedItems = new List<Item>();
        readyButton.interactable = false;
        readyButtonText.text = "0/5";

        hero.DisableMovement();

        items = items.OrderBy(x => Random.value).ToArray();

        for (int i = 0; i < items.Length; i++)
        {
            dropSpot[i].sprite = items[i].Sprite;
        }
    }
}
