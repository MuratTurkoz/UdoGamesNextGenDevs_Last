using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class MoneyIconManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _moneyIcons;
    [SerializeField] private Transform _playerMoneyIcon;
    [SerializeField] private float _animationDuration = 1f;
    [SerializeField] private float _startPositionVariance = 50f;
    [SerializeField] private Transform _startPositionTransform;

    public void ShowMoneyIcons(Action onSequenceComplete)
    {
        Vector3 startPosition = _startPositionTransform.position;
        Sequence sequence = DOTween.Sequence();
        
        for (int i = 0; i < _moneyIcons.Length; i++)
        {
            GameObject moneyIcon = _moneyIcons[i];

            // Activate the money icon
            moneyIcon.SetActive(true);

            // Set the initial position near the given startPosition
            float randomX = UnityEngine.Random.Range(-_startPositionVariance, _startPositionVariance);
            float randomY = UnityEngine.Random.Range(-_startPositionVariance, _startPositionVariance);
            moneyIcon.transform.position = startPosition + new Vector3(randomX, randomY, 0);

            // Animate the icon to move to the player's money icon position
            float delay = UnityEngine.Random.Range(0f, 0.2f); // Add slight delay for each icon
            sequence.Insert(delay, moneyIcon.transform.DOMove(_playerMoneyIcon.position, _animationDuration).SetEase(Ease.InOutQuad));
        }

        // Optionally, deactivate icons after they reach the target position
        sequence.OnComplete(() =>
        {
            foreach (var icon in _moneyIcons)
            {
                icon.SetActive(false);
            }
            onSequenceComplete?.Invoke();
        });
    }
}
