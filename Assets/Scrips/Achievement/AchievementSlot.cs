using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;
    public Text nameText;
    public Text descriptionText;
    public Text ProgressText;
    public Slider PorGressSlider;

    public void SetAchievement(AchievementData achievement, float progress)
    {
        if (nameText != null)
            nameText.text = achievement.achievementName;
        if (descriptionText != null)
            descriptionText.text = achievement.description;
        if (iconImage != null)
            iconImage.sprite = achievement.icon;
        if (PorGressSlider != null)
            PorGressSlider.value = achievement.isUnlocked ? 1f : progress;
        if(ProgressText != null)
            if(achievement.isUnlocked)
                ProgressText.text = "¿Ï·á";
            else
            {
                int current = Mathf.FloorToInt(progress * achievement.requiredAmount);
                ProgressText.text = current + "/" + achievement.requiredAmount;
            }
    }
}
