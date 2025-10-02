using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractionObj
{
    [Header("���� ����")]
    public int coinValue = 1;
    public string questTag = "Coin";

    protected override void Start()
    {
        base.Start();
        objName = "����";
        interactionText = "[E] ���� ȹ��";
        interactionType = InteractionType.Item;
    }

    protected override void CollectItem()
    {
        if(QuestManager.instance != null)
        {
            QuestManager.instance.AddCollectProgress(questTag);
        }
        Destroy(gameObject);
    }
}
