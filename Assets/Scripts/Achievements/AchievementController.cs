using System.Collections.Generic;
using UdoGames.NextGenDev;
using UnityEngine;

namespace IgnuxNex.SpaceConqueror
{
    public class AchievementController : MonoBehaviour
    {
        public static AchievementController Instance { get; private set; }

        public AchievementUi achievementUi { get; private set; }
        public List<Achievement> achievements;

        public Int collectedItem;
        public Int roadTravelled;

        private void Awake()
        {
            Instance = this;
            achievementUi = GetComponent<AchievementUi>();
        }
        
        public void ClaimReward(Achievement achievement)
        {
            CurrencyManager.Instance.AddGold(achievement.rewardAmount, "Achievement " + achievement.achievementName + " reward.");
        }

        public void CalculateCollectedAchievement(int itemCount)
        {
            collectedItem.Value += itemCount;
        }

        public void CalculateTravellerAchievement(int value)
        {
            roadTravelled.Value += value;
        }
        
        
        private void OnEnable()
        {
            foreach (var ach in achievements)
            {
                if (ach.isLocked) continue;
                    
                ach.achievementProgress.OnValueChanged += ach.AddProgress;
                ach.LoadProgress();
            }
        }

        public void SetNextAchievement(Achievement ach)
        {
            ach.achievementProgress.OnValueChanged += ach.AddProgress;
            ach.achievementProgress.Value = 0;
            ach.LoadProgress();
        }

        private void OnDisable()
        {
            foreach (var ach in achievements)
            {
                ach.achievementProgress.OnValueChanged -= ach.AddProgress;
            }
        }
    }
}