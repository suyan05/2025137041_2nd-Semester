using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    [Header("�⺻����")]
    public string questTitle; // ����Ʈ ����

    [TextArea(2,4)]
    public string questDescription; // ����Ʈ ����
    public Sprite questIcon; // ����Ʈ ������

    [Header("����Ʈ ��ǥ")]
    public QuestType questType; // ����Ʈ Ÿ��
    public int targetAmount; // ��ǥ ����

    [Header("��� ����Ʈ")]
    public Vector3 deliveryLocation; // ��� ��ġ
    public float deliveryRadius; // ��� �ݰ�

    [Header("���� ����Ʈ")]
    public string itemToCollect; // ������ ������ �̸�

    [Header("����")]
    public int rewardGold; // ���� ���
    public string rewardItem; // ���� ������ �̸�

    [Header("����Ʈ ����")]
    public QuestData nextQuest; // ���� ����Ʈ

    //��Ÿ�� ������(��¡���� ����)
    [System.NonSerialized] public int currentAmount; // ���� ���� ����
    [System.NonSerialized] public bool isCompleted; // ����Ʈ �Ϸ� ����
    [System.NonSerialized] public bool isActive; // ����Ʈ Ȱ��ȭ ����

    // ����Ʈ �ʱ�ȭ �޼���
    public void InitializeQuest()
    {
        currentAmount = 0;
        isCompleted = false;
        isActive = false;
    }

    //����Ʈ �Ϸ� �޼���
    public bool IsCompleted()
    {
        switch(questType)
        {
            case QuestType.Delivery:
                return currentAmount >= 1; // ��� ����Ʈ�� isCompleted�� �Ǵ�
            case QuestType.Collect:
            case QuestType.Interect:
                return currentAmount >= targetAmount; // ���� ����Ʈ�� ���� �������� �Ǵ�
            default:
                return false;
        }
    }

    //����Ʈ ����� �ۼ�Ƽ�� ��ȯ �޼���
    public float GetProgressPercentage()
    {
        if(targetAmount <= 0) return 0f;
        return Mathf.Clamp01((float)currentAmount / targetAmount);
    }

    //����Ʈ ���� ��Ȳ �ؽ�Ʈ
    public string GetProgressText()
    {
        switch (questType)
        {
            case QuestType.Delivery:
                return isCompleted ? "��� �Ϸ�" : "��� �����";
            case QuestType.Collect:
            case QuestType.Interect:
                return $"{currentAmount} / {targetAmount}";
            default:
                return "";
        }
    }


}
