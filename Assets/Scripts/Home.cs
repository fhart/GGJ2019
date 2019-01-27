using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MapObject
{
    [SerializeField]
    private Hero hero;
    [SerializeField]
    private float radius;
    [SerializeField]
    private Canvas homeCanvas;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject doorPopUp;
    [SerializeField]
    private GameObject surePopUp;
    [SerializeField]
    private GameObject insideHome;
    [SerializeField]
    private Image[] heroItemsSlots;
    [SerializeField]
    private RawImage insideBackground;
    [SerializeField]
    private Texture insideTexture;

    [SerializeField]
    private AudioClip openDoor;
    [SerializeField]
    private AudioClip closeDoor;
    [SerializeField]
    private AudioClip endGame;

    private SpriteRenderer spriteRenderer;
    private TextMeshPro completion;
    private AudioSource audioSource;

    private bool inArea;

    private int completionValue;

    public event Action OnShow = delegate { };
    public event Action OnHide = delegate { };

    public void ShowDoorPopUp()
    {
        doorPopUp.gameObject.SetActive(true);
        inArea = true;
    }

    public void HideDoorPopUp()
    {
        doorPopUp.gameObject.SetActive(false);
        inArea = false;
    }

    public void ExitHome()
    {
        hero.EnableMovement();
        homeCanvas.gameObject.SetActive(false);
        audioSource.clip = closeDoor;
        audioSource.Play();

        OnHide?.Invoke();
    }

    public void EndGame()
    {
        surePopUp.gameObject.SetActive(false);
        insideHome.gameObject.SetActive(true);
        SetupHome();
        audioSource.clip = endGame;
        audioSource.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateHouse(int newCompletion, Sprite sprite, Texture inside)
    {
        spriteRenderer.sprite = sprite;
        completion.text = string.Format("{0}% completed", newCompletion);
        completionValue = newCompletion;
        insideTexture = inside;
    }

    private void SetupHome()
    {
        var score = hero.gold;

        if (completionValue == 0)
        {
            score -= 5000;
        }
        else
        {
            score += completionValue * 5;
        }

        var i = 0;
        foreach (var item in hero.items)
        {
            if (item != null)
            {
                heroItemsSlots[i].sprite = item.Sprite;
                score += item.Score;
                Debug.Log(score);
            }

            i++;
        }

        scoreText.text = string.Format("Your score is {0}", score);
        insideBackground.texture = insideTexture;

        OnShow?.Invoke();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        completion = GetComponentInChildren<TextMeshPro>();
        audioSource = GetComponent<AudioSource>();
        completionValue = 0;
    }

    private void Update()
    {
        if (!HUB.IsAction && inArea && Input.GetMouseButtonDown(0))
        {
            Vector3 mousepos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if ((doorPopUp.transform.position - mousepos).sqrMagnitude < radius * radius)
            {
                hero.DisableMovement();
                homeCanvas.gameObject.SetActive(true);
                surePopUp.gameObject.SetActive(true);
                insideHome.gameObject.SetActive(false);

                audioSource.clip = openDoor;
                audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShowDoorPopUp();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideDoorPopUp();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(doorPopUp.transform.position, radius);
    }
}
