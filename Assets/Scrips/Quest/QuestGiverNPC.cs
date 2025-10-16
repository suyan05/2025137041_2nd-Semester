using UnityEngine;

public class QuestGiverNPC : InteractionObj
{
    [Header("NPC Quest setting")]
    public QuestData questToGive;
    public string npcName = "NPC";
    public string questStartMessage = "새로운 퀘스트가 있습니다.";
    public string noQuestMessage = "퀘스트가 없습니다.";
    public string QuestAlreadyActiveMessage = "이미 진행중니 퀘스트가 있습니다.";

    private QuestManager QuestManager;

    protected override void Start()
    {
        base.Start();

        QuestManager = FindAnyObjectByType<QuestManager>();

        if (QuestManager == null )
        {
            Debug.LogError("QuestManager가 없습니다.");
        }

        interactionText = "[E]" + npcName + "와 대화하기";
    }

    public override void Interact()
    {
        base.Interact();
        QuestManager.StartQuste(questToGive);
    }
}
