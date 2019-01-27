using System;
using UnityEngine;
using UnityEngine.UI;

public class CarpenterManager : MonoBehaviour
{
    [SerializeField]
    private Carpenter currentCarpenter;
    [SerializeField]
    private Hero hero;
    [SerializeField]
    private float radius;
    [SerializeField]
    private Canvas carpenterCanvas;
    [SerializeField]
    private Text dialogText;
    [SerializeField]
    private string welcomeDialog;
    [SerializeField]
    private string InvalidFixDialog;
    [SerializeField]
    private string successFixDialog;
    [SerializeField]
    private string allFixesDoneDialog;

    [SerializeField]
    private AudioClip openDoor;
    [SerializeField]
    private AudioClip closeDoor;
    [SerializeField]
    private AudioClip work;
    [SerializeField]
    private AudioClip invalidRequest;

    private AudioSource audioSource;
    private int currentHomeTier;

    public event Action OnShow = delegate { };
    public event Action OnHide = delegate { };

    public void Activate(Carpenter carpenter)
    {
        gameObject.SetActive(true);
        transform.position = carpenter.transform.position + Vector3.right * 1.15f + Vector3.up;
        currentCarpenter = carpenter;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        currentCarpenter = null;
    }

    public void ExitCarpentry()
    {
        hero.EnableMovement();
        carpenterCanvas.gameObject.SetActive(false);

        audioSource.clip = closeDoor;
        audioSource.Play();

        OnHide?.Invoke();
    }
    
    public void RequestFix()
    {
        if (currentHomeTier < currentCarpenter.houseTiers.Count && hero.gold > currentCarpenter.houseTiers[currentHomeTier].value)
        {
            currentCarpenter.FixHouse(currentHomeTier);
            hero.Pay(currentCarpenter.houseTiers[currentHomeTier].value);
            currentHomeTier++;

            audioSource.clip = work;
            audioSource.Play();

            if (currentHomeTier < currentCarpenter.houseTiers.Count)
            {
                dialogText.text = string.Format(successFixDialog, currentCarpenter.houseTiers[currentHomeTier].value);
            }
            else
            {
                dialogText.text = allFixesDoneDialog;
            }
        }
        else
        {
            audioSource.clip = invalidRequest;
            audioSource.Play();

            if (currentHomeTier < currentCarpenter.houseTiers.Count)
            {
                dialogText.text = InvalidFixDialog;
            }
            else
            {
                dialogText.text = allFixesDoneDialog;
            }
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!HUB.IsAction && Input.GetMouseButtonDown(0))
        {
            Vector3 mousepos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if ((transform.position - mousepos).sqrMagnitude < radius * radius)
            {
                hero.DisableMovement();
                InitializeStore();
            }
        }
    }

    private void InitializeStore()
    {
        carpenterCanvas.gameObject.SetActive(true);
        dialogText.text = string.Format(welcomeDialog, currentCarpenter.houseTiers[currentHomeTier].value);
        audioSource.clip = openDoor;
        audioSource.Play();

        OnShow?.Invoke();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
