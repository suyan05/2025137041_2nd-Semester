using UnityEngine;


[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement/AchievementData")]
public class AchievementData : ScriptableObject
{
    public string achievementName;
    public string description;
    public AchievementType achievementType;
    public int requiredAmount;
    public int rewardCoins;
    public bool isUnlocked;
    public Sprite icon;
}
