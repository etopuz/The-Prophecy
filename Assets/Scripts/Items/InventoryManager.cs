using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Items
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        private const int INVENTORY_SIZE = 18;
        public ItemDatabase itemDatabase;
        public List<ItemSlot> inventory = new List<ItemSlot>();

        public void Start()
        {
            //Test();
        }

        public void Test()
        {
            foreach (var item in itemDatabase.allItems)
            {
                switch (item.itemType)
                {
                    case ItemType.HEALTH_REGEN:
                        Debug.Log(((HealtRegenItemSO)item).healthRegenAmount);
                        break;
                    case ItemType.EXPLOSIVE:
                        Debug.Log(((ExplosiveItemSO)item).explosionRange);
                        break;
                    default:
                        Debug.Log("powerup");
                        break;
                }
            }
        }

        public bool TryToAddItem(ItemSO item, int count = 0)
        {
            if(inventory.Count < INVENTORY_SIZE)
            {
                ItemSlot itemSlot = new ItemSlot(item, count);
            }

            else
            {

            }
            return false;
        }

    }

}
