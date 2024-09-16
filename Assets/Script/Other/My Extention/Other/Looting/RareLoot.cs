using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySystem.Inventory;

namespace MySystem.Looting
{
    [System.Serializable]
    public class RareLoot
    {
        [SerializeField] ItemBase item;
        [SerializeField] int dropRate = 100;

        /// <summary>
        /// Get the Item from Loot.
        /// </summary>
        /// <returns></returns>
        public ItemBase Item { get { return item; } }

        /// <summary>
        /// Get Item depend on Drop Rate of LootBase.
        /// It will return null if not in Drop Rate.
        /// </summary>
        /// <returns></returns>
        public virtual ItemBase GetItemByChance()
        {
            return Random.Range(1, 101) <= dropRate ? item : null;
        }
    }
}

