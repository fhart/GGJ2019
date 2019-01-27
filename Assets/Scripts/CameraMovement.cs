using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Hero hero;
    [SerializeField]
    private float yOffset;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, hero.transform.position.y + yOffset, transform.position.z);
    }
}
