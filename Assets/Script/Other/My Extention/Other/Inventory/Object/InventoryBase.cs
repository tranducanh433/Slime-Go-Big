using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySystem.Crafting;

namespace MySystem.Inventory
{
    [System.Serializable]
    public class InventoryBase
    {
        [SerializeField] SlotBase[] slots;

        public InventoryBase(int length)
        {
            slots = new SlotBase[length];
        }

        /// <summary>
        /// Get all slots of the Inventory
        /// </summary>
        /// <returns></returns>
        public SlotBase[] GetAllSlots()
        {
            return slots;
        }

        /// <summary>
        /// Add Item to Inventory.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <param name="amount">Amount add.</param>
        /// <param name="updateSlotFunc">Func(int i) to update a slot element.</param>
        /// <returns>Return amount left if Inventory if Full.</returns>
        public int AddItem(ItemBase item, int amount, System.Action<int> updateSlotFunc)
        {
            int amountLeft = amount;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].getItem == item && slots[i].IsFull() == false)
                {
                    amountLeft = slots[i].Add(amountLeft);
                    updateSlotFunc(i);

                    if (amountLeft == 0) return 0;
                }
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].getItem == null)
                {
                    amountLeft = slots[i].SetSlotData(item, amount);
                    updateSlotFunc(i);

                    if (amountLeft == 0) return 0;
                }
            }

            return amountLeft;
        }

        /// <summary>
        /// Remove amount of Item in inventory.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <param name="amount">Amount to remove.</param>
        /// <param name="updateSlotFunc">Func(int i) to update a slot element.</param>
        /// <returns>Return False if don't have enough Amount of Item in Inventory</returns>
        public bool RemoveItem(ItemBase item, int amount, System.Action<int> updateSlotFunc)
        {
            if (AmoutOfItem(item) < amount)
                return false;

            int amountleft = amount;
            for (int i = 0; i < slots.Length; i++)
            {
                if(slots[i].getItem == item)
                {
                    amountleft = slots[i].RemoveAmount(amountleft);
                    updateSlotFunc(i);

                    if (amountleft == 0) break;
                }
            }
            return true;
        }

        /// <summary>
        /// Get a slot by Element.
        /// </summary>
        /// <param name="i">Element.</param>
        /// <returns></returns>
        public SlotBase GetSlot(int i)
        {
            return slots[i];
        }

        /// <summary>
        /// Clear a slot by Element.
        /// </summary>
        /// <param name="i">Element.</param>
        public void ClearSlot(int i)
        {
            slots[i].Clear(); 
        }

        /// <summary>
        /// Check if Inventory have enough item to craft Item.
        /// </summary>
        /// <param name="recipe">Craft Recipe to craft Item.</param>
        /// <returns>Return true if have enough Item.</returns>
        public bool CanCraft(CraftingRecipe recipe)
        {
            CraftMaterial[] materials = recipe.Materials;

            foreach (CraftMaterial material in materials)
            {
                if (AmoutOfItem(material.item) < material.amount)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Get the total amount of a Item in Inventory.
        /// </summary>
        /// <param name="item">Item to count.</param>
        /// <returns></returns>
        int AmoutOfItem(ItemBase item)
        {
            int amount = 0;

            foreach (SlotBase slot in slots)
            {
                if (slot.getItem == item)
                    amount += slot.getAmount;
            }

            return amount;
        }

        /// <summary>
        /// Set Item and Amonnt to a Slot.
        /// </summary>
        /// <param name="item">Item to set.</param>
        /// <param name="amount">Amount to set.</param>
        /// <param name="i">Element of Slot.</param>
        public void SetItem(ItemBase item, int amount, int i)
        {
            slots[i].SetSlotData(item, amount);
        }
    }
}

