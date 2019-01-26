using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private SelectionBorder border;

    [SerializeField]
    private LootItem[] items;
    [SerializeField]
    private int selectedItemIndex;

    [SerializeField]
    private int gold;

    private Store currentStore;

    private void Start()
    {
        var i = 0;
        foreach (var item in items)
        {
            inventory.slots[i].sprite = item.GetComponent<SpriteRenderer>().sprite;
            i++;
        }
    }

    void Update()
    {
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position += Vector3.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentStore != null)
            {
                Debug.Log(currentStore.GetValueForItem(items[selectedItemIndex]));
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
