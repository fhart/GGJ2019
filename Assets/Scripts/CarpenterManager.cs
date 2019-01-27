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
    private string dialogServiceInfoFormat;
    [SerializeField]
    private string welcomeDialog;
    [SerializeField]
    private string InvalidFixDialog;
    [SerializeField]
    private string successFixDialog;

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

        OnHide?.Invoke();
    }
    
    public void RequestFix()
    {
        if (currentHomeTier < currentCarpenter.houseTiers.Count && hero.gold > currentCarpenter.houseTiers[currentHomeTier].value)
        {
            currentCarpenter.FixHouse(currentHomeTier);
            dialogText.text = successFixDialog;
            hero.Pay(currentCarpenter.houseTiers[currentHomeTier].value);
            currentHomeTier++;
        }
        else
        {
            dialogText.text = InvalidFixDialog;
        }
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
        dialogText.text = welcomeDialog;

        OnShow?.Invoke();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
