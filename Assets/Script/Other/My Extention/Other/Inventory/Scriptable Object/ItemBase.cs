using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySystem.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "My System/Inventory System/ItemBase")]
    public class ItemBase : ScriptableObject
    {
        [Header("Item Information")]
        [SerializeField] string itemID;
        [SerializeField] Sprite itemSprite;
        [SerializeField] string itemName;
        [TextArea]
        [SerializeField] string description;

        [Header("Item Setting")]
        [SerializeField] bool stackLimite = true;
        [SerializeField] int maxStack = 20;

        /// <summary>
        /// Get Item Sprite.
        /// </summary>
        /// <returns></returns>
        public Sprite Sprite { get { return itemSprite; } }

        /// <summary>
        /// Get Item Name.
        /// </summary>
        /// <returns></returns>
        public string Name { get { return itemName; } }

        public string Id { get { return itemID; } }

        /// <summary>
        /// Get Item Description.
        /// </summary>
        /// <returns></returns>
        public string Description { get { return description; } }


        /// <summary>
        /// Get amount of Item that can stack.
        /// Return -1 if this Item do not have Stack Limite.
        /// </summary>
        /// <returns></returns>
        public int GetMaxStack()
        {
            return stackLimite ? maxStack : -1;
        }
    }
}
