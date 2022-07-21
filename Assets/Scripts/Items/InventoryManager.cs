using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Items
{
    public class InventoryManager : MonoBehaviour
    {
        public List<ItemSO> items = new List<ItemSO>();

        public void Start()
        {
            Test();
        }

        public void Test()
        {
            foreach (var item in items)
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
