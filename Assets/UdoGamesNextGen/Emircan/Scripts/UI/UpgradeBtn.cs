using System.Collections;
using System.Collections.Generic;
using MelonMath;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBtn : MonoBehaviour
{
    [SerializeField] private Button _buyBtn;
    [SerializeField] private TextMeshProUGUI _upgradeNameTMP;
    [SerializeField] private TextMeshProUGUI _upgradeDescriptionTMP;
    [SerializeField] private TextMeshProUGUI _upgradePriceTMP;
    [SerializeField] private TextMeshProUGUI _upgradeLevelTMP;
    [SerializeField] private Image _upgradeIcon;
    [SerializeField] private GameObject _shadow;

    private UpgradeBaseSO _upgradeBaseSO;

    public void InitUpgrade(UpgradeBaseSO upgradeBaseSO)
    {
        this._upgradeBaseSO = upgradeBaseSO;

        _upgradeNameTMP.SetText(_upgradeBaseSO.upgradeName);
        _upgradeIcon.sprite = _upgradeBaseSO.icon;
        _buyBtn.onClick.AddListener(OnUpgradeBuy);
    }

    private void OnEnable() {
        if (_upgradeBaseSO)
        {
            UpdateVisual();
        }
    }

    private void OnUpgradeBuy()
    {
        _upgradeBaseSO.ReducePrice();
        _upgradeBaseSO.Upgrade();
        UpdateVisual();
        UpgradeManager.Instance.UpdateAllVisual();
        _upgradeBaseSO.Save();
    }

    public void UpdateVisual()
    {
        _upgradeLevelTMP.SetText("lv." + _upgradeBaseSO.level.ToString());
        if (_upgradeBaseSO.CanLevelUpMore())
        {
            if (_upgradeBaseSO.CanPlayerBuy())
            {
                /* buyBtn.gameObject.SetActive(true);
                watchAdBtn.gameObject.SetActive(false); */
                _shadow.SetActive(false);
            }
            else
            {
                _shadow.SetActive(true);
                /* buyBtn.gameObject.SetActive(false);
                watchAdBtn.gameObject.SetActive(true); */
            }
            _upgradePriceTMP.SetText(NumberConverter.ConvertToString(_upgradeBaseSO.Price));
            _upgradeDescriptionTMP.SetText(_upgradeBaseSO.CurrentValue() + " -> " + _upgradeBaseSO.NextLevelValue());
        }
        else
        {
            _upgradeDescriptionTMP.gameObject.SetActive(false);
            _buyBtn.gameObject.SetActive(false);
            _shadow.SetActive(true);
        }

    }
}
