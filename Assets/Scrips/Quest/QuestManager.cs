using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("UI References")]
    public GameObject questPanel; // 퀘스트 패널
    public Text questTitleText; // 퀘스트 제목 텍스트
    public Text questDescriptionText; // 퀘스트 설명 텍스트
    public Text questProgressText; // 퀘스트 진행 텍스트
    public Button completeQuestButton; // 퀘스트 완료 버튼

    [Header("Quest Data")]
    public QuestData[] availableQuests; // 사용 가능한 퀘스트 목록

    private QuestData currentQuests; // 현재 활성화된 퀘스트
    private int currentQuestIndex = 0; // 현재 활성화된 퀘스트 인덱스

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

    //UI 업데이트 (Queste 진행 상황 UI 표시)
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

    //퀘스트 시작
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

    //배달 퀘스트 진행 상황
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

    //수집 퀘스트 진행 (외부 호출)
    public void AddCollectProgress(string ObjTag)
    {
        if (currentQuests == null || !currentQuests.isActive)
        {
            return;
        }

        if (currentQuests.questType != QuestType.Collect && currentQuests.questTitle == ObjTag)
        {
            currentQuests.currentAmount++;
            Debug.Log("아이템 수집: " + ObjTag);
        }
    }
    
    //상호작용 퀘스트 진행 (외부 호출)
    public void AddIntaractProgress(string ObjTag)
    {
        if (currentQuests == null || !currentQuests.isActive)
        {
            return;
        }

        if (currentQuests.questType != QuestType.Interect && currentQuests.questTitle == ObjTag)
        {
            currentQuests.currentAmount++;
            Debug.Log("상호 작용 완료: " + ObjTag);
        }
    }

    //현제 퀘스트 완료
    public void CompleteCurrentQuest()
    {
        if (currentQuests == null || !currentQuests.isActive)
        {
            Debug.LogWarning("No active quest to complete.");
            return;
        }

        //완료 버튼 비활성화
        if(completeQuestButton != null)
        {
            completeQuestButton.gameObject.SetActive(false);
        }

        //다음 퀘스트가 있으면 시작
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

    //퀘스트 진행 체크
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


