using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    [CreateAssetMenu(menuName = "Upgrades/Float Upgrade")]
    public class UpgradeFloatSO : UpgradeBaseSO
    {
        public Float valueToUpgrade;
        public float[] values;

        public override void Load()
        {
            level = PlayerPrefs.GetInt(upgradeName, 1);
            valueToUpgrade.Value = values[level - 1];
        }

        public override void Upgrade()
        {
            level++;
            valueToUpgrade.Value = values[level - 1];
        }

        public override string CurrentValue()
        {
            return values[level - 1].ToString();
        }
        public override string NextLevelValue()
        {
            return values[level].ToString();
        }
    }
}