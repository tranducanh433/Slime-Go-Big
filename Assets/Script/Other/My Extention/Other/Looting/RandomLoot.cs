using MySystem.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySystem.Looting
{
    [System.Serializable]
    public class RandomLoot
    {
        [SerializeField] ItemBase[] itemsCanDrop;

        /// <summary>
        /// Get all Items from Loot.
        /// </summary>
        /// <returns></returns>
        public ItemBase[] ItemsCanDrop { get { return itemsCanDrop; } }

        /// <summary>
        /// Get amount of Item depend on Drop Rate of LootBase.
        /// </summary>
        /// <returns></returns>
        public ItemBase GetItemByChance()
        {
            return itemsCanDrop[Random.Range(0, itemsCanDrop.Length)];
        }
    }
}
