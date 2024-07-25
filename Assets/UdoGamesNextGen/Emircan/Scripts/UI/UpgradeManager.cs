using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set;}

    [SerializeField] private Transform _upgradeListContentParent;
    [SerializeField] private UpgradeBtn _upgradeBtnPrefab;
    [SerializeField] private UpgradeBaseSO[] _upgrades;

    private List<UpgradeBtn> _upgradeBtns;

    private void Awake() {
        Instance = this;

        _upgradeBtns = new List<UpgradeBtn>();

        for (int i = 0; i < _upgrades.Length; i++)
        {
            var btn = Instantiate(_upgradeBtnPrefab, _upgradeListContentParent);
            btn.InitUpgrade(_upgrades[i]);
            _upgradeBtns.Add(btn);
        }
    }

    private void Start() {
        foreach (var upgrade in _upgrades)
        {
            upgrade.Load();
        }
    }

    public void UpdateAllVisual()
    {
        foreach (var btn in _upgradeBtns)
        {
            btn.UpdateVisual();
        }
    }
}
