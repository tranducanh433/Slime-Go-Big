using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySystem.Inventory
{
    [System.Serializable]
    public class SlotBase
    {
        [SerializeField] ItemBase item;
        [SerializeField] int amount;

        /// <summary>
        /// Get Item of the Slot.
        /// </summary>
        public ItemBase getItem
        {
            get
            {
                return item;
            }
        }

        /// <summary>
        /// Get Amount of Item of the Slot.
        /// </summary>
        /// <returns></returns>
        public int getAmount
        {
            get
            {
                return amount;
            }
        }

        public SlotBase() 
        {
            item = null;
            amount = 0;
        }
        public SlotBase(ItemBase item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        /// <summary>
        /// Add Amount of Item to Slot and return the remain amount of Item if bigger than Item's Max Stack.
        /// </summary>
        /// <param name="addAmount">Amount to add.</param>
        /// <returns></returns>
        public int Add(int addAmount)
        {
            int maxStack = item.GetMaxStack();
            if(maxStack != -1)
            {
                amount += addAmount;

                if(amount > maxStack)
                {
                    int remainAmount = amount - maxStack;
                    amount = maxStack;
                    return remainAmount;
                }
            }
            return 0;
        }

        /// <summary>
        /// Remove amount of item.
        /// </summary>
        /// <param name="amount">Remove amount.</param>
        /// <returns>Return amount left if Amount(input) is greater than Amount(Amount in Slot)</returns>
        public int RemoveAmount(int amount)
        {
            if(this.amount > amount)
            {
                this.amount -= amount;
                return 0;
            }
            else if(this.amount < amount)
            {
                int amountLeft = amount - this.amount;
                Clear();
                return amountLeft;
            }
            else
            {
                Clear();
                return 0;
            }
        }

        /// <summary>
        /// Set Item and Amount data and return the remain amount of Item if bigger thanh Item's Max Stack.
        /// </summary>
        /// <param name="item">Item to set.</param>
        /// <param name="amount">Amount to set.</param>
        /// <returns>Return Amount left if Amount is greater than Max Stack of Item.</returns>
        public int SetSlotData(ItemBase item, int amount)
        {
            this.item = item;
            this.amount = amount;

            if (item == null) return 0;

            if(amount > item.GetMaxStack())
            {
                this.amount = item.GetMaxStack();
                return amount - item.GetMaxStack();
            }
            return 0;
        }

        /// <summary>
        /// Clear all information about this Slot.
        /// </summary>
        public void Clear()
        {
            item = null;
            amount = 0;
        }

        /// <summary>
        /// Check the Slot is full or not.
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            return amount == item.GetMaxStack();
        }
    }
}
