using MySystem.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MySystem.Crafting
{
    public class MaterialToCraftSlot : MonoBehaviour/*, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler*/
    {
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI textMesh;

        ItemBase item;
        int amount;

        /// <summary>
        /// Do not use and delete this funcion, this funcion is used in InventoryHierarchyMenu script
        /// </summary>
        public void SetDefaultOption()
        {
            itemImage.GetComponentInChildren<Image>();
            textMesh.GetComponentInChildren<TextMeshProUGUI>();
        }

        /// <summary>
        /// Set the Slot Object data to Slot
        /// </summary>
        /// <param name="slotData"></param>
        public void SetSlotData(ItemBase item, int amount)
        {
            this.item = item;
            this.amount = amount;

            UpdateDisplay();
        }

        /// <summary>
        /// Call this function when the Slot Data is change to update UI Display of Slot
        /// </summary>
        public void UpdateDisplay()
        {
            Sprite itemSprite = item.Sprite;
            int amountGet = amount;

            itemImage.sprite = itemSprite;
            textMesh.text = amountGet.ToString();

            if (amountGet == 1)
                textMesh.gameObject.SetActive(false);
        }

        /*public void OnPointerClick(PointerEventData eventData)
        {
            if (clickable == false) return;
            CraftingUI craftUI = GetComponentInParent<CraftingUI>();
            craftUI.OnSlotClick(recipe);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            CraftingUI craftUI = GetComponentInParent<CraftingUI>();
            craftUI.OnSlotEnter(recipe);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            CraftingUI craftUI = GetComponentInParent<CraftingUI>();
            craftUI.OnSlotExit(recipe);
        }*/
    }
}

