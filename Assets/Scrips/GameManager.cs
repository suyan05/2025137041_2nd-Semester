using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("개임 상태")]
    public int PlayerScore = 0;
    public int ItemsColleted = 0;

    [Header("UI 참조")]
    public Text ScoreText;
    public Text ItemCountText;
    public Text gameStatusText;

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem()
    {
        ItemsColleted++;
        Debug.Log($"아이템 수집!(총:{ItemsColleted}개");
    }

    public void UpdateUI()
    {
        if(ScoreText != null)
        {
            ScoreText.text = "점수 : " + PlayerScore;
        }

        if (ItemCountText != null)
        {
            ItemCountText.text = "아이템 : " + ItemsColleted + "개";
        }
    }
}
