using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("���� ����")]
    public int PlayerScore = 0;
    public int ItemsColleted = 0;

    [Header("UI ����")]
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
        Debug.Log($"������ ����!(��:{ItemsColleted}��");
    }

    public void UpdateUI()
    {
        if(ScoreText != null)
        {
            ScoreText.text = "���� : " + PlayerScore;
        }

        if (ItemCountText != null)
        {
            ItemCountText.text = "������ : " + ItemsColleted + "��";
        }
    }
}
