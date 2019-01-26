using UnityEngine;

public class SelectionBorder : MonoBehaviour
{
    public float firstPosition;
    public float offset;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetPositionIndex(int index)
    {
        transform.localPosition = new Vector2(firstPosition + offset * index, transform.localPosition.y);
    }
}
