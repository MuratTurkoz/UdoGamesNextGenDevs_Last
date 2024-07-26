using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IgnuxNex.SpaceConqueror
{
    public class AchievementUi : MonoBehaviour
    {
        [SerializeField] private GameObject achievementPrefab;

        [SerializeField] private Button openButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject achievementPanel;
        [SerializeField] private GameObject achievementContentPanel;
        
        private AchievementController achievementController;
        private List<AchievementPrefab> currentAchievements;
        private int unclaimedAchievementCount;
        [SerializeField] private TextMeshProUGUI unclaimedAchievementCountText;
        
        
        private void Awake()
        {
            achievementController = GetComponent<AchievementController>();
            
            openButton.onClick.AddListener(HandlePanel);
            openButton.onClick.AddListener(GetAchievements);
            
            closeButton.onClick.AddListener(HandlePanel);
            closeButton.onClick.AddListener(ClearAchievements);
        }

        private void HandlePanel()
        {
            achievementPanel.SetActive(!achievementPanel.activeSelf);
        }
        
        public void AddAchievement(Achievement achievement)
        {
            currentAchievements ??= new List<AchievementPrefab>();
            
            var achievementGo = Instantiate(achievementPrefab, achievementContentPanel.transform);
            achievementGo.transform.SetParent(achievementContentPanel.transform);
            var achievementEdit = achievementGo.GetComponent<AchievementPrefab>();
            achievementEdit.SetAchievement(achievement);
            currentAchievements.Add(achievementEdit);
        }

        public void ReloadAchievement(Achievement achievement)
        {
            var ach = currentAchievements.Find(x => x.Id == achievement.AchievementId);
            if (ach == null) return;
            
            ach.SetAchievement(achievement);
        }

        private void GetAchievements()
        {
            foreach (var ach in achievementController.achievements)
            {
                AddAchievement(ach);
            }
        }
        
        private void ClearAchievements()
        {
            foreach (Transform child in achievementContentPanel.transform)
                Destroy(child.gameObject);
            
            currentAchievements.Clear();
            currentAchievements = null;
        }
        
        public void UpdateUnclaimedAchievementCount()
        {
            unclaimedAchievementCount = 0;
            foreach (var ach in achievementController.achievements)
            {
                if (ach.isCompleted && !ach.isClaimed)
                    unclaimedAchievementCount++;
            }

            unclaimedAchievementCountText.text = unclaimedAchievementCount.ToString();
        }
    }
}