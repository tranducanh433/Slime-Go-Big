using MySystem.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySystem.Crafting
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "My System/Inventory System/Crafting Recipe")]
    public class CraftingRecipe : ScriptableObject
    {
        [SerializeField] string recipeId = "rec0000";
        [SerializeField] ItemBase itemGet;
        [SerializeField] int amountGet = 1;
        [SerializeField] CraftMaterial[] materialsNeed;

        public string RecipeId { get { return recipeId; } }
        public ItemBase ItemGet { get { return itemGet; } }
        public int AmountGet { get { return amountGet; } }
        public CraftMaterial[] Materials { get { return materialsNeed; } }


    }

    [System.Serializable]
    public class CraftMaterial
    {
        [SerializeField] ItemBase itemNeed;
        [SerializeField] int amountNeed;

        public ItemBase item { get { return itemNeed; } }

        public int amount { get { return amountNeed; } }
    }
}
