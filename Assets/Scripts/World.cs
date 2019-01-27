using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Grid grid;

    private void Start()
    {
        var mapObjects = GetComponentsInChildren<MapObject>();

        grid = new Grid();
        grid.tiles = new MapObject[31, 10];

        foreach (var mapObject in mapObjects)
        {
            var startXPos = (int)mapObject.transform.position.x;
            var startYPos = (int)mapObject.transform.position.y;

            grid.tiles[startYPos, startXPos] = mapObject;
            grid.tiles[startYPos, startXPos + 1] = mapObject;
            grid.tiles[startYPos + 1, startXPos] = mapObject;
            grid.tiles[startYPos + 1, startXPos + 1] = mapObject;
            grid.tiles[startYPos + 2, startXPos] = mapObject;
            grid.tiles[startYPos + 2, startXPos + 1] = mapObject;
        }
    }
}
