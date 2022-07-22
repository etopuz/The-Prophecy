using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Items
{
    public class InventoryManager : Singleton<InventoryManager>
    {

        public ItemDatabase itemDatabase;

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
    }

}
