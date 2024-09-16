using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySystem.Inventory;
using System.Linq;

namespace MySystem.Looting
{
    [CreateAssetMenu(fileName = "Looting Table", menuName = "My System/Lotting/Loots table")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] RareLoot[] rareLoots;
        [SerializeField] AmountLoot[] amountLoots;
        [SerializeField] RandomLoot[] randomLoots;

        /*/// <summary>
        /// Get all loots information.
        /// </summary>
        /// <returns></returns>
        public RareLoot[] GetLootTable()
        {
            return loots;
        }*/

        /// <summary>
        /// Get all the Item that player can get.
        /// </summary>
        /// <returns></returns>
        public ItemBase[] ItemsCanGet()
        {
            HashSet<ItemBase> result = new HashSet<ItemBase>();
            foreach (RareLoot loot in rareLoots)
            {
                result.Add(loot.Item);
            }
            foreach (AmountLoot loot in amountLoots)
            {
                result.Add(loot.GetItem());
            }
            foreach (RandomLoot loot in randomLoots)
            {
                ItemBase[] lootItem = loot.ItemsCanDrop;
                foreach (ItemBase item in lootItem)
                {
                    result.Add(item);
                }
            }
            return result.ToArray();
        }


        /// <summary>
        /// Get List of Items depend on Item's Drop Rate.
        /// </summary>
        /// <returns></returns>
        public virtual ItemBase[] GetLootsbyChance()
        {
            List<ItemBase> result = new List<ItemBase>();

            foreach (RareLoot loot in rareLoots)
            {
                ItemBase itemsGet = loot.GetItemByChance();
                if (itemsGet != null)
                {
                    result.Add(itemsGet);
                }
            }

            foreach (AmountLoot loot in amountLoots)
            {
                ItemBase[] itemsGet = loot.GetItemByChance();
                if (itemsGet != null)
                {
                    foreach (ItemBase item in itemsGet)
                    {
                        result.Add(item);
                    }
                }
            }

            foreach (RandomLoot loot in randomLoots)
            {
                ItemBase itemsGet = loot.GetItemByChance();
                if (itemsGet != null)
                {
                    result.Add(itemsGet);
                }
            }

            return result.ToArray();
        }
    }
}
