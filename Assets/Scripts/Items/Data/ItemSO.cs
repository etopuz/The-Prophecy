using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Items
{
    public abstract class ItemSO : ScriptableObject
    {
        public string displayName;
        public Sprite icon;
        public int price;
        public ItemType itemType;
    }
}

