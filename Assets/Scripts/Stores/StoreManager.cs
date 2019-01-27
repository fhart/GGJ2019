using System;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private Store currentStore;
    [SerializeField]
    private Hero hero;
    [SerializeField]
    private float radius;
    [SerializeField]
    private Canvas storeCanvas;
    [SerializeField]
    private Image[] heroItemsSlots;
    private ItemType[] heroItemsTypes = new ItemType[5];
    [SerializeField]
    private Image selectionBorder;
    [SerializeField]
    private Text dialogText;
    [SerializeField]
    private string dialogItemInfoFormat;
    [SerializeField]
    private string welcomeDialog;
    [SerializeField]
    private string InvalidSellDialog;
    [SerializeField]
    private string successfulSellDialog;

    [SerializeField]
    private AudioClip openDoor;
    [SerializeField]
    private AudioClip closeDoor;
    [SerializeField]
    private AudioClip sell;
    [SerializeField]
    private AudioClip invalidSell;

    private AudioSource audioSource;
    private int selectedItemIndex;

    public event Action OnShow = delegate { };
    public event Action OnHide = delegate { };

    public void Activate(Store store)
    {
        gameObject.SetActive(true);
        transform.position = store.transform.position + Vector3.right * 1.15f + Vector3.up;
        currentStore = store;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        currentStore = null;
    }

    public void ExitStore()
    {
        hero.EnableMovement();
        storeCanvas.gameObject.SetActive(false);
        selectionBorder.gameObject.SetActive(false);
        audioSource.clip = closeDoor;
        audioSource.Play();

        OnHide?.Invoke();
    }

    public void SelectItem(int index)
    {
        selectedItemIndex = index;
        selectionBorder.gameObject.SetActive(true);
        selectionBorder.rectTransform.localPosition = heroItemsSlots[index].rectTransform.localPosition;

        dialogText.text = string.Format(dialogItemInfoFormat, currentStore.GetValueForItem(heroItemsTypes[index]));
    }

    public void SellSelectedItem()
    {
        if (selectedItemIndex != -1)
        {
            hero.SellItem(selectedItemIndex, currentStore.GetValueForItem(heroItemsTypes[selectedItemIndex]));
            heroItemsSlots[selectedItemIndex].GetComponent<Button>().interactable = false;
            selectedItemIndex = -1;
            selectionBorder.gameObject.SetActive(false);
            dialogText.text = successfulSellDialog;
            audioSource.clip = sell;
            audioSource.Play();
        }
        else
        {
            dialogText.text = InvalidSellDialog;
            audioSource.clip = invalidSell;
            audioSource.Play();
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!HUB.IsAction && Input.GetMouseButtonDown(0))
        {
            Vector3 mousepos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if ((transform.position - mousepos).sqrMagnitude < radius * radius)
            {
                hero.DisableMovement();
                InitializeStore();
            }
        }
    }

    private void InitializeStore()
    {
        storeCanvas.gameObject.SetActive(true);
        dialogText.text = welcomeDialog;
        selectedItemIndex = -1;

        SetHeroItems();
        audioSource.clip = openDoor;
        audioSource.Play();

        OnShow?.Invoke();
    }

    private void SetHeroItems()
    {
        var i = 0;
        foreach (var item in hero.items)
        {
            if (item != null)
            {
                heroItemsSlots[i].sprite = item.Sprite;
                heroItemsTypes[i] = item.Type;
            }

            i++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
