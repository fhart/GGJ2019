using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class Hero : MonoBehaviour
{
	//adrian
	public float gridstep = 1;
	Vector2 clickm;
	public SpriteRenderer Sprx;
	//adrian
	
    [SerializeField]
    public float speed;
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
		
		//adrian
		clickm = transform.position;
		//adrian
		
        var i = 0;
        foreach (var item in items)
        {
            inventory.slots[i].sprite = item.Sprite;
            i++;
        }
    }

    private void Update()
    {
        //adrian
		if (Input.GetMouseButtonDown(0))
		{
			var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			var xpos = Mathf.Round(mousepos.x / gridstep) * gridstep;
			var ypos = Mathf.Round(mousepos.y / gridstep) * gridstep;
			clickm = new Vector2(xpos, ypos);
			Debug.Log(clickm);
			
			}
		
		if (transform.position.x != clickm.x)
		{
			if (transform.position.x <clickm.x)
			{
				Debug.Log("der");
				Sprx.flipX = true;
			}else{
				Sprx.flipX = false;
				Debug.Log("izq");
			}
			
			transform.position = Vector2.MoveTowards(transform.position, new Vector2(clickm.x, transform.position.y), speed * Time.deltaTime);
		}
		else if (transform.position.y != clickm.y)
		{
			if (transform.position.y <clickm.y)
			{
				Debug.Log("arr");
			}else{
				Debug.Log("aba");
			}
			
			transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, clickm.y), speed * Time.deltaTime);
		}
		//adrian

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
