using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractionObj
{
    [Header("문 설정")]
    public bool isOpen = false;
    public Vector3 openPosition;
    public float openSpeed = 2f;

    private Vector3 closedPosition;

    protected override void Start()
    {
        base.Start();
        objName = "문";
        interactionText = "[E]문 열기";
        interactionType= InteractionType.Building;

        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.right * 3;
    }

    protected override void AccessBuilding()
    {
        isOpen=! isOpen;
        if(isOpen)
        {
            interactionText = "[E] 문 닫기";
            StartCoroutine(MoveDoor(closedPosition));
        }
        else
        {
            interactionText = "[E] 문 열기";
            StartCoroutine(MoveDoor(openPosition));
        }
    }

    IEnumerator MoveDoor(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, openSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
