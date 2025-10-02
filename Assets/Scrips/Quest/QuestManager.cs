using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("UI References")]
    public GameObject questPanel; // ����Ʈ �г�
    public Text questTitleText; // ����Ʈ ���� �ؽ�Ʈ
    public Text questDescriptionText; // ����Ʈ ���� �ؽ�Ʈ
    public Text questProgressText; // ����Ʈ ���� �ؽ�Ʈ
    public Button completeQuestButton; // ����Ʈ �Ϸ� ��ư

    [Header("Quest Data")]
    public QuestData[] availableQuests; // ��� ������ ����Ʈ ���

    private QuestData currentQuests; // ���� Ȱ��ȭ�� ����Ʈ
    private int currentQuestIndex = 0; // ���� Ȱ��ȭ�� ����Ʈ �ε���

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(availableQuests.Length > 0)
        {
            StartQuste(availableQuests[0]);
        }
        if(completeQuestButton != null)
        {
            completeQuestButton.onClick.AddListener(CompleteCurrentQuest);
        }
    }

    private void Update()
    {
        if(currentQuests != null && currentQuests.isActive)
        {
            CheckQuestProgress();
            UpdateQuestUI();
        }
    }

    //UI ������Ʈ (Queste ���� ��Ȳ UI ǥ��)
    private void UpdateQuestUI()
    {
        if (instance == null) return;

        if(questTitleText != null)
        {
            questTitleText.text = currentQuests.questTitle;
        }

        if(questDescriptionText != null)
        {
            questDescriptionText.text = currentQuests.questDescription;
        }

        if(questProgressText != null)
        {
            questProgressText.text = currentQuests.GetProgressText();
        }
    }

    //����Ʈ ����
    public void StartQuste(QuestData data)
    {
        if (data == null)
        {
            Debug.LogWarning("Quest data is null.");
            return;
        }
        currentQuests = data;
        currentQuests.isActive = true;
        currentQuests.InitializeQuest();

        Debug.Log("Quest Started: " + questTitleText);
        UpdateQuestUI();
        questPanel.SetActive(true);

        if(questPanel != null)
        {
            questPanel.SetActive(true);
        }
    }

    //��� ����Ʈ ���� ��Ȳ
    private void CheckDeliveryProgress()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        float distance = Vector3.Distance(player.position, currentQuests.deliveryLocation);

        if(distance <= currentQuests.deliveryRadius)
        {
            if(currentQuests.currentAmount==0)
            {
                currentQuests.currentAmount = 1;
            }
            else
            {
                currentQuests.currentAmount = 0;
            }
        }
    }

    //���� ����Ʈ ���� (�ܺ� ȣ��)
    public void AddCollectProgress(string ObjTag)
    {
        if (currentQuests == null || !currentQuests.isActive)
        {
            return;
        }

        if (currentQuests.questType != QuestType.Collect && currentQuests.questTitle == ObjTag)
        {
            currentQuests.currentAmount++;
            Debug.Log("������ ����: " + ObjTag);
        }
    }
    
    //��ȣ�ۿ� ����Ʈ ���� (�ܺ� ȣ��)
    public void AddIntaractProgress(string ObjTag)
    {
        if (currentQuests == null || !currentQuests.isActive)
        {
            return;
        }

        if (currentQuests.questType != QuestType.Interect && currentQuests.questTitle == ObjTag)
        {
            currentQuests.currentAmount++;
            Debug.Log("��ȣ �ۿ� �Ϸ�: " + ObjTag);
        }
    }

    //���� ����Ʈ �Ϸ�
    public void CompleteCurrentQuest()
    {
        if (currentQuests == null || !currentQuests.isActive)
        {
            Debug.LogWarning("No active quest to complete.");
            return;
        }

        //�Ϸ� ��ư ��Ȱ��ȭ
        if(completeQuestButton != null)
        {
            completeQuestButton.gameObject.SetActive(false);
        }

        //���� ����Ʈ�� ������ ����
        currentQuestIndex++;
        if(currentQuestIndex < availableQuests.Length)
        {
            StartQuste(availableQuests[currentQuestIndex]);
        }
        else
        {
            currentQuests = null;
            if(questPanel != null)
            {
                questPanel.SetActive(false);
            }
        }
    }

    //����Ʈ ���� üũ
    private void CheckQuestProgress()
    {
        if(currentQuests.questType == QuestType.Delivery)
        {
            CheckDeliveryProgress();
        }

        if(currentQuests.IsCompleted()&&!currentQuests.isCompleted)
        {
            completeQuestButton.gameObject.SetActive(true);
        }
    }
}


