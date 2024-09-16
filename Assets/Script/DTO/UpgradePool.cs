using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UpgradePool
{
    [SerializeField] List<UpgradeData> pool;
    [SerializeField] List<UpgradeData> selectedUpgrade;

    public UpgradeData[] SelectedUpgrade { get { return selectedUpgrade.ToArray(); } }


    public UpgradeData[] GetRandomUpgrade(int amount)
    {
        if(pool.Count <= amount)
        {
            return pool.ToArray();
        }
        else
        {
            List<UpgradeData> rs = new List<UpgradeData>();

            for (int i = 0; i < amount; i++)
            {
                UpgradeData rand = pool[Random.Range(0, pool.Count)];
                pool.Remove(rand);
                rs.Add(rand);
            }

            foreach (UpgradeData upgradeData in rs)
            {
                pool.Add(upgradeData);
            }

            return rs.ToArray();
        }
    }

    public void RemoveFromPool(UpgradeData upgradeData)
    {
        UpgradeData nextUpgrade = upgradeData.nextUpgrade;

        pool.Remove(upgradeData);

        if(nextUpgrade != null)
        {
            pool.Add(nextUpgrade);
        }


        //Update Selected Upgrade
        for (int i = 0; i < selectedUpgrade.Count; i++)
        {
            if (selectedUpgrade[i].nextUpgrade == upgradeData)
            {
                selectedUpgrade.RemoveAt(i);
                break;
            }
        }
        selectedUpgrade.Add(upgradeData);
    }

    public bool IsEmpty()
    {
        return pool.Count == 0;
    }
}
