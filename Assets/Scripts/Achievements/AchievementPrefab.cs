using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IgnuxNex.SpaceConqueror
{
    public class AchievementPrefab : MonoBehaviour
    {
        [SerializeField] private Image sprite;
        [SerializeField] private TextMeshProUGUI achievementName;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI amount;
        [SerializeField] private bool isCompleted;
        [SerializeField] private Image notCompletedImage;
        [SerializeField] private Image completedImage;
        [SerializeField] private Image slider;
        [SerializeField] private Button rewardButton;
        [SerializeField] private Image lockedImage;
        private Achievement achievement;

        public string Id = "-1";

        [SerializeField] private TextMeshProUGUI reward;

        private void OnEnable() {
            if (achievement) SetAchievement(achievement);
        }

        public void SetAchievement(Achievement achievement)
        {
            this.achievement = achievement;
            sprite.sprite = achievement.icon;
            achievementName.text = achievement.achievementName;
            description.text = achievement.description;
            isCompleted = achievement.isCompleted;
            reward.text = achievement.rewardAmount.ToString();
            notCompletedImage.gameObject.SetActive(!isCompleted);
            completedImage.gameObject.SetActive(achievement.isClaimed);
            lockedImage.gameObject.SetActive(achievement.isLocked);

            float progress = achievement.isLocked ? 0 : achievement.achievementProgress.Value;
            progress = achievement.isClaimed ? achievement.neededAmount : progress;
            
            amount.text = $"{progress}/{achievement.neededAmount}";
            slider.fillAmount = progress / achievement.neededAmount;
            completedImage.enabled = achievement.isClaimed;
            Id = achievement.AchievementId;

            if (isCompleted)
            {
                notCompletedImage.enabled = false;
                if (rewardButton.onClick.GetPersistentEventCount() == 0)
                {
                    rewardButton.onClick.AddListener(() =>
                    {
                        AchievementController.Instance.ClaimReward(achievement);
                        achievement.isClaimed = true;
                        AchievementController.Instance.achievementUi.UpdateUnclaimedAchievementCount();
                        AchievementController.Instance.achievementUi.ReloadAchievement(achievement);
                        achievement.UnlockNextAchievements();
                    });
                    // rewardButton.onClick.AddListener(() => completedImage.enabled = true);
                }
            }
        }
    }
}