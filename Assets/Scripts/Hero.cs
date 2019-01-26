using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private SelectionBorder border;
    [SerializeField]
    private TextMeshProUGUI goldValue;

    [SerializeField]
    private List<Item> items;
    [SerializeField]
    private int selectedItemIndex;

    [SerializeField]
    private int gold;

    private Store currentStore;

    public void SellItem()
    {
        gold += currentStore.GetValueForItem(items[selectedItemIndex]);
        items.RemoveAt(selectedItemIndex);
        selectedItemIndex = 0;
        inventory.RemoveItem(selectedItemIndex);
        border.SetPositionIndex(selectedItemIndex);

        goldValue.text = gold.ToString();
    }

    private void Start()
    {
        var i = 0;
        foreach (var item in items)
        {
            inventory.slots[i].sprite = item.Sprite;
            i++;
        }
    }

    private void Update()
    {
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position += Vector3.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentStore != null && items.Count > selectedItemIndex)
            {
                Debug.Log(currentStore.GetValueForItem(items[selectedItemIndex]));
                SellItem();
            }
        }

        border.SetPositionIndex(selectedItemIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entrando a " + collision.gameObject.name);

        currentStore = collision.gameObject.GetComponent<Store>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Saliendo de " + collision.gameObject.name);

        currentStore = null;
    }
}
