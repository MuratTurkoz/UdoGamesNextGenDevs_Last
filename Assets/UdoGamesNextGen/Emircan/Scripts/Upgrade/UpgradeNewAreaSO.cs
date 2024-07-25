using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/New Area Upgrade")]
public class UpgradeNewAreaSO : UpgradeBaseSO
{
    public override string CurrentValue()
    {
        return "Area 1";
    }

    public override string NextLevelValue()
    {
        return "Area 2";
    }

    public override void Load()
    {
        level = 1;
        for (int i = 0; i < AreaManager.Instance.AreaLocks.Length; i++)
        {
            AreaManager.Instance.AreaLocks[i].SetActive(i <= (level - 2));
        }
    }

    public override void Upgrade()
    {
        level++;
        AreaManager.Instance.AreaLocks[level - 2].SetActive(false);
    }
}
