using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using System;

public class Hero : MonoBehaviour
{
    //adrian
    public float gridstep = 1;
    public float movementOffset = 0.5f;
    public float movementThershold = 0.2f;
    List<Vector2> positionsToMove;
    private SpriteRenderer Sprx;
    //adrian

    [SerializeField]
    public float speed;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private TextMeshProUGUI goldValue;
    [SerializeField]
    private World world;

    public Item[] items;

    public int gold;

    private Animator animator;

    private bool canMove;

    public void SellItem(int index, int price)
    {
        gold += price;
        items[index] = null;
        inventory.RemoveItem(index);

        goldValue.text = gold.ToString();
    }

    public void Pay(int price)
    {
        gold -= price;
        goldValue.text = gold.ToString();
    }

    public void SetInventory()
    {
        var i = 0;
        foreach (var item in items)
        {
            inventory.slots[i].sprite = item.Sprite;
            i++;
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    private void Start()
    {
        Sprx = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        goldValue.text = gold.ToString();

        positionsToMove = new List<Vector2>();
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        //adrian
        if (!HUB.IsAction && Input.GetMouseButtonDown(0))
        {
            var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var xpos = Mathf.Round(mousepos.x / gridstep) * gridstep + movementOffset;
            var ypos = Mathf.Round(mousepos.y / gridstep) * gridstep;

            xpos = Mathf.Clamp(xpos, 0.5f, 9.5f);
            ypos = Mathf.Clamp(ypos, 3.0f, 27.0f);

            var clickm = world.grid.GetPositionToGo(new Vector2(xpos, ypos));

            int count = 0;
            var currentDir = Direction.Invalid;
            var currentPos = new Vector2(Mathf.Floor(transform.position.x / gridstep) * gridstep + movementOffset, Mathf.Round(transform.position.y / gridstep) * gridstep);
            var directions = PathFinder.GetFasterRoute(world.grid, (int)currentPos.x, (int)currentPos.y, (int)clickm.x, (int)clickm.y);

            if (directions != null)
            {
                positionsToMove.Clear();
                positionsToMove.Add(currentPos);

                foreach (var direction in directions)
                {
                    if (currentDir != direction)
                    {
                        if (currentDir != Direction.Invalid)
                        {
                            switch (currentDir)
                            {
                                case Direction.Left:
                                    currentPos += Vector2.left * gridstep * count;
                                    break;
                                case Direction.Right:
                                    currentPos += Vector2.right * gridstep * count;
                                    break;
                                case Direction.Up:
                                    currentPos += Vector2.up * gridstep * count;
                                    break;
                                case Direction.Down:
                                    currentPos += Vector2.down * gridstep * count;
                                    break;
                            }

                            positionsToMove.Add(currentPos);
                            Debug.Log(currentPos);
                        }

                        currentDir = direction;
                        count = 1;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
        }

        if (positionsToMove.Any())
        {
            var currentPositionToMove = positionsToMove.First();

            if (Mathf.Abs(transform.position.x - currentPositionToMove.x) > movementThershold)
            {
                if (transform.position.x < currentPositionToMove.x)
                {
                    Sprx.flipX = false;
                }
                else
                {
                    Sprx.flipX = true;
                }
                animator.SetBool("Running", true);

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentPositionToMove.x, transform.position.y), speed * Time.deltaTime);
            }
            else if (Mathf.Abs(transform.position.y - currentPositionToMove.y) > movementThershold)
            {
                animator.SetBool("Running", true);

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, currentPositionToMove.y), speed * Time.deltaTime);
            }
            else
            {
                positionsToMove.RemoveAt(0);
                if (!positionsToMove.Any())
                {
                    animator.SetBool("Running", false);
                }
            }
        }
        //adrian
    }
}
