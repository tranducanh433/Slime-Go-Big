using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySystem.Crafting
{
    public class CraftingUI : MonoBehaviour
    {
        [SerializeField] Transform itemCanCraftContainer;
        [SerializeField] Transform materialNeedContainer;
        [SerializeField] GameObject displayItemCanCraftprefab;
        [SerializeField] GameObject displayMaterialNeedprefab;
        [SerializeField] CraftingRecipe[] itemCanCrafts;
        CraftingRecipe selectRecipe;

        private void OnEnable()
        {
            DisplayItemCanCraft();
        }

        void DisplayItemCanCraft()
        {
            ClearAll();
            foreach(CraftingRecipe recipe in itemCanCrafts)
            {
                GameObject display = Instantiate(displayItemCanCraftprefab, itemCanCraftContainer);
                display.GetComponent<ItemCanCraftSlot>().SetSlotData(recipe);
            }
        }

        public void OnSlotClick(CraftingRecipe recipe)
        {
            selectRecipe = recipe;
            ClearMaterial();
            CraftMaterial[] materials = recipe.Materials;

            foreach (CraftMaterial material in materials)
            {
                GameObject display = Instantiate(displayMaterialNeedprefab, materialNeedContainer);
                display.GetComponent<MaterialToCraftSlot>().SetSlotData(material.item, material.amount);
            }
        }
        public void OnSlotEnter(CraftingRecipe recipe)
        {

        }
        public void OnSlotExit(CraftingRecipe recipe)
        {

        }

        void ClearAll()
        {
            for (int i = 0; i < itemCanCraftContainer.childCount; i++)
            {
                Destroy(itemCanCraftContainer.GetChild(i).gameObject);
            }
            for (int i = 0; i < materialNeedContainer.childCount; i++)
            {
                Destroy(materialNeedContainer.GetChild(i).gameObject);
            }
        }

        void ClearMaterial()
        {
            for (int i = 0; i < materialNeedContainer.childCount; i++)
            {
                Destroy(materialNeedContainer.GetChild(i).gameObject);
            }
        }

        public void Button_Craft()
        {
            if(selectRecipe != null)
            {
                Inventory.Inventory inventory = GameObject.Find("Inventory Manager").GetComponent<Inventory.Inventory>();
                bool canCraft = inventory.CanCraft(selectRecipe);

                if (canCraft)
                {
                    CraftMaterial[] materials = selectRecipe.Materials;
                    foreach (CraftMaterial material in materials)
                    {
                        inventory.RemoveItem(material.item, material.amount);
                    }
                    inventory.AddItem(selectRecipe.ItemGet, selectRecipe.AmountGet);
                }
            }
        }
    }
}
