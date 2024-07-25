using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    [CreateAssetMenu(menuName = "Upgrades/Int Upgrade")]
    public class UpgradeIntSO : UpgradeBaseSO
    {
        public Int valueToUpgrade;
        public int[] values;

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