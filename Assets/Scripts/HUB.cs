using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HUB : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool IsAction;

    public Hero hero;

    public CarpenterManager carpenterMnager;
    public StoreManager storeManager;
    public Home home;
    [SerializeField]
    private AudioClip openInventory;
    [SerializeField]
    private AudioClip closeInventory;

    private AudioSource audioSource;

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
            audioSource.clip = closeInventory;
            audioSource.Play();
        }
        else
        {
            hero.DisableMovement();
            inventory.SetActive(true);
            audioSource.clip = openInventory;
            audioSource.Play();
        }

        inventoryIsOpen = !inventoryIsOpen;
    }

    private void Start()
    {
        carpenterMnager.OnShow += HideInventoryButton;
        storeManager.OnShow += HideInventoryButton;
        home.OnShow += HideInventoryButton;

        carpenterMnager.OnHide += ShowInventoryButton;
        storeManager.OnHide += ShowInventoryButton;
        home.OnHide += ShowInventoryButton;

        audioSource = GetComponent<AudioSource>();
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
