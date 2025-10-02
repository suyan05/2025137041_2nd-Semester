using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    [Header("기본정보")]
    public string questTitle; // 퀘스트 제목

    [TextArea(2,4)]
    public string questDescription; // 퀘스트 설명
    public Sprite questIcon; // 퀘스트 아이콘

    [Header("퀘스트 목표")]
    public QuestType questType; // 퀘스트 타입
    public int targetAmount; // 목표 수량

    [Header("배달 퀘스트")]
    public Vector3 deliveryLocation; // 배달 위치
    public float deliveryRadius; // 배달 반경

    [Header("수집 퀘스트")]
    public string itemToCollect; // 수집할 아이템 이름

    [Header("보상")]
    public int rewardGold; // 보상 골드
    public string rewardItem; // 보상 아이템 이름

    [Header("퀘스트 연결")]
    public QuestData nextQuest; // 다음 퀘스트

    //런타임 데이터(저징되지 않음)
    [System.NonSerialized] public int currentAmount; // 현재 진행 수량
    [System.NonSerialized] public bool isCompleted; // 퀘스트 완료 여부
    [System.NonSerialized] public bool isActive; // 퀘스트 활성화 여부

    // 퀘스트 초기화 메서드
    public void InitializeQuest()
    {
        currentAmount = 0;
        isCompleted = false;
        isActive = false;
    }

    //퀘스트 완료 메서드
    public bool IsCompleted()
    {
        switch(questType)
        {
            case QuestType.Delivery:
                return currentAmount >= 1; // 배달 퀘스트는 isCompleted로 판단
            case QuestType.Collect:
            case QuestType.Interect:
                return currentAmount >= targetAmount; // 수집 퀘스트는 수집 수량으로 판단
            default:
                return false;
        }
    }

    //퀘스트 진행률 퍼센티지 반환 메서드
    public float GetProgressPercentage()
    {
        if(targetAmount <= 0) return 0f;
        return Mathf.Clamp01((float)currentAmount / targetAmount);
    }

    //퀘스트 진행 상황 텍스트
    public string GetProgressText()
    {
        switch (questType)
        {
            case QuestType.Delivery:
                return isCompleted ? "배달 완료" : "배달 대기중";
            case QuestType.Collect:
            case QuestType.Interect:
                return $"{currentAmount} / {targetAmount}";
            default:
                return "";
        }
    }


}
