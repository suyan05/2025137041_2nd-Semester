using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractionObj
{
    [Header("코인 설정")]
    public int coinValue = 1;
    public string questTag = "Coin";

    protected override void Start()
    {
        base.Start();
        objName = "코인";
        interactionText = "[E] 코인 획득";
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
