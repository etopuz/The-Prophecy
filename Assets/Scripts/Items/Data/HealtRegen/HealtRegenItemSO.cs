using UnityEngine;

namespace TheProphecy.Items
{
    [CreateAssetMenu(fileName = "New Healt_Regen_Item", menuName = "Item/Health_Regen_Item")]
    
    public class HealtRegenItemSO : ItemSO
    {
        public int healthRegenAmount = 10;

        private void Awake()
        {
            itemType = ItemType.HEALTH_REGEN;
        }

    }
}

