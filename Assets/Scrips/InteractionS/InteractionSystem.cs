using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("상호 작용 설정")]
    public float interactionRange = 2.0f;
    public LayerMask interactionLayerMask = 1;
    public KeyCode interactionKey = KeyCode.E;

    [Header("UI")]
    public Text interactionText;
    public GameObject interactionUI;

    private Transform PlayerTransform;
    private InteractionObj currentInteractiable;

    private void Start()
    {
        if(PlayerTransform == null) PlayerTransform = transform;
    }

    private void Update()
    {
        ChackForInteractables();
        HendlenteractionInput();
    }

    private void HendlenteractionInput()
    {
        if(currentInteractiable!=null&&Input.GetKeyDown(interactionKey))
        {
            currentInteractiable = null;
        }
    }

    private void ShowInteractionUI(string text)
    {
        if(interactionUI != null)
        {
            interactionUI.SetActive(true);
        }
        if(interactionUI != null)
        {
            interactionText.text = text;
        }
    }

    private void HideInteractionUI()
    {
        if(interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    private void ChackForInteractables()
    {
        Vector3 chackPosition = PlayerTransform.position + PlayerTransform.forward * (interactionRange * 0.5f);

        Collider[] hitColliders = Physics.OverlapSphere(chackPosition, interactionRange, interactionLayerMask);

        InteractionObj closestinteractable = null;
        float closesDistance = float.MaxValue;

        foreach(Collider collider in hitColliders)
        {
            InteractionObj interactionObj = collider.GetComponent<InteractionObj>();
            if( interactionObj != null )
            {
                float distance = Vector3.Distance(PlayerTransform.position,collider.transform.position);

                Vector3 directionToObj = (collider.transform.position - PlayerTransform.position).normalized;
                float angle = Vector3.Angle(PlayerTransform.forward, directionToObj);

                if (angle < 90f && distance < closesDistance)
                {
                    closesDistance = distance;
                    closestinteractable = interactionObj;
                }
            }            
        }

        if(closestinteractable!=currentInteractiable)
        {
            if(currentInteractiable != null)
            {
                currentInteractiable.OnPlayerExit();
            }

            currentInteractiable = closestinteractable;

            if(currentInteractiable != null)
            {
                currentInteractiable.OnPlayerEnter();
                ShowInteractionUI(currentInteractiable.GetInteractionText());
            }
            else
            {
                HideInteractionUI();
            }
        }

    }
}
