using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Items
{
    public class InventoryManager : MonoBehaviour
    {
        private const int INVENTORY_SIZE = 18;
        public ItemDatabase itemDatabase;

        private Dictionary<ItemSO, ItemSlot> _itemSlotsByItemDatas = new Dictionary<ItemSO, ItemSlot>();
        public List<ItemSlot> inventory { get; private set; }

        private void Awake()
        {
            inventory = new List<ItemSlot>();
        }

        private void Start()
        {
            //Test();
        }


        public bool TryToAddItem(ItemSO item, int count = 1)
        {
            if (_itemSlotsByItemDatas.TryGetValue(item, out ItemSlot itemSlot))
            {
                itemSlot.AddToStack(count);
                return true;
            }

            if (inventory.Count < INVENTORY_SIZE)
            {
                ItemSlot newSlot = new ItemSlot(item, count);
                inventory.Add(newSlot);
                _itemSlotsByItemDatas.Add(item, newSlot);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            Remove(inventory[index].item);
        }

        public void Remove(ItemSO item, int count = 1)
        {
            if (_itemSlotsByItemDatas.TryGetValue(item, out ItemSlot itemSlot))
            {
                itemSlot.RemoveFromStack(count);

                if(itemSlot.stackSize == 0)
                {
                    inventory.Remove(itemSlot);
                    _itemSlotsByItemDatas.Remove(item);
                }
            }
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
                }
            }
        }

    }

}
