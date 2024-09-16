using MySystem.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySystem.Looting
{
    [System.Serializable]
    public class AmountLoot
    {
        [SerializeField] ItemBase item;
        [SerializeField] int minAmount = 1;
        [SerializeField] int maxAmount = 10;

        /// <summary>
        /// Get the Item from Loot.
        /// </summary>
        /// <returns></returns>
        public ItemBase GetItem()
        {
            return item;
        }

        /// <summary>
        /// Get amount of Item with random Amount.
        /// </summary>
        /// <returns></returns>
        public ItemBase[] GetItemByChance()
        {
            List<ItemBase> result = new List<ItemBase>();
            int r = Random.Range(minAmount, maxAmount + 1);
            for (int i = 0; i < r; i++)
            {
                result.Add(item);
            }

            return result.ToArray();
        }
    }
}
