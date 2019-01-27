using UnityEngine;

public class MapObject : MonoBehaviour
{
    public Vector2 positionOverride;
    public bool haveOverride;

    public Vector2 GetPositionToGo(Vector2 position)
    {
        if (haveOverride)
        {
            return positionOverride; 
        }
        else
        {
            return position;
        }
    }
}
