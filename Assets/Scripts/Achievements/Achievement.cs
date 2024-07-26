using System.Collections.Generic;
using UdoGames.NextGenDev;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace IgnuxNex.SpaceConqueror
{
    [CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
    public class Achievement : ScriptableObject
    {
        public string achievementName;
        public string description;
        public Int achievementProgress;
        public int neededAmount;
        public bool isCompleted;
        public bool isClaimed;
        public bool isLocked;
        public bool IsFirstAch;
        public Sprite icon;

        public int rewardAmount;

        public List<Achievement> nextAchievements;
        public string AchievementId => "ach_" + achievementName.Replace(" ", "_").ToLower();
        public float Progress => (float)achievementProgress.Value / neededAmount;


        public void LoadProgress()
        {
            achievementProgress.Value = PlayerPrefs.GetInt(AchievementId, 0);
            isCompleted = PlayerPrefs.GetInt("completed_" + AchievementId, 0) == 1;
            isClaimed = PlayerPrefs.GetInt("claimed" + AchievementId, 0) == 1;
            /* if (achievementProgress.Value == 0)
            {
                isCompleted = false;
                isClaimed = false;
            } */
            AchievementController.Instance.achievementUi.UpdateUnclaimedAchievementCount();
        }


        public void AddProgress(int amount)
        {
            if (isLocked || isClaimed) return;
            PlayerPrefs.SetInt(AchievementId, achievementProgress.Value);

            if (achievementProgress.Value >= neededAmount)
                isCompleted = true;

            if (isCompleted)
            {
                PlayerPrefs.SetInt("completed_" + AchievementId, 1);
                AchievementController.Instance.achievementUi.UpdateUnclaimedAchievementCount();
            }
        }

        public void UnlockNextAchievements()
        {
            if (nextAchievements == null || nextAchievements.Count == 0) return;
            achievementProgress.Value = 0;
            PlayerPrefs.SetInt(AchievementId, achievementProgress.Value);

            foreach (var ach in nextAchievements)
            {
                ach.isLocked = false;
                AchievementController.Instance.achievementUi.ReloadAchievement(ach);
                AchievementController.Instance.SetNextAchievement(ach);
            }
        }
    }

}