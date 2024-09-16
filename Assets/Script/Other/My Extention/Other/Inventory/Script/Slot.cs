using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace MySystem.Inventory
{
    public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI textMesh;
        SlotBase slotData;

        /*/// <summary>
        /// Do not use and delete this funcion, this funcion is used in InventoryHierarchyMenu script
        /// </summary>
        public void SetDefaultOption()
        {
            itemImage.GetComponentInChildren<Image>();
            textMesh.GetComponentInChildren<TextMeshProUGUI>();
        }*/

        /// <summary>
        /// Set the Slot Object data to Slot
        /// </summary>
        /// <param name="slotData">Data to set.</param>
        public void SetSlotData(SlotBase slotData)
        {
            this.slotData = slotData;

            UpdateDisplay();
        }

        /// <summary>
        /// Crear the Data of the Slot.
        /// </summary>
        public void ClearData()
        {
            slotData.Clear();
            UpdateDisplay();
        }

        /// <summary>
        /// Call this function when the Slot Data is change to update UI Display of Slot
        /// </summary>
        public void UpdateDisplay()
        {
            if (slotData.getItem != null)
            {
                itemImage.sprite = slotData.getItem.Sprite;
                textMesh.text = slotData.getAmount.ToString();

                itemImage.gameObject.SetActive(true);
                textMesh.gameObject.SetActive(true);
            }
            else
            {
                itemImage.sprite = null;
                textMesh.text = "0";

                itemImage.gameObject.SetActive(false);
                textMesh.gameObject.SetActive(false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Inventory inventory = GetComponentInParent<Inventory>();
            inventory.OnSlotEnter(slotData, this);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            Inventory inventory = GetComponentInParent<Inventory>();
            inventory.OnSlotExit(slotData, this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Inventory inventory = GetComponentInParent<Inventory>();
            if(eventData.button == PointerEventData.InputButton.Left)
                inventory.OnSlotLeftClick(slotData, this);
            if (eventData.button == PointerEventData.InputButton.Right)
                inventory.OnSlotRightClick(slotData, this);
        }
    }
}
