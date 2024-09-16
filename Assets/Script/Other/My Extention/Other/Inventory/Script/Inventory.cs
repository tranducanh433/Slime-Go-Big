using MySystem.Crafting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySystem.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] protected GameObject inventoryUI;
        [SerializeField] protected InventoryBase inventory;
        [SerializeField] protected Slot[] slots;

        public InventoryBase inventoryBase { get { return inventory; } }

        /// <summary>
        /// Open Inventory UI.
        /// </summary>
        public void OpenInventory()
        {
            inventoryUI.SetActive(true);
            OnOpenInventory();
        }

        /// <summary>
        /// Close inventory UI.
        /// </summary>
        public void CloseInventory()
        {
            inventoryUI.SetActive(false);
            OnCloseInventory();
        }

        /// <summary>
        /// Switch On/Off of the Inventory UI.
        /// </summary>
        public void OpenNCloseinventory()
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            if (inventoryUI.activeSelf == true)
                OnOpenInventory();
            else
                OnCloseInventory();
        }

        /// <summary>
        /// Add Amount of Item to Inventory.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <param name="amount">Amount to add.</param>
        /// <returns>Return Amount left that can't add to Inventory.</returns>
        public int AddItem(ItemBase item, int amount)
        {
            int amountLeft = inventory.AddItem(item, amount, UpdateSlot);
            OnAddItem(item, amount - amountLeft);
            return amountLeft;
        }

        /// <summary>
        /// Remove Amount of Item in Inventory.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <param name="amount">Amount to remove.</param>
        /// <returns>Return true if have enough Amount of Item to add.</returns>
        public bool RemoveItem(ItemBase item, int amount)
        {
            bool canRemove = inventory.RemoveItem(item, amount, UpdateSlot);
            if(canRemove)
                OnRemoveItem(item, amount);
            return canRemove;
        }

        /// <summary>
        /// Update display a SLot by Element.
        /// </summary>
        /// <param name="i">Element.</param>
        void UpdateSlot(int i)
        {
            slots[i].UpdateDisplay();
        }

        /// <summary>
        /// Update display all Slots in Inventory.
        /// </summary>
        public void UpdateAllSlot()
        {
            foreach (Slot slot in slots)
            {
                slot.UpdateDisplay();
            }
        }

        /// <summary>
        /// Set Slot Data to all Slot in Inventory.
        /// </summary>
        public void SetData()
        {
            if(slots.Length != inventory.GetAllSlots().Length)
            {
                Debug.LogError("Number of slots object is not equal to number of slots in Inventory");
                return;
            }    
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetSlotData(inventory.GetSlot(i));
            }
        }

        /// <summary>
        /// Check if Inventory have enough item to craft Item.
        /// </summary>
        /// <param name="recipe">Craft Recipe to craft Item.</param>
        /// <returns>Return true if have enough Item.</returns>
        public bool CanCraft(CraftingRecipe material)
        {
            return inventory.CanCraft(material);
        }

        /// <summary>
        /// Check if a Item is in Inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool HaveItem(ItemBase item)
        {
            for (int i = 0; i < inventoryBase.GetAllSlots().Length; i++)
            {
                SlotBase slot = inventoryBase.GetAllSlots()[i];
                if (slot.getItem == item)
                    return true;
            }

            return false;
        }

        #region Inventory Event
        /// <summary>
        /// Trigger when a Slot being Left Clicked.
        /// </summary>
        /// <param name="slotdata"></param>
        /// <param name="slot"></param>
        public virtual void OnSlotLeftClick(SlotBase slotdata, Slot slot)
        {

        }
        /// <summary>
        /// Trigger when a Slot being Right Click.
        /// </summary>
        /// <param name="slotdata"></param>
        /// <param name="slot"></param>
        public virtual void OnSlotRightClick(SlotBase slotdata, Slot slot)
        {

        }
        /// <summary>
        /// Trigger when a mouse is enter a Slot.
        /// </summary>
        /// <param name="slotdata"></param>
        /// <param name="slot"></param>
        public virtual void OnSlotEnter(SlotBase slotdata, Slot slot)
        {

        }
        /// <summary>
        /// Trigger when a mouse is get out of a Slot.
        /// </summary>
        /// <param name="slotdata"></param>
        /// <param name="slot"></param>
        public virtual void OnSlotExit(SlotBase slotdata, Slot slot)
        {

        }
        /// <summary>
        /// Trigger when Inventory is open.
        /// </summary>
        public virtual void OnOpenInventory()
        {

        }
        /// <summary>
        /// Trigger when Inventory is Close.
        /// </summary>
        public virtual void OnCloseInventory()
        {

        }
        /// <summary>
        /// Trigger when a Item is added.
        /// </summary>
        /// <param name="item">Item added.</param>
        /// <param name="amount">Amount added.</param>
        public virtual void OnAddItem(ItemBase item, int amount)
        {

        }

        /// <summary>
        /// Trigger when a Item is removed.
        /// </summary>
        /// <param name="item">Item removed.</param>
        /// <param name="amount">Amount removed.</param>
        public virtual void OnRemoveItem(ItemBase item, int amount)
        {

        }
        #endregion
    }
}
