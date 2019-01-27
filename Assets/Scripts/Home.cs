using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Home : MapObject
{
    private SpriteRenderer spriteRenderer;
    private TextMeshPro completion;

    public void UpdateHouse(int newCompletion, Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        completion.text = string.Format("{0}% completed", newCompletion);
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        completion = GetComponentInChildren<TextMeshPro>();
    }
}
