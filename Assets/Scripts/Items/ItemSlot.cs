using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Items
{
    [System.Serializable]
    public class ItemSlot
    {
        public int numberOfStackedItems = 0;
        private ItemSO _item;

        public ItemSlot(ItemSO item, int count)
        {
            _item = item;
            numberOfStackedItems = count;
        }



    }
}
