using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class InteractionObj : MonoBehaviour
{
    [Header("��ȣ �ۿ� ����")]
    public string objName = "������";
    public string interactionText = "[E] ��ȣ �ۿ�";
    public InteractionType interactionType = InteractionType.Item;

    [Header("���̶���Ʈ ����")]
    public Color highlightColor = Color.yellow;
    public float highlightIntensity = 1.5f;

    public Renderer objectRanderer;
    private Color originalColor;
    private bool isHightlighted = false;

    public enum InteractionType
    {
        Item,
        Machine,
        Building,
        NPC,
        Cellectible
    }

    protected virtual void HighlightObj()
    {
        if (objectRanderer != null && !isHightlighted)
        {
            objectRanderer.material.color = highlightColor;
            objectRanderer.material.SetFloat("_Emission",highlightIntensity);
            isHightlighted = true;
        }
    }

    protected virtual void Start()
    {
        objectRanderer = GetComponent<Renderer>();
        if(objectRanderer != null )
        {
            originalColor = objectRanderer.material.color;
        }
        gameObject.layer = 8; 
    }

    protected virtual void RemoveHighlight()
    {
        if (objectRanderer != null && !isHightlighted)
        {
            objectRanderer.material.color = originalColor;
            objectRanderer.material.SetFloat("_Emission", 0f);
            isHightlighted = false;
        }
    }

    public virtual void OnPlayerEnter()
    {
        Debug.Log($"[{objName}]������");
        HighlightObj();
    }

    public virtual void OnPlayerExit()
    {
        Debug.Log($"[{objName}]�������� ���");
        RemoveHighlight();
    }

    protected virtual void CollectItem()
    {
        Destroy(gameObject);
    }

    protected virtual void OperateMachine()
    {
        if (objectRanderer != null) objectRanderer.material.color = Color.green;
    }

    protected virtual void AccessBuilding()
    {
        transform.Rotate(Vector3.up * 90);
    }

    protected virtual void TalkToNPC()
    {
        Debug.Log($"{objName}�� ��ȭ�� ���� �մϴ�.");
    }

    public virtual void Interact()
    {
        switch(interactionType)
        {
            case InteractionType.Item:
                CollectItem();
                break;
            case InteractionType.Machine:
                OperateMachine();
                break;
            case InteractionType.Building:
                AccessBuilding();
                break;
            case InteractionType.Cellectible:
                TalkToNPC();
                break;
        }
    }

    public string GetInteractionText()
    {
        return interactionText;
    }
}
