using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HUB : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool IsAction;

    public Hero hero;

    public CarpenterManager carpenterMnager;
    public StoreManager storeManager;

    public GameObject inventory;
    public GameObject inventoryButton;
    private bool inventoryIsOpen;

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsAction = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsAction = false;
    }

    public void ToggleInventory()
    {
        if (inventoryIsOpen)
        {
            hero.EnableMovement();
            inventory.SetActive(false);
        }
        else
        {
            hero.DisableMovement();
            inventory.SetActive(true);
        }

        inventoryIsOpen = !inventoryIsOpen;
    }

    private void Start()
    {
        carpenterMnager.OnShow += HideInventoryButton;
        storeManager.OnShow += HideInventoryButton;

        carpenterMnager.OnHide += ShowInventoryButton;
        storeManager.OnHide += ShowInventoryButton;
    }

    private void ShowInventoryButton()
    {
        inventoryButton.SetActive(true);
    }

    private void HideInventoryButton()
    {
        inventoryButton.SetActive(false);
    }
}
